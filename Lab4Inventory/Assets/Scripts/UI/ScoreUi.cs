using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TextMeshProUGUI label;

    private void OnEnable()
    {
        EventBus.Subscribe<int>(EventIds.ScoreUpdated, OnScoreUpdated);
        if (label && scoreManager) label.text = $"Score: {scoreManager.Score}";
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<int>(EventIds.ScoreUpdated, OnScoreUpdated);
    }

    private void OnScoreUpdated(int newScore)
    {
        if (label) label.text = $"Score: {newScore}";
    }
}
