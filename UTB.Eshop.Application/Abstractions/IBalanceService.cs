using System.Threading.Tasks;

namespace UTB.Eshop.Application.Abstractions
{
    public interface IBalanceService
    {
        Task<int> GetBalanceAsync(string userName);
    }
}
