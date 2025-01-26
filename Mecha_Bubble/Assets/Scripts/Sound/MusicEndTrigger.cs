using UnityEngine;

public class MusicEndTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        MusicController.Instance.PlayEnd();

        Destroy(gameObject);
    }

}
