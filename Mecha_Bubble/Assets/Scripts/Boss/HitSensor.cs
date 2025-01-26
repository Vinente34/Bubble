using UnityEngine;

public class HitSensor : MonoBehaviour
{

    public BossController _character;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TakeDamage(1);
    }

    public void TakeDamage(int damage)
    {
        _character.TakeDamage(damage);
    }

}
