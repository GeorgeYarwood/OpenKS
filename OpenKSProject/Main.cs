using OpenKSProject;
using System.Runtime.Serialization.Formatters.Binary;
using System.Timers;

const int SLOW_UPDATE_TIMER_INTERVAL = 5000;
const string CONFIG_FILE_LOADED_MESSAGE = "Config file found, loading OpenKs...";
const string CONFIG_FILE_PATH = "./Config.ksFile";
const string FILE_LOCK_ERROR = "Unable to save config file, is being used by another process!";

List<Subsystem> allSubsystems = new List<Subsystem>();
SerialisedConfig ksConfig = new SerialisedConfig();

Setup SetupInstance = new();

System.Timers.Timer SlowUpdateTimer
    = new System.Timers.Timer(SLOW_UPDATE_TIMER_INTERVAL);

InitSubsystems();


void SaveCurrentConfig()
{
    BinaryFormatter ThisFormatter = new();
    try
    {
        Stream SavedFile = new FileStream(CONFIG_FILE_PATH, FileMode.Create);
        ThisFormatter.Serialize(SavedFile, ksConfig);
        SavedFile.Close();
    }
    catch
    {
        Console.WriteLine(FILE_LOCK_ERROR);
        Console.ReadKey();
        return;
    }
}

bool LoadSavedConfig()
{
    BinaryFormatter ThisFormatter = new();
    try
    {
        Stream SavedFile = new FileStream(CONFIG_FILE_PATH, FileMode.Open, FileAccess.Read);
        if (SavedFile != null)
        {
            ksConfig = (SerialisedConfig)ThisFormatter.Deserialize(SavedFile);
            SavedFile.Close();
            return true;
        }
    }
    catch
    {
        return false;
    }
    return false;
}

//TODO GPIO (Ticket beep, bump bar input), Networking between nodes, menu editing

void InitSubsystems()
{
    bool UpdateSubsytems = false;
    if (!LoadSavedConfig())
    {
        if (!SetupInstance.HasSetupBeenRun)
        {
            SetupInstance.Init();
            while (!SetupInstance.HasSetupBeenRun)
            {
                continue;
            }
            ksConfig = SetupInstance.ReturnKsConfig();
            SaveCurrentConfig();
            InitSubsystems(); //The first time setup must be completed before continuing
        }
    }
    else
    {
        Console.WriteLine(CONFIG_FILE_LOADED_MESSAGE);
        UpdateSubsytems = true;
    }
  
    UiManager UiManagerInstance = new();
    TicketManager TicketManagerInstance = new();
    InputManager InputManagerInstance = new();
    NetworkManager NetworkManagerInstance = new();
    SerialReader SerialReaderInstance = new();
    MenuSearch MenuSearchInstance = new();
    HardwareManager HardwareManagerInstance = new();

    if(UpdateSubsytems)
    {
        //Network
        NetworkManagerInstance.KsMode = ksConfig.KsMode;
        NetworkManagerInstance.HostDiscoveryMode = ksConfig.HostDiscoveryMode;

        //TODO IP address

        //Serial/COM
        if (ksConfig.BaudRate != null)
        {
            SerialReaderInstance.ConfiguredBaud = (int)ksConfig.BaudRate;
        }
        if(ksConfig.ComPort != null)
        {
            SerialReaderInstance.ConfiguredPort = ksConfig.ComPort;
        }
        //Hardware (GPIO)
        HardwareManagerInstance.TicketBeepEnabled = ksConfig.TicketBeepEnabled;

        //TODO TicketManager late ticket threshhold

    }

    allSubsystems.Add(UiManagerInstance);
    allSubsystems.Add(TicketManagerInstance);
    allSubsystems.Add(InputManagerInstance);
    allSubsystems.Add(NetworkManagerInstance);
    allSubsystems.Add(MenuSearchInstance);
    allSubsystems.Add(SerialReaderInstance);
    allSubsystems.Add(HardwareManagerInstance);

    for (int s = 0; s < allSubsystems.Count; s++)
    {
        allSubsystems[s].Init();
    }

    InitTimer();

    Console.Clear();

    HardwareManagerInstance.TicketBeep();

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