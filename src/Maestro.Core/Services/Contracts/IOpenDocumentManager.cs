using System.Threading.Tasks;

namespace Maestro.Core.Services.Contracts;

public interface IOpenDocumentManager
{
    Task OpenDocumentAsync(string name);

    void CloseDocument(string name);
}