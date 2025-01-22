using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_moviments : MonoBehaviour
{
      public Rigidbody2D rig;
    public float speed = 0.0f;
    public float jumpforce = 0.0f;
    public BoxCollider2D box;
    public float castdistance = 0;
    public LayerMask groundlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float horizontalmoviment = Input.GetAxis("Horizontal");
        if (horizontalmoviment != 0)
        {
            rig.velocity = new Vector2(horizontalmoviment * speed, transform.position.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGround())
        {
            rig.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
        }
    }

    bool IsGround() {

        RaycastHit2D raycast = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.down, castdistance, groundlayer);
        Vector2 perpendicular = Vector2.Perpendicular(raycast.normal);

        Debug.DrawRay (raycast.point, raycast.normal, Color.yellow);

        return raycast.collider!= null && rig.velocity.y<0.1f ;

    }
}
