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

        var index = Random.Range(0, 3) <= 1 ? 0 : 1;
        var sides = new Vector2[2] { new(-1.5f, 0f), new(1f, 3.5f) };
        var yRandom = Random.Range(sides[index].x, sides[index].y);

        var position = (Vector2) transform.position + Vector2.right * 30f;
        position.y = yRandom;

        CameraController.Instance.ShowWarningSign(yRandom);

        Instantiate(_mudPrefab, position, Quaternion.identity);

        m_throwed = true;

        Destroy(gameObject);
    }

}
