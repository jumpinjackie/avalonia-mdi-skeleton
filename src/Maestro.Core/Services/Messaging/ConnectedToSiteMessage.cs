﻿namespace Maestro.Core.Services.Messaging;

internal class ConnectedToSiteMessage
{
    public required string SiteName { get; set; }

    public required ResourceListMessage Root { get; set; }
}
