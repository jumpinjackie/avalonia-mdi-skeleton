using CommunityToolkit.Mvvm.Messaging;
using Maestro.Services.Messaging;
using System.Threading.Tasks;

namespace Maestro.Services;

public class OpenResourceManager
{
    public OpenResourceManager()
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

    internal void Close(string? title)
    {
        WeakReferenceMessenger.Default.Send(new CloseResourceMessage { Name = title });
    }
}
