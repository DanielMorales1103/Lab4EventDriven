using UnityEngine;

public class GameLoader : MonoBehaviour
{
    public GameStateAggregator state;

    void Awake()
    {
        if (PersistenceManager.TryLoad(out var data))
        {
            CollectedRegistry.ImportList(data.collectedPickupIds);
            _loaded = data;
            _has = true;
        }
    }

    bool _has;
    SaveData _loaded;

    void Start()
    {
        if (_has)
        {
            state.Apply(_loaded);
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
