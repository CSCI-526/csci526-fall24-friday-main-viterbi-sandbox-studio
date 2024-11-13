using Unity.Services.Analytics;

public class LevelCompleteEvent : Event
{
    // Constructor to initialize the event with its name
    public LevelCompleteEvent() : base("levelComplete")
    {
    }

    public int CharacterSwitchCounts
    {
        set { SetParameter("characterSwitchCounts", value); }
    }

    public float TimeSpent
    {
        set { SetParameter("timeSpent", value); }
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
