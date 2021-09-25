using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Silky.Core;

namespace Silky.Rpc.Runtime.Server
{
    public class DefaultServiceEntryProvider : IServiceEntryProvider
    {
        public ILogger<DefaultServiceEntryProvider> Logger { get; set; }
        private readonly IServiceEntryGenerator _serviceEntryGenerator;
        private readonly ITypeFinder _typeFinder;

        public DefaultServiceEntryProvider(IServiceEntryGenerator serviceEntryGenerator,
            ITypeFinder typeFinder)
        {
            _serviceEntryGenerator = serviceEntryGenerator;
            _typeFinder = typeFinder;
            Logger = NullLogger<DefaultServiceEntryProvider>.Instance;
        }

        public IReadOnlyList<ServiceEntry> GetEntries()
        {
            var serviceTypeInfos = ServiceHelper.FindAllServiceTypes(_typeFinder);
            var entries = new List<ServiceEntry>();
            foreach (var serviceTypeInfo in serviceTypeInfos)
            {
                Logger.LogDebug("The Service were be found,type:{0},IsLocal:{1}", serviceTypeInfo.Item1.FullName,
                    serviceTypeInfo.Item2);
                entries.AddRange(_serviceEntryGenerator.CreateServiceEntry(serviceTypeInfo));
            }

            return entries;
        }
    }
}