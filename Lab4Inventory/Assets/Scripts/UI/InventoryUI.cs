using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject itemTemplate;

    private Action<object> _handler;

    private void OnEnable()
    {
        _handler = _ => Redraw();
        EventBus.Subscribe<object>(EventIds.InventoryUpdated, _handler);
        Redraw();
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<object>(EventIds.InventoryUpdated, _handler);
    }

    private void Redraw()
    {
        if (!inventory || !content || !itemTemplate) return;

        // limpia todo salvo el template
        for (int i = content.childCount - 1; i >= 0; i--)
        {
            var child = content.GetChild(i).gameObject;
            if (child != itemTemplate) Destroy(child);
        }

        foreach (var it in inventory.Items)
        {
            var go = Instantiate(itemTemplate, content);
            go.SetActive(true);

            // busca por nombre exacto; si falla, usa fallback
            var icon = go.transform.Find("Icon")?.GetComponent<Image>()
                       ?? go.GetComponentInChildren<Image>(true);
            var name = go.transform.Find("Name")?.GetComponent<TextMeshProUGUI>()
                       ?? go.GetComponentInChildren<TextMeshProUGUI>(true);

            if (icon) icon.sprite = it.icon;
            if (name) name.text = string.IsNullOrWhiteSpace(it.displayName) ? "(Unnamed)" : it.displayName;
        }

        Debug.Log($"[InventoryUI] Redraw items={inventory.Items.Count}");
    }
}
