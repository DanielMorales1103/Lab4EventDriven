using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EndZone : MonoBehaviour
{
    [SerializeField] private float quitDelay = 0f; 

    private void Reset()
    {
        var col = GetComponent<Collider>();
        if (col) col.isTrigger = true;

        var rb = GetComponent<Rigidbody>();
        if (!rb) rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (quitDelay > 0f)
            StartCoroutine(QuitAfter(quitDelay));
        else
            QuitNow();
    }

    private System.Collections.IEnumerator QuitAfter(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        QuitNow();
    }

    private void QuitNow()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit(); 
        #endif
    }
}
