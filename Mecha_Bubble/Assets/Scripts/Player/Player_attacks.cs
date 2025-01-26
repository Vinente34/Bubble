using System.Collections;
using UnityEngine;

public class Player_attacks : MonoBehaviour
{

    public MeleeSensor sensor;
    public Transform sensorPosition;
    public Transform shootingPoint;
    public GameObject meleeBlock;
    public ProjectileMoves prefab;
    public Animator anim;
    public int attackPower = 0;
    public bool attackedNow = false;
    public bool shootedNow = false;
    public float timeBeforeAttack = 0;
    public float projectileScale = 1f;

    private MeleeSensor m_sensor;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && shootedNow == false)
        {
            anim.SetInteger("animOption", 4);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetInteger("animOption", 7);
        }
    }

    public void ShootAttack()
    {
        StartCoroutine(ShootAttackProcess());
    }

    public void MeleeAttackOn()
    {
        //meleeBlock.SetActive(true);
        m_sensor = Instantiate(sensor, sensorPosition.position, Quaternion.identity);
    }

    public void MeleeAttackOff()
    {
        //meleeBlock.SetActive(false);
        m_sensor.SelfDestroy();
    }

    IEnumerator ShootAttackProcess()
    {
        var projectile = Instantiate(prefab, shootingPoint.position, shootingPoint.rotation);
        projectile.SetupScale(projectileScale);
        
        shootedNow = true;
        attackedNow = true;
        yield return new WaitForSeconds(timeBeforeAttack/2);
        attackedNow = false;
        yield return new WaitForSeconds(timeBeforeAttack/2);
        
        shootedNow = false;
    }
}
