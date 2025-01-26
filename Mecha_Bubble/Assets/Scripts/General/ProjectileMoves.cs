using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMoves : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float timeOfDeath;

    // Esta vari�vel � essencial, pois carrega consigo o poder de ataque do player, recolhido anteriomente
    // pelo script PlayerAttacks:
    public int attackPower;

    // Update is called once per frame
    void Update()
    {
        ShotDirection();
        ShotLife();
    }

    // Movimenta��o que faz o game object se movimentar para direita:
    private void ShotDirection()
    {
        Vector2 position = this.transform.position;
        Vector2 direction = this.transform.right; // Obter a dire��o do objeto
        position += direction * speed * Time.deltaTime; // Adicionar � posi��o na dire��o do objeto
        this.transform.position = position;
    }

    // Destr�i o game object no qual este script est� atrelado depois de um certo tempo:
    private void ShotLife()
    {

        StartCoroutine(BubbleCoroutine());
    }

    IEnumerator BubbleCoroutine()
    {
        yield return new WaitForSeconds(timeOfDeath);
        anim.SetBool("imDead", true);
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se a bala encontrar um game object com a tag Player, este game object se destruir�:
        if (collision.gameObject.CompareTag("Enemy"))
        {
            anim.SetBool("imDead", true);
        }
    }
}
