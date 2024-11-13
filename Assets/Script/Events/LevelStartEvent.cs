using Unity.Services.Analytics;

public class LevelStartEvent : Event
{
    // Constructor to initialize the event with its name
    public LevelStartEvent() : base("levelStart")
    {
    }

    public int CurrentLevel
    {
        set { SetParameter("currentLevel", value); }
    }

    public string CurrentLevelName
    {
        set { SetParameter("currentLevelName", value); }
    }
}
