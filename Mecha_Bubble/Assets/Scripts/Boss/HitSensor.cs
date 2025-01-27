using UnityEngine;

public class HitSensor : MonoBehaviour
{

    public BossController _character;

    public void TakeDamage(int damage)
    {
        _character.TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Projectile"))
            return;

        if (other.TryGetComponent(out ProjectileMoves projectile))
            projectile.StartDestruction();

        TakeDamage(1);
    }

}
