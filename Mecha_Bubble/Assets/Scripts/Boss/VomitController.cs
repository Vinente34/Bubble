using System.Collections;
using UnityEngine;

public class VomitController : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D _rigibody2D;
    [SerializeField] private Animator _animator;

    private bool m_impacted = false;

    public void AddForce(Vector2 force)
    {
        _rigibody2D.AddForce(force, ForceMode2D.Impulse);
    }

    public void DisableGravity()
    {
        _rigibody2D.gravityScale = 0f;
        _rigibody2D.bodyType = RigidbodyType2D.Static;
    }

    public void EnableGravity()
    {
        _rigibody2D.bodyType = RigidbodyType2D.Dynamic;
        _rigibody2D.gravityScale = 2f;
    }

    public void Impact()
    {
        if (m_impacted)
            return;

        m_impacted = true;
        _animator.Play("DropVomit_Impact");

        StartCoroutine(AfterImpact());
    }

    private IEnumerator AfterImpact()
    {
        yield return new WaitForSeconds(0.5f);

        DisableGravity();
    }

}
