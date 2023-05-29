using OpenKSProject;
using System.Timers;

const int SLOW_UPDATE_TIMER_INTERVAL = 5000;

List<Subsystem> AllSubsystems = new List<Subsystem>();

System.Timers.Timer SlowUpdateTimer
    = new System.Timers.Timer(SLOW_UPDATE_TIMER_INTERVAL);

InitSubsystems();

void InitSubsystems()
{
    UiManager UiManagerInstance = new();
    TicketManager TicketManagerInstance = new();
    InputManager InputManagerInstance = new();

    AllSubsystems.Add(UiManagerInstance);
    AllSubsystems.Add(TicketManagerInstance);
    AllSubsystems.Add(InputManagerInstance);

    for (int s = 0; s < AllSubsystems.Count; s++)
    {
        AllSubsystems[s].Init();
    }

    InitTimer();

    while (true)
    {
        FastUpdateSubsystems();
    }
}

void InitTimer()
{
    SlowUpdateTimer.Enabled = true;
    SlowUpdateTimer.Elapsed += SlowUpdateSubsystems;
    SlowUpdateTimer.AutoReset = true;
}

void FastUpdateSubsystems()
{
    for(int s = 0;  s < AllSubsystems.Count; s++)
    {
        AllSubsystems[s].FastUpdate();
    }
}

void SlowUpdateSubsystems(Object _, ElapsedEventArgs E)
{
    for (int s = 0; s < AllSubsystems.Count; s++)
    {
        AllSubsystems[s].SlowUpdate();
    }
}