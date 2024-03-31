using Quartz;

namespace QuartzTask;

public class PrintToConsoleJob : IJob
{
    private ContactService _contactService;

    public PrintToConsoleJob(ContactService contactService)
    {
        _contactService = contactService;
    }

    public Task Execute(IJobExecutionContext context)
    {
        var contactsString = string.Join(" ",_contactService.Contacts);

        Console.WriteLine(DateTime.Now + " "+ contactsString);

        return Task.FromResult(true);
    }
}