using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField][Range(0f, 1f)] private float volume = 1f;

    private void Awake()
    {
        if (!source)
        {
            source = GetComponent<AudioSource>();
            if (!source) source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.spatialBlend = 0f; 
        }
    }

    private void OnEnable()
    {
        EventBus.Subscribe<AudioClip>(EventIds.PlaySfx, OnPlay);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<AudioClip>(EventIds.PlaySfx, OnPlay);
    }

    private void OnPlay(AudioClip clip)
    {
        if (!clip || !source) return;
        source.PlayOneShot(clip, volume);
    }
}
