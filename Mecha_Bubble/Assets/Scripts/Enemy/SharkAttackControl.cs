using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkAttackControl : MonoBehaviour
{
    // Parte do c�digo feita pelo gamedev Edson Miguel (CSJ Academy):
    [Header("Vari�veis do Usu�rio")]
    [SerializeField]
    SharkMoves sharkMoves;

    [Header("Vari�veis da Unity")]
    [SerializeField]
    Animator anim;

    [Header("Detec��o do Player")]
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
