using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DoorController : MonoBehaviour
{
    [SerializeField] private string doorKey = "Blue";
    [SerializeField] private float liftDistance = 3f;
    [SerializeField] private float liftDuration = 0.6f;

    private bool _hasKey;
    private bool _opened;
    private Vector3 _startPos;
    private Collider _solid;

    public bool IsOpen => _opened;

    public void ForceOpen()
    {
        if (_opened) return;
        _opened = true;
        if (_solid) _solid.enabled = false;
        Vector3 target = _startPos + Vector3.up * liftDistance;
        transform.position = target;
        StopAllCoroutines();
    }

    public void GrantKey(string key)
    {
        if (string.IsNullOrWhiteSpace(key)) return;
        if (string.Equals(key, doorKey, System.StringComparison.OrdinalIgnoreCase))
            _hasKey = true;
    }

    private void Awake()
    {
        _startPos = transform.position;
        _solid = GetComponent<Collider>();
        if (_solid) _solid.isTrigger = false; 

        var rb = GetComponent<Rigidbody>();
        if (!rb) rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

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
        if (data.kind != PickUpKind.Key) return;
        if (string.IsNullOrWhiteSpace(data.doorKey)) return;

        if (string.Equals(data.doorKey, doorKey, System.StringComparison.OrdinalIgnoreCase))
        {
            _hasKey = true;
        }
    }

    public void TryOpen()
    {
        if (_opened) return;
        if (!_hasKey)
        {
            return;
        }
        StartCoroutine(OpenRoutine());
    }

    private IEnumerator OpenRoutine()
    {
        _opened = true;
        if (_solid) _solid.enabled = false; 

        Vector3 target = _startPos + Vector3.up * liftDistance;
        float t = 0f;
        while (t < liftDuration)
        {
            t += Time.deltaTime;
            float k = Mathf.Clamp01(t / liftDuration);
            transform.position = Vector3.Lerp(_startPos, target, k);
            yield return null;
        }
        transform.position = target;
    }
}
