using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkAttackControl : MonoBehaviour
{
    // Parte do código feita pelo gamedev Edson Miguel (CSJ Academy):
    [Header("Variáveis do Usuário")]
    [SerializeField]
    SharkMoves sharkMoves;

    [Header("Variáveis da Unity")]
    [SerializeField]
    Animator anim;

    [Header("Detecção do Player")]
    [SerializeField]
    private Vector2 boxCastSize;
    [SerializeField]
    private float castDistance = 0;
    [SerializeField]
    private LayerMask playerLayer;



    // Update is called once per frame
    void Update()
    {
        if (Physics2D.BoxCast(this.transform.position, boxCastSize, 0, transform.up, castDistance, playerLayer) == true)
        {
            sharkMoves.nearFlag = true;
        }
        else
        {
            sharkMoves.nearFlag = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxCastSize);
    }
}
