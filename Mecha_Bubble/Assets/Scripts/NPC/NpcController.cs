using UnityEngine;

public class NpcController : MonoBehaviour
{

    [SerializeField] private Animator _animator;

    private bool m_saved = false;
    public bool Saved => m_saved;

    public void ClickOnNpc()
    {
        m_saved = true;

        _animator.Play("Free");
    }

}
