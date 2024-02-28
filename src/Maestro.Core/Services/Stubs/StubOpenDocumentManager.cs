using CommunityToolkit.Mvvm.Messaging;
using Maestro.Core.Services.Contracts;
using Maestro.Core.Services.Messaging;
using System.Threading.Tasks;

namespace Maestro.Core.Services.Stubs;

public class StubOpenDocumentManager : IOpenDocumentManager
{
    readonly IMessenger _messenger;

    public StubOpenDocumentManager(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public Task OpenDocumentAsync(string name)
    {
        _messenger.Send(new OpenDocumentMessage
        {
            Name = "New MapGuide Resource " + name,
            Content = $"Content for (New MapGuide Resource {name})"
        });
        return Task.CompletedTask;
    }

    public void CloseDocument(string? title)
    {
        _messenger.Send(new CloseDocumentMessage { Name = title });
    }
}
