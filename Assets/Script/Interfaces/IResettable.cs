using UnityEngine;

public interface IResettable
{
    void SaveInitialState();
    void ResetState();
    void UpdateState(System.Action customResetAction = null);
}
