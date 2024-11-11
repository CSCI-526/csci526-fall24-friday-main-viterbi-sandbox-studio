using Unity.Services.Analytics;

public class TimeSpentOnLearningMechanicsEvent : Event
{
    // Constructor to initialize the event with its name
    public TimeSpentOnLearningMechanicsEvent() : base("timeSpentOnLearningMechanics")
    {
    }

    // Property to set the characterSwitchCounts parameter (int)
    public float TimeSpent
    {
        set { SetParameter("timeSpent", value); }
    }

    // Property to set the currentLevel parameter (int)
    public string MechanicName
    {
        set { SetParameter("mechanicName", value); }
    }
}
