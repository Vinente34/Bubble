using UnityEngine;

public class NpcDetector : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out NpcController npcController))
            return;

        if (npcController.Saved)
            return;

        GameObject.FindGameObjectWithTag("Player").GetComponent<RunnerMode>().Death(false);
    }

}
