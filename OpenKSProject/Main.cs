using OpenKSProject;
using System.Timers;

const int SLOW_UPDATE_TIMER_INTERVAL = 5000;

List<Subsystem> allSubsystems = new List<Subsystem>();
SerialisedConfig ksConfig = new SerialisedConfig();

System.Timers.Timer SlowUpdateTimer
    = new System.Timers.Timer(SLOW_UPDATE_TIMER_INTERVAL);

InitSubsystems();

void InitSubsystems()
{
    Setup SetupInstance = new();
    if (!SetupInstance.HasSetupBeenRun)
    {
        SetupInstance.Init();
        return; //The first time setup must be completed before continuing
    }

    UiManager UiManagerInstance = new();
    TicketManager TicketManagerInstance = new();
    InputManager InputManagerInstance = new();
    NetworkManager NetworkManagerInstance = new();
    SerialReader SerialReaderInstance = new();
    MenuSearch MenuSearchInstance = new();

    allSubsystems.Add(UiManagerInstance);
    allSubsystems.Add(TicketManagerInstance);
    allSubsystems.Add(InputManagerInstance);
    allSubsystems.Add(NetworkManagerInstance);
    allSubsystems.Add(MenuSearchInstance);
    allSubsystems.Add(SerialReaderInstance);
    allSubsystems.Add(SetupInstance);

    for (int s = 0; s < allSubsystems.Count; s++)
    {
        allSubsystems[s].Init();
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
    for(int s = 0;  s < allSubsystems.Count; s++)
    {
        allSubsystems[s].FastUpdate();
    }
}

void SlowUpdateSubsystems(Object _, ElapsedEventArgs E)
{
    for (int s = 0; s < allSubsystems.Count; s++)
    {
        allSubsystems[s].SlowUpdate();
    }
}