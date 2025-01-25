using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_moviments : MonoBehaviour
{
    [Header("Physics2D")]
    public Rigidbody2D rig;
    public float xAxis;
    public float speed = 0.0f;
    public float jumpforce = 0.0f;
    public bool facingRight = true;
    [Header("Ground Detecting")]
    public Transform groundCheck;
    public bool isGround;
    public float radius;
    public LayerMask groundLayer;
    public bool isJumping;
    [Header("Dashing")]
    public bool isDashing = false;
    public float dashSpeed = 0.0f;
    public float dashTimeLeft = 0.0f;
    public float dashTime = 0.0f;
    [Header("Animation")]
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Dashing Mechanics:
        if (Input.GetKeyDown(KeyCode.Space))
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

        // Jumping Mechanics:
        isGround = Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGround)
        {
            isJumping = true;
            anim.SetInteger("animOption", 3);
        }

        if (isGround == false && isDashing == true)
        {
            anim.SetInteger("animOption", 2);
        }
        else if (isGround == false)
        {
            anim.SetInteger("animOption", 0);
        }

        AllowFlip();
    }

    void FixedUpdate()
    {
        // Walking Mechanics:
        xAxis = Input.GetAxis("Horizontal") * speed;

        if (isDashing == false && isJumping == false)
        {
            AllowHorizontalMove();
        }

        // Jumping Mechanics:
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

    void AllowFlip()
    {
        transform.eulerAngles = xAxis != 0 ? new Vector3(0, xAxis > 0 ? 0 : 180, 0) : transform.eulerAngles;

        if (xAxis < 0 && facingRight)
        {
            facingRight = false;
        }
        else if (xAxis > 0 && !facingRight)
        {
            facingRight = true;
        }
    }

    void AllowJump()
    {
        if (isJumping == true && isDashing == false)
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
