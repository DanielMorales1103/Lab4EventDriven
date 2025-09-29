using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private readonly List<PickUpData> _items = new();
    public IReadOnlyList<PickUpData> Items => _items;

    private void OnEnable()
    {
        EventBus.Subscribe<PickUpData>(EventIds.ItemCollected, OnItemCollected);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PickUpData>(EventIds.ItemCollected, OnItemCollected);
    }

    private void OnItemCollected(PickUpData data)
    {
        if (data == null) return;
        _items.Add(data);
        EventBus.Publish(EventIds.InventoryUpdated);
    }
}
