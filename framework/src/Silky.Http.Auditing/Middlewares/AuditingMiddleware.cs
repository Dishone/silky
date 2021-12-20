using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Silky.Core;
using Silky.Core.Extensions;
using Silky.Core.Rpc;
using Silky.Rpc.Auditing;
using Silky.Rpc.Configuration;
using Silky.Rpc.Runtime.Server;
using UAParser;
using ISession = Silky.Rpc.Runtime.Server.ISession;

namespace Silky.Http.Auditing.Middlewares;

public class AuditingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ISession _session;
    private readonly AuditingOptions _auditingOptions;
    private readonly IAuditSerializer _auditSerializer;
    private readonly ILogger<AuditingMiddleware> _logger;

    public AuditingMiddleware(
        RequestDelegate next,
        IOptions<AuditingOptions> auditingOptions,
        IAuditSerializer auditSerializer,
        ILogger<AuditingMiddleware> logger)
    {
        _next = next;
        _auditSerializer = auditSerializer;
        _logger = logger;
        _auditingOptions = auditingOptions.Value;
        _session = NullSession.Instance;
    }

    public async Task Invoke(HttpContext context)
    {
        if (_auditingOptions.IsEnabled)
        {
            var userAgent = context.Request.Headers["User-Agent"];
            var uaParser = Parser.GetDefault();
            var clientInfo = uaParser.Parse(userAgent);

            var auditLogInfo = new AuditLogInfo()
            {
                Url = context.Request.Path,
                HttpMethod = context.Request.Method,
                ExecutionTime = DateTimeOffset.Now,
                BrowserInfo = clientInfo.String,
                ClientId = context.Connection.Id,
                ClientIpAddress = context.Connection.RemoteIpAddress?.MapToIPv4().ToString(),
                CorrelationId = context.TraceIdentifier,
            };
            try
            {
                await _next(context);
            }
            finally
            {
                auditLogInfo.UserId = _session.UserId;
                auditLogInfo.UserName = _session.UserName;
                auditLogInfo.TenantId = _session.TenantId;
                auditLogInfo.HttpStatusCode = context.Response.StatusCode;
                auditLogInfo.ExceptionMessage = context.Features.Get<ExceptionHandlerFeature>()?.Error.Message;
                auditLogInfo.ExecutionDuration =
                    (int)(DateTimeOffset.Now - auditLogInfo.ExecutionTime).TotalMilliseconds;
                auditLogInfo.Actions = SetAuditLogActions();
                var auditingStore = EngineContext.Current.ServiceProvider.GetService<IAuditingStore>();
                if (auditingStore != null)
                {
                    await auditingStore.SaveAsync(auditLogInfo);
                }

                _logger.LogDebug(auditLogInfo.ToString());
            }
        }
        else
        {
            await _next(context);
        }
    }

    private List<AuditLogActionInfo> SetAuditLogActions()
    {
        var auditLogActions = new List<AuditLogActionInfo>();

        var auditLogActionsResultAttachment = RpcContext.Context.GetResultAttachment(AttachmentKeys.AuditActionLog);
        if (auditLogActionsResultAttachment != null)
        {
            var auditLogActionsValues = auditLogActionsResultAttachment.ConventTo<IList<string>>();
            foreach (var auditLogActionsValue in auditLogActionsValues)
            {
                var auditLogAction = _auditSerializer.Deserialize<AuditLogActionInfo>(auditLogActionsValue);
                auditLogActions.Add(auditLogAction);
            }
        }

        return auditLogActions.OrderBy(p => p.ExecutionTime).ToList();
    }
}