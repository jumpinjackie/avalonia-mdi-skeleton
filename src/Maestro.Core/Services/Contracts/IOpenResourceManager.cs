using System.Threading.Tasks;

namespace Maestro.Core.Services.Contracts
{
    public interface IOpenResourceManager
    {
        Task OpenResourceAsync(string name);

        void Close(string name);
    }
}