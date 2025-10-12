using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickUp : MonoBehaviour
{
    [SerializeField] public PickUpData data;    
    bool collected;

    [SerializeField] private string persistentId;

#if UNITY_EDITOR
    void OnValidate()
    {
        if (string.IsNullOrEmpty(persistentId))
            persistentId = System.Guid.NewGuid().ToString();

        var col = GetComponent<Collider>();
        if (col) col.isTrigger = true;
    }
#endif

    void Start()
    {
        if (!string.IsNullOrEmpty(persistentId) && CollectedRegistry.IsCollected(persistentId))
        {
            Destroy(gameObject);
            return;
        }
    }

    void Reset()
    {
        var col = GetComponent<Collider>();
        if (col) col.isTrigger = true;         
    }

    void OnTriggerEnter(Collider other)
    {
        if (collected) return;
        if (!other.CompareTag("Player")) return;  
        Collect();
    }

    void Collect()
    {
        collected = true;
        var label = string.IsNullOrWhiteSpace(data?.displayName) ? name : data.displayName;

        if (data != null && data.kind == PickUpKind.Score && data.scoreAmount != 0)
        {
            EventBus.Publish<int>(EventIds.ScoreChanged, data.scoreAmount);
        }

        if (data != null && data.goesToInventory)
        {
            EventBus.Publish<PickUpData>(EventIds.ItemCollected, data);
        }

        if (data != null && data.kind == PickUpKind.Key && !string.IsNullOrWhiteSpace(data.doorKey))
        {
            EventBus.Publish<string>(EventIds.DoorOpen, data.doorKey);
        }

        if (data != null && data.kind == PickUpKind.Heal && data.healAmount > 0)
        {
            EventBus.Publish<int>(EventIds.HealRequested, data.healAmount);
        }

        if (data != null && data.kind == PickUpKind.Damage && data.damageAmount > 0)
        {
            EventBus.Publish<int>(EventIds.DamageRequested, data.damageAmount);
        }

        if (data != null && data.kind == PickUpKind.Speed && data.speedSeconds > 0f)
        {
            EventBus.Publish<float>(EventIds.SpeedBuffRequested, data.speedSeconds);
        }

        if (data != null && data.audioClip)
        {
            EventBus.Publish<AudioClip>(EventIds.PlaySfx, data.audioClip);
        }

        if (!string.IsNullOrEmpty(persistentId))
            CollectedRegistry.MarkCollected(persistentId);

        Destroy(gameObject); 
    }
}
