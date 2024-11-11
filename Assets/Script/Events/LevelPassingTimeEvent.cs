using Unity.Services.Analytics;

public class LevelPassingTimeEvent : Event
{
    // Constructor to initialize the event with its name
    public LevelPassingTimeEvent() : base("levelPassingTime")
    {
    }

    // Property to set the characterSwitchCounts parameter (int)
    public float TimeSpent
    {
        set { SetParameter("timeSpent", value); }
    }

    // Property to set the currentLevel parameter (int)
    public string CurrentLevelName
    {
        set { SetParameter("currentLevelName", value); }
    }
}
