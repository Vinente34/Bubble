using UnityEngine;

public class MudTrigger : MonoBehaviour
{
    
    [SerializeField] private Mud _mudPrefab;

    private bool m_throwed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (m_throwed)
            return;

        if (!other.CompareTag("Player"))
            return;

        var yRandom = Random.Range(-1.5f, 3.5f);
        var position = (Vector2) transform.position + Vector2.right * 30f;
        position.y = yRandom;

        CameraController.Instance.ShowWarningSign(yRandom);

        Instantiate(_mudPrefab, position, Quaternion.identity);

        m_throwed = true;

        Destroy(gameObject);
    }

}
