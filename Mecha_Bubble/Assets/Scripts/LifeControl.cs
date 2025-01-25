using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeControl : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public int lifePoints = 0;
    public bool noDamageFlag = false;
    public Color damageColor = Color.white;
    public float flickeringTime = 0;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lifePoints <= 0)
        {
            spriteRenderer.color = damageColor; // Temporary death effect...
            isDead = true;
        }
    }

    void TakeDamage()
    {
        if (noDamageFlag == false && lifePoints > 0)
        {
            lifePoints--;
            StartCoroutine(Flickering());
        }
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Enemy") && noDamageFlag == false)
        {
            TakeDamage();
        }
    }
}
