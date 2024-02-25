using System.Collections.Generic;

namespace Maestro.Core.Services.Messaging;

internal class FolderItem
{
    public required string Name { get; set; }
}

internal class ResourceItem
{
    public required string Name { get; set; }
}

internal class ResourceListMessage
{
    public string? Path { get; set; }

    public List<FolderItem> Folders { get; } = new();

    public List<ResourceItem> Resources { get; } = new();
}
