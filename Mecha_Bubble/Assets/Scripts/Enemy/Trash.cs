using UnityEngine;

public class Trash : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        other.GetComponent<RunnerMode>().Death();
    }

}
