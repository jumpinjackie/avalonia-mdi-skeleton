namespace Maestro.Services.Messaging;

internal class ConnectedToSiteMessage
{
    public required string Name { get; set; }

    public required ResourceListMessage Root { get; set; }
}
