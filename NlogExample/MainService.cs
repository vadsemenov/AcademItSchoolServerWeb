using Microsoft.Extensions.Logging;

namespace NLogExample;

public class MainService
{
    private readonly ILogger<MainService> _logger;

    public MainService(ILogger<MainService> logger)
    {
        _logger = logger;
    }

    public void DoAction(string? name)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        _logger.LogDebug(20, "Doing hard work! {Action}", name);

        try
        {
            throw new Exception();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception in DoAction method");
        }
    }
}