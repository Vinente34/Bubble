using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private Rigidbody2D rig;

    public float speed;
    public bool facingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = new Vector2(speed * Time.deltaTime, rig.velocity.y);

        transform.position += speed * Time.deltaTime * Vector3.right;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("FlipPoint"))
        {
            speed *= -1;
        }
        transform.eulerAngles = speed != 0 ? new Vector3(0, -speed > 0 ? 0 : 180, 0) : transform.eulerAngles;

        if (speed < 0 && facingRight)
        {
            facingRight = false;
        }
        else if (speed > 0 && !facingRight)
        {
            facingRight = true;
        }
    }
}
