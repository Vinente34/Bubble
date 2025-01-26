using System.Collections;
using UnityEngine;

public class LifeControl : MonoBehaviour
{
    public Player_moviments playerMoviments;
    public SpriteRenderer spriteRenderer;
    public int lifePoints = 0;
    public bool noDamageFlag = false;
    public Color damageColor = Color.white;
    public float flickeringTime = 0;
    public bool isDead = false;

    // Update is called once per frame
    void Update()
    {
        if (lifePoints <= 0)
        {
            
            isDead = true;

            if (gameObject.CompareTag("Player"))
            {
                playerMoviments.anim.SetInteger("animOption", 10);
            }
            else
            {
                //spriteRenderer.color = damageColor; // Temporary death effect...
                gameObject.SetActive(false);
            }  
        }
    }

    void TakeDamage()
    {
        Debug.Log(noDamageFlag);
        Debug.Log(lifePoints);
        if (noDamageFlag == false && lifePoints > 0)
        {
            Debug.Log("A2");
            lifePoints--;
            StartCoroutine(Flickering());
        }

        if (gameObject.CompareTag("Player"))
            MenuControl.Instance.UpdateLifes(lifePoints);
    }

    IEnumerator Flickering()
    {
        noDamageFlag = true;

        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(flickeringTime / 5);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(flickeringTime / 5);
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(flickeringTime / 5);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(flickeringTime / 5);
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(flickeringTime / 5);
        spriteRenderer.color = Color.white;

        noDamageFlag = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.CompareTag("Enemy") && collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("A1");
            TakeDamage();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }

        if (this.gameObject.CompareTag("Enemy") && collision.gameObject.CompareTag("Player"))
        {
            playerMoviments = collision.GetComponent<Player_moviments>();
            playerMoviments.Knockback(collision.transform.position);
        }
    }
}
