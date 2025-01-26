using UnityEngine;

public class Mud : MonoBehaviour
{

    [SerializeField] private float _speed;

    [SerializeField] private float _timeToSelfDestruction;

    private void Awake()
    {
        Invoke(nameof(SelfDestruction), _timeToSelfDestruction);
    }

    private void Update()
    {
        transform.position += _speed * Time.deltaTime * Vector3.left;
    }

    private void SelfDestruction()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        other.GetComponent<RunnerMode>().Death();

        SelfDestruction();
    }

}
