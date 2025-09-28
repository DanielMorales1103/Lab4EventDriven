using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickUp : MonoBehaviour
{
    [SerializeField] public PickUpData data;    
    bool collected;

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
        Debug.Log($"[PickUp] Collected: {data?.displayName ?? name}");
        Destroy(gameObject); 
    }
}
