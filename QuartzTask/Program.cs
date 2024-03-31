using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using QuartzTask;

//This need to install:
//install-package Microsoft.Extensions.Hosting
//install-package Microsoft.Extensions.DependencyInjection
//Install-Package Quartz.Extensions.DependencyInjection
//install-package Quartz.Extensions.Hosting

var builder = Host.CreateApplicationBuilder(args);

var services = builder.Services;

services.AddSingleton<ContactService>();

services.AddQuartz(q =>
    {
        q.ScheduleJob<PrintToConsoleJob>(trigger =>
            trigger
                .WithIdentity(nameof(PrintToConsoleJob))
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(3)
                    .RepeatForever()));
    }
);

services.AddQuartzHostedService(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;

    // when we need to init another IHostedServices first
    options.StartDelay = TimeSpan.FromSeconds(3);
});

using var host = builder.Build();

await host.RunAsync();