using System.Threading.Tasks;
using Lms.Account.Application.Contracts.Accounts.Dtos;
using Silky.Lms.Core.DependencyInjection;
using Silky.Lms.Transaction.Tcc;

namespace Lms.Account.Domain.Accounts
{
    public interface IAccountDomainService : ITransientDependency
    {
        Task<Account> Create(Account account);
        Task<Account> GetAccountByName(string name);
        Task<Account> GetAccountById(long id);
        Task<Account> Update(UpdateAccountInput input);
        Task Delete(long id);
        Task<long?> DeductBalance(DeductBalanceInput input, TccMethodType tccMethodType);
    }
}