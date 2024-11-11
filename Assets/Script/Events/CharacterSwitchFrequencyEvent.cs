using Unity.Services.Analytics;

public class CharacterSwitchFrequencyEvent : Event
{
    // Constructor to initialize the event with its name
    public CharacterSwitchFrequencyEvent() : base("chracterSwitchFrequency")
    {
    }

    // Property to set the characterSwitchCounts parameter (int)
    public int CharacterSwitchCounts
    {
        set { SetParameter("characterSwitchCounts", value); }
    }

    // Property to set the currentLevel parameter (int)
    public string CurrentLevelName
    {
        set { SetParameter("currentLevelName", value); }
    }
}
