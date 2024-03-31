﻿using Timer = System.Timers.Timer;

namespace TopShelfJob;

public class TownCrier
{
    readonly Timer _timer;

    public TownCrier()
    {
        _timer = new Timer(1000) { AutoReset = true };
        _timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is {0} and all is well", DateTime.Now);
    }

    public void Start()
    {
        _timer.Start();
    }

    public void Stop()
    {
        _timer.Stop();
    }
}