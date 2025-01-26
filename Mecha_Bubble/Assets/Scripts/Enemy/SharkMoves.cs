using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMoves : MonoBehaviour
{
    [Header("Variáveis da Unity:")]
    [SerializeField]
    private GameObject playerDetector;
    [SerializeField]
    private Animator anim;
    private Vector2 pos; // Esta variável guardará a posição atual do inimigo a utilizando...
    private Transform empty;
    public Transform shootingPoint;


    [Header("Variáveis Numéricas:")]
    // Contém a velocidade do personagem:
    [SerializeField]
    private float speed;

    [Header("Detecção do Player")]
    [SerializeField]
    public Transform player;
    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private float castRadius = 0;
    [SerializeField]
    private float castDistance = 0;
    [SerializeField]
    private LayerMask playerLayer;

    [Header("Variáveis de Ataque")]
    [SerializeField]
    private float beforeAttackTime = 0;
    [SerializeField]
    private bool isAttacking = false;
    [SerializeField]
    private float delayTimer = 0;
    [SerializeField]
    public bool nearFlag = false;
    [SerializeField]
    private GameObject prefab;

    void Start()
    {
        pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Se o player não estiver no perímetro:
        if (Physics2D.CircleCast(this.transform.position, castRadius, transform.up, castDistance, playerLayer) == false)
        {
            delayTimer = beforeAttackTime;

            if (isAttacking == true)
            {
                AllowFlip3();
                this.transform.position = Vector2.MoveTowards(transform.position, startPoint.position, speed * Time.deltaTime);

                if (this.transform.position == startPoint.position)
                {
                    pos = this.transform.position;
                    isAttacking = false;
                }
            }
            else if (isAttacking == false)
            {
                FlyLeft();
                AllowFlip1();
                isAttacking = false;
            }
        }
        // Se o player for detectado no perímetro:
        else
        {
            AllowFlip2();
            isAttacking = true;

            if (speed < 0)
            {
                speed *= -1;
            }

            if (delayTimer >= 0)
            {
                delayTimer -= Time.deltaTime;
            }
            else
            {
                //if (nearFlag == false)
                //{
                    Vector2 playerPos = player.position;
                    //playerPos.y += 1.06f;

                    this.transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
                //}
            }
        }
    }

    public void FlyLeft()
    {
        pos.x -= speed * Time.deltaTime;
        this.transform.position = pos;
    }

    public void AllowFlip1()
    {
        // Flipping:
        if (speed > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (speed < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    public void AllowFlip2()
    {
        // Flipping:
        if (transform.position.x > player.position.x)
        {
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (transform.position.x < player.position.x)
        {
            //transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    public void AllowFlip3()
    {
        // Flipping:
        if (transform.position.x > startPoint.position.x)
        {
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (transform.position.x < startPoint.position.x)
        {
            //transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    public void setAttackAnimationOff()
    {
        this.transform.position = empty.position;
        //anim.SetBool("onAttack", false);
        Debug.Log("Tchau!");
    }

    public void setAttackAnimationOn()
    {
        //anim.SetBool("onAttack", true);
        Debug.Log("Oi!");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position - transform.up * castDistance, castRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FlipPoint") && isAttacking == false)
        {
            speed *= -1;
        }
    }
}
