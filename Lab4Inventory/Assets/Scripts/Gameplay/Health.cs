using UnityEngine;

public struct HealthPayload
{
    public int current;
    public int max;
    public HealthPayload(int c, int m) { current = c; max = m; }
}

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int startHealthOverride = -1;

    public int Current { get; private set; }
    public int Max => maxHealth;

    private void Awake()
    {
        Current = (startHealthOverride >= 0)
            ? Mathf.Clamp(startHealthOverride, 0, maxHealth)
            : maxHealth;

        EventBus.Publish<HealthPayload>(EventIds.HealthUpdated, new HealthPayload(Current, Max));
    }

    private void OnEnable()
    {
        EventBus.Subscribe<int>(EventIds.HealRequested, OnHeal);
        EventBus.Subscribe<int>(EventIds.DamageRequested, OnDamage);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<int>(EventIds.HealRequested, OnHeal);
        EventBus.Unsubscribe<int>(EventIds.DamageRequested, OnDamage);
    }

    private void OnHeal(int amount)
    {
        if (amount <= 0) return;

        int before = Current;
        Current = Mathf.Min(Max, Current + amount);
        if (Current != before)
        {
            EventBus.Publish<HealthPayload>(EventIds.HealthUpdated, new HealthPayload(Current, Max));
        }
    }

    private void OnDamage(int amount)
    {
        if (amount <= 0) return;
        int before = Current;
        Current = Mathf.Max(0, Current - amount);
        if (Current != before)
        {
            EventBus.Publish<HealthPayload>(EventIds.HealthUpdated, new HealthPayload(Current, Max));
        }
    }
}
