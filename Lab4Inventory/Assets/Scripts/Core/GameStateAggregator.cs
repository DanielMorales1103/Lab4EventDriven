using System.Collections.Generic;
using UnityEngine;

public class GameStateAggregator : MonoBehaviour
{
    [Header("Refs")]
    public Transform player;          
    public ScoreManager score;        
    public Inventory inventory;       
    public Health health;             
    public DoorController door;      
    public SpeedBuffTP speedBuff;

    public PickUpCatalog catalog;

    public SaveData BuildSave()
    {
        var d = new SaveData();

        d.coins = score ? score.Score : 0;                   
        d.hp = health ? health.Current : 100;             
        if (player) d.SetPos(player.position);

        d.doorOpened = door && door.IsOpen;                  

        d.collectedPickupIds = CollectedRegistry.ExportList();

        d.inventoryKinds.Clear();
        if (inventory != null && inventory.Items != null)
        {
            foreach (var it in inventory.Items)
            {
                if (it == null) continue;
                if (it.goesToInventory)
                    d.inventoryKinds.Add(it.kind.ToString()); 
            }
        }

        d.speedBuffTimeLeft = 0f;

        return d;
    }

    public void Apply(SaveData d)
    {
        CollectedRegistry.ImportList(d.collectedPickupIds);

        if (player) player.position = d.GetPos();

        if (health) health.SetHP(Mathf.RoundToInt(d.hp));

        if (score)
        {
            int cur = score.Score;
            int delta = d.coins - cur;
            if (delta != 0) EventBus.Publish<int>(EventIds.ScoreChanged, delta); 
        }

        if (door)
        {
            if (d.doorOpened) door.ForceOpen();  
        }

        if (inventory)
        {
            var list = new List<PickUpData>();
            if (catalog != null && d.inventoryKinds != null)
            {
                foreach (var kindName in d.inventoryKinds)
                {
                    if (catalog.TryGet(kindName, out var so) && so != null)
                        list.Add(so);
                }
            }
            inventory.Import(list); 
        }

        if (door && catalog != null && d.inventoryKinds != null)
        {
            bool hadKey = d.inventoryKinds.Exists(k => string.Equals(k, PickUpKind.Key.ToString(), System.StringComparison.OrdinalIgnoreCase));
            if (hadKey)
            {
                var keySO = catalog.keyItem;
                string keyName = keySO ? keySO.doorKey : null;
                door.GrantKey(keyName);
            }
        }

    }
}
