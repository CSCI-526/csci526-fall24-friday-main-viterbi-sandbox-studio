public interface IEventTracker
{
    void Initialize(int levelId);
    void ResetTracker();
    void SendEvent();
}
