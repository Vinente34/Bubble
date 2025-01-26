using System.Collections;
using UnityEngine;

public class Player_attacks : MonoBehaviour
{

    public Transform shootingPoint;
    public ProjectileMoves prefab;
    public Animator anim;
    public int attackPower = 0;
    public bool attackedNow = false;
    public bool shootedNow = false;
    public float timeBeforeAttack = 0;
    public float projectileScale = 1f;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && shootedNow == false)
        {
            anim.SetInteger("animOption", 4);
        }
    }

    public void ShootAttack()
    {
        StartCoroutine(ShootAttackProcess());
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
