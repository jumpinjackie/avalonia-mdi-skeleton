namespace Maestro.Core.Services.Messaging;

internal class OpenResourceMessage
{
    public required string Content { get; set; }
    
    public required string Name { get; set; }
}
