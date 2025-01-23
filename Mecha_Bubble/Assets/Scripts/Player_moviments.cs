using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Player_moviments : MonoBehaviour
{
    [Header("Physics2D")]
    public Rigidbody2D rig;
    public float xAxis;
    public float speed = 0.0f;
    public float jumpforce = 0.0f;
    [Header("Ground Detecting")]
    public Transform groundCheck;
    public bool isGround;
    public float radius;
    public LayerMask groundLayer;
    public bool isJumping;
    [Header("Animation")]
    public Animator anim;
    public bool isDashing = false;
    public float dashSpeed = 0.0f;
    public float dashTimeLeft = 0.0f;
    public float dashTime = 0.0f;
    public bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = xAxis != 0 ? new Vector3(0, xAxis > 0 ? 0 : 180, 0) : transform.eulerAngles;
        
        xAxis = Input.GetAxis("Horizontal") * speed;

        if (Input.GetKeyDown(KeyCode.P))
        {
            AllowDash();
        }

        if (isDashing)
        {
            dashTimeLeft -= Time.deltaTime;
            if (dashTimeLeft <= 0)
            {
                anim.SetInteger("animOption", 0);
                isDashing = false;
            }
        }

        isGround = Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGround)
        {
            isJumping = true;
        }

        if (xAxis < 0 && facingRight)
        {
            facingRight = false;
        }
        else if (xAxis > 0 && !facingRight)
        {
            facingRight = true;
        }
    }

    void FixedUpdate()
    {
        if (isDashing == false)
        {
            AllowHorizontalMove();
        }
        AllowJump();
    }

    void AllowHorizontalMove()
    {
        if (xAxis != 0)
        {
            rig.velocity = new Vector2(xAxis, rig.velocity.y);
            anim.SetInteger("animOption", 1);
        }
        else
        {
            anim.SetInteger("animOption", 0);
        }
    }

    void AllowJump()
    {
        if (isJumping)
        {
            isJumping = false;
            rig.velocity = Vector2.zero;
            rig.AddForce(new Vector3(0f, jumpforce, 0f), ForceMode2D.Impulse);
        }
    }

    public void AllowDash()
    {
        anim.SetInteger("animOption", 2);

        isDashing = true;
        dashTimeLeft = dashTime;

        if (!facingRight)
        {
            rig.velocity = new Vector3(-speed * dashSpeed, rig.velocity.y, 0f);
        }
        else
        {
            rig.velocity = new Vector3(speed * dashSpeed, rig.velocity.y, 0f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, radius);
    }
}
