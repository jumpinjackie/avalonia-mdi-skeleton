namespace Maestro.Services;

public class AppServices
{
    public AppServices()
    {
        this.ConnectionManager = new ConnectionManager();
        this.OpenResourceManager = new OpenResourceManager();
    }

    public ConnectionManager ConnectionManager { get; }

    public OpenResourceManager OpenResourceManager { get; }
}
