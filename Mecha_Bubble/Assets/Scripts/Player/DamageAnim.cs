using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAnim : MonoBehaviour
{
    public LifeControl playerControl;
    public Animator anim;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Enemy"))
        {
            anim.Play("Damage");
        }
    }
}