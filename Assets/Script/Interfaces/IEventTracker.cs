public interface IEventTracker
{
    void Initialize(int levelId);
    void ResetTracker();
    string GetTrackerId();
    void SendEvent();
}
