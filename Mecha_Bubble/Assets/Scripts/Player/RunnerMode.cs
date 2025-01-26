using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerMode : MonoBehaviour
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
    [Header("Animation")]
    public Animator anim;
    public Player_attacks playerAttacks;

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGround)
        {
            isJumping = true;
            anim.SetInteger("animOption", 3);
        }
    }

    void FixedUpdate()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);
        if (isJumping == false)
        {
            anim.SetInteger("animOption", 1);
        }

        // Jumping Mechanics:
        isGround = Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);

        if (isJumping == true)
        {
            isJumping = false;
            rig.velocity = Vector2.zero;
            rig.AddForce(new Vector3(0f, jumpforce, 0f), ForceMode2D.Impulse);
        }
    }
}