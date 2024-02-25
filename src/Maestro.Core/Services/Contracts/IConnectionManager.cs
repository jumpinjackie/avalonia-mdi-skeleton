using System.Threading.Tasks;

namespace Maestro.Core.Services.Contracts;

public interface IConnectionManager
{
    Task ConnectAsync(string site, string username, string password);

    void CancelConnect();
}