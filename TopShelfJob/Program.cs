using Topshelf;

namespace TopShelfJob;

/// <summary>
/// TopShelfJob.exe install     - In console to install service
/// TopShelfJob.exe start       - In console to start service
/// TopShelfJob.exe stop        - In console to stop service
/// TopShelfJob.exe uninstall   - In console to uninstall service
/// </summary>
internal class Program
{
    static void Main(string[] args)
    {
        var rc = HostFactory.Run(x =>
        {
            x.Service<TownCrier>(s =>
            {
                s.ConstructUsing(name => new TownCrier());
                s.WhenStarted(tc => tc.Start());
                s.WhenStopped(tc => tc.Stop());
            });

            x.RunAsLocalSystem();

            x.SetDescription("Sample Topshelf Host");
            x.SetDisplayName("Stuff");
            x.SetServiceName("Stuff");
        });

        var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
        Environment.ExitCode = exitCode;
    }
}

