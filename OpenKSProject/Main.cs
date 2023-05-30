using OpenKSProject;
using System.Timers;

const int SLOW_UPDATE_TIMER_INTERVAL = 5000;

List<Subsystem> AllSubsystems = new List<Subsystem>();
SerialisedConfig ksConfig = new SerialisedConfig();


System.Timers.Timer SlowUpdateTimer
    = new System.Timers.Timer(SLOW_UPDATE_TIMER_INTERVAL);

InitSubsystems();

void InitSubsystems()
{
    //TODO Load saved ksConfig
    //Let user input different menu items and save to database
    //Try using google doc to store items, sync periodically?
    //Each time ticket comes through, traverse database and find matching items

    UiManager UiManagerInstance = new();
    TicketManager TicketManagerInstance = new();
    InputManager InputManagerInstance = new();
    NetworkManager NetworkManagerInstance = new();
    SerialReader SerialReaderInstance = new();

    AllSubsystems.Add(UiManagerInstance);
    AllSubsystems.Add(TicketManagerInstance);
    AllSubsystems.Add(InputManagerInstance);
    AllSubsystems.Add(NetworkManagerInstance);
    AllSubsystems.Add(SerialReaderInstance);

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