namespace Maestro.Core.Services.Messaging;

internal class FolderListedMessage
{
    public required ResourceListMessage List { get; set; }
    public required string SiteName { get; set; }
}
