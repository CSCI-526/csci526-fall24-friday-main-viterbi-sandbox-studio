using Unity.Services.Analytics;

public class CharacterSwitchCountsEvent : Event
{
    // Constructor to initialize the event with its name
    public CharacterSwitchCountsEvent() : base("CharacterSwitchCountsEvent")
    {
    }

    // Property to set the characterSwitchCounts parameter (int)
    public int CharacterSwitchCounts
    {
        set { SetParameter("characterSwitchCounts", value); }
    }

    // Property to set the currentLevel parameter (int)
    public int CurrentLevel
    {
        set { SetParameter("currentLevel", value); }
    }
}
