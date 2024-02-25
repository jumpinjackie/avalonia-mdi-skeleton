using CommunityToolkit.Mvvm.Messaging;
using Maestro.Core.Services.Contracts;
using Maestro.Services.Messaging;
using System.Threading.Tasks;

namespace Maestro.Core.Services.Stubs;

public class StubOpenResourceManager : IOpenResourceManager
{
    public StubOpenResourceManager()
    {

    }

    public Task OpenResourceAsync(string name)
    {
        WeakReferenceMessenger.Default.Send(new OpenResourceMessage
        {
            Name = "New MapGuide Resource " + name,
            Content = $"Content for (New MapGuide Resource {name})"
        });
        return Task.CompletedTask;
    }

    public void Close(string? title)
    {
        WeakReferenceMessenger.Default.Send(new CloseResourceMessage { Name = title });
    }
}
