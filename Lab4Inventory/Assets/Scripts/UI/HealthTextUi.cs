using TMPro;
using UnityEngine;

public class HealthTextUI : MonoBehaviour
{
    [SerializeField] private Health playerHealth;       
    [SerializeField] private TextMeshProUGUI label;     

    private void OnEnable()
    {
        EventBus.Subscribe<HealthPayload>(EventIds.HealthUpdated, OnHealthUpdated);
        if (playerHealth && label)
            label.text = $"HP: {playerHealth.Current}/{playerHealth.Max}";
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<HealthPayload>(EventIds.HealthUpdated, OnHealthUpdated);
    }

    private void OnHealthUpdated(HealthPayload p)
    {
        if (label) label.text = $"HP: {p.current}/{p.max}";
    }
}
