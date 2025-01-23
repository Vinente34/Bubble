using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Player_moviments : MonoBehaviour
{
    [Header("Physics2S")]
    public Rigidbody2D rig;
    public float horizontalmoviment;
    public float speed = 0.0f;
    public float jumpforce = 0.0f;
    [Header("Ground Detecting")]
    public BoxCollider2D box;
    public float castdistance = 0;
    public LayerMask groundlayer;
    [Header("Animation")]
    public Animator anim;
    public bool dashButton = false;
    public float dashSpeed = 0.0f;
    public bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = horizontalmoviment != 0 ? new Vector3(0, horizontalmoviment > 0 ? 0 : 180, 0) : transform.eulerAngles;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DashNow();
        }
    }

    void FixedUpdate()
    {
        horizontalmoviment = Input.GetAxis("Horizontal");
        if (horizontalmoviment != 0)
        {
            rig.velocity = new Vector2(horizontalmoviment * speed, transform.position.y);
            anim.SetInteger("animOption", 1);
        }
        else
        {
            anim.SetInteger("animOption", 0);
        }

        if (Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.UpArrow) && IsGround())
        {
            rig.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
        }
    }

    bool IsGround() 
    {

        RaycastHit2D raycast = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.down, castdistance, groundlayer);
        Vector2 perpendicular = Vector2.Perpendicular(raycast.normal);

        Debug.DrawRay (raycast.point, raycast.normal, Color.yellow);

        return raycast.collider!= null && rig.velocity.y < 0.1f;
    }

    public void DashNow()
    {
        anim.SetInteger("animOption", 2);

        if(Math.Abs(transform.localRotation.y) == 180)
        {
            rig.velocity = new Vector2(-speed, transform.position.y);
        }
        else
        {
            rig.velocity = new Vector2(speed, transform.position.y);
        }
    }

    public void stopDashAnim()
    {
        anim.SetInteger("animOption", 0);
    }
}
