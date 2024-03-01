using CommunityToolkit.Mvvm.Messaging;
using Maestro.Core.Services.Contracts;
using Maestro.Core.Services.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maestro.Core.Services.Stubs;

public class StubConnectionManager : IConnectionManager
{
    readonly IMessenger _messenger;
    private int _counter = 0;

    public StubConnectionManager(IMessenger messenger)
    {
        _messenger = messenger;
    }

    private Task<ResourceListMessage> ListResourcesInternalAsync(string site, IEnumerable<string> path)
    {
        // This is basically a stub for what would be a EnumerateResources API call
        // if this were a real implementation

        var msg = new ResourceListMessage { ParentPath = path };

        // Simulating an enumerate resources
        //await Task.Delay(3000);
        var r = new Random();
        var numFolders = r.Next(3, 8);
        var numResources = r.Next(2, 4);

        List<string> resourceTypes = [
            "FeatureSource",
            "LayerDefinition",
            "MapDefinition",
            "WebLayout",
            "SymbolLibrary",
            "LoadProcedure",
            "ApplicationDefinition",
            "SymbolDefinition",
            "WatermarkDefinition"
        ];

        foreach (int i in Enumerable.Range(0, numFolders))
        {
            msg.Folders.Add(new FolderItem { Name = "Folder " + i });
        }
        foreach (int i in Enumerable.Range(0, numResources))
        {
            // Add a resource of a random type
            msg.Resources.Add(new ResourceItem { Name = "Resource " + i + numFolders + 1, Type = resourceTypes[r.Next(resourceTypes.Count)] });
        }

        return Task.FromResult(msg);
    }

    public async Task ConnectAsync(string site, string username, string password)
    {
        var name = $"MapGuide Site {_counter++} - {site}";
        var root = await ListResourcesInternalAsync(site, []);

        _messenger.Send(new ConnectedToSiteMessage
        {
            SiteName = name,
            Root = root
        });
    }

    public async Task ListResourcesAsync(string site, IEnumerable<string> path)
    {
        var res = await ListResourcesInternalAsync(site, path);
        _messenger.Send(new FolderListedMessage
        {
            SiteName = site,
            List = res
        });
    }

    public void CancelConnect()
    {
        _messenger.Send(new CancelConnectMessage());
    }
}
