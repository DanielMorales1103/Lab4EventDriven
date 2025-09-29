using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int Score { get; private set; }

    private void OnEnable()
    {
        EventBus.Subscribe<int>(EventIds.ScoreChanged, OnScoreDelta);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<int>(EventIds.ScoreChanged, OnScoreDelta);
    }

    private void OnScoreDelta(int delta)
    {
        Score += delta;
        if (Score < 0) Score = 0;
        EventBus.Publish<int>(EventIds.ScoreUpdated, Score);
    }
}
