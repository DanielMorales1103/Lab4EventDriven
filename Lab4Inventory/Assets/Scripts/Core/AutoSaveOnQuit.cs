using UnityEngine;

public class AutoSaveOnQuit : MonoBehaviour
{
    public GameStateAggregator state;

    void OnApplicationQuit() { Save("Quit"); }
    void OnApplicationPause(bool p) { if (p) Save("Pause"); }

    void Save(string reason)
    {
        if (!state) return;
        var ok = PersistenceManager.Save(state.BuildSave());
    }
}
