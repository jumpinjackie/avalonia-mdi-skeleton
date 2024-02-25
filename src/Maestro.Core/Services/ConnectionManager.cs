using CommunityToolkit.Mvvm.Messaging;
using Maestro.Services.Messaging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Maestro.Services;

public class ConnectionManager
{
    private int _counter = 0;

    private Task<ResourceListMessage> ListResourcesAsync(string? path)
    {
        var msg = new ResourceListMessage
        {
            Path = path
        };

        // Simulating an enumerate resources
        //await Task.Delay(3000);
        var r = new Random();
        var numFolders = r.Next(3, 8);
        var numResources = r.Next(2, 4);
        foreach (int i in Enumerable.Range(0, numFolders))
        {
            msg.Folders.Add(new FolderItem { Name = "Folder " + i });
        }
        foreach (int i in Enumerable.Range(0, numResources))
        {
            msg.Resources.Add(new ResourceItem { Name = "Resource " + i + numFolders + 1 });
        }

        return Task.FromResult(msg);
    }

    public async Task ConnectAsync()
    {
        var name = $"MapGuide Site {_counter++}";
        var root = await ListResourcesAsync(null);

        WeakReferenceMessenger.Default.Send(new ConnectedToSiteMessage 
        {
            Name = name,
            Root = root
        });
    }
}
