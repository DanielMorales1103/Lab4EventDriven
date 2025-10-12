using System;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCatalog : MonoBehaviour
{
    [Header("Asignacion de ScriptableObjects")]
    public PickUpData speedPotion;
    public PickUpData healthPotion;
    public PickUpData damagePotion;
    public PickUpData keyItem;  

    Dictionary<string, PickUpData> _map;

    void Awake()
    {
        _map = new Dictionary<string, PickUpData>(StringComparer.OrdinalIgnoreCase)
        {
            { PickUpKind.Speed.ToString(),  speedPotion },
            { PickUpKind.Heal.ToString(),   healthPotion },
            { PickUpKind.Damage.ToString(), damagePotion },
            { PickUpKind.Key.ToString(),    keyItem }
        };
    }

    public bool TryGet(string kindName, out PickUpData data)
    {
        data = null; 
        return _map != null && _map.TryGetValue(kindName, out data);
    }

}
