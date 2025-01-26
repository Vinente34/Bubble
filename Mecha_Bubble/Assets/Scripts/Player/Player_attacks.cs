using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_attacks : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject prefab;
    public Animator anim;
    public int attackPower = 0;
    public bool attackedNow = false;
    public bool shootedNow = false;
    public float timeBeforeAttack = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        Instantiate(prefab, shootingPoint.position, shootingPoint.rotation);
        shootedNow = true;
        attackedNow = true;
        yield return new WaitForSeconds(timeBeforeAttack/2);
        attackedNow = false;
        yield return new WaitForSeconds(timeBeforeAttack/2);
        
        shootedNow = false;
    }
}
