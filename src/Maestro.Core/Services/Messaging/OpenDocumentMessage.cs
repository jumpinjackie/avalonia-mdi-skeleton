namespace Maestro.Core.Services.Messaging;

internal class OpenDocumentMessage
{
    public object? Content { get; set; }
    
    public required string Name { get; set; }
}
