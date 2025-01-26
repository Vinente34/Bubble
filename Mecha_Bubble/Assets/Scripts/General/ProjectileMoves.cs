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

    // Esta variável é essencial, pois carrega consigo o poder de ataque do player, recolhido anteriomente
    // pelo script PlayerAttacks:
    public int attackPower;

    // Update is called once per frame
    void Update()
    {
        ShotDirection();
        ShotLife();
    }

    // Movimentação que faz o game object se movimentar para direita:
    private void ShotDirection()
    {
        Vector2 position = this.transform.position;
        Vector2 direction = this.transform.right; // Obter a direção do objeto
        position += direction * speed * Time.deltaTime; // Adicionar à posição na direção do objeto
        this.transform.position = position;
    }

    // Destrói o game object no qual este script está atrelado depois de um certo tempo:
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
        // Se a bala encontrar um game object com a tag Player, este game object se destruirá:
        if (collision.gameObject.CompareTag("Enemy"))
        {
            anim.SetBool("imDead", true);
        }
    }
}
