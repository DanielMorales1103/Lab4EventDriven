using UnityEngine;

public class DoorSensor : MonoBehaviour
{
    private DoorController _door;

    private void Awake()
    {
        _door = GetComponentInParent<DoorController>();

        var col = GetComponent<Collider>();
        if (col) col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_door) return;
        if (other.CompareTag("Player"))
        {
            _door.TryOpen();
        }
    }
}
