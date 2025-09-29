using System.Collections;
using UnityEngine;
using StarterAssets; 

public class SpeedBuffTP : MonoBehaviour
{
    [SerializeField] private ThirdPersonController controller;
    [SerializeField] private float multiplier = 2f;            

    private float _baseMove;
    private float _baseSprint;
    private Coroutine _buffCoro;

    private void Awake()
    {
        if (!controller) controller = GetComponent<ThirdPersonController>();
        if (controller)
        {
            _baseMove = controller.MoveSpeed;
            _baseSprint = controller.SprintSpeed;
        }
    }

    private void OnEnable()
    {
        EventBus.Subscribe<float>(EventIds.SpeedBuffRequested, OnBuffRequested);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<float>(EventIds.SpeedBuffRequested, OnBuffRequested);
        ResetSpeeds();
    }

    private void OnBuffRequested(float seconds)
    {
        if (!controller) return;

        if (_buffCoro != null) StopCoroutine(_buffCoro);

        controller.MoveSpeed = _baseMove * multiplier;
        controller.SprintSpeed = _baseSprint * multiplier;

        _buffCoro = StartCoroutine(BuffTimer(seconds));
    }

    private IEnumerator BuffTimer(float seconds)
    {
        float t = 0f;
        while (t < seconds)
        {
            t += Time.deltaTime;
            yield return null;
        }
        ResetSpeeds();
        _buffCoro = null;
    }

    private void ResetSpeeds()
    {
        if (!controller) return;
        controller.MoveSpeed = _baseMove;
        controller.SprintSpeed = _baseSprint;
    }
}
