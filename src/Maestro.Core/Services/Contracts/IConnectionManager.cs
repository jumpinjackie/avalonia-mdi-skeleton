using System.Threading.Tasks;

namespace Maestro.Core.Services.Contracts
{
    public interface IConnectionManager
    {
        Task ConnectAsync();
    }
}