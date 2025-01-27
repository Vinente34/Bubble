using UnityEngine;

public class NpcController : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _handTutorial;

    private bool m_saved = false;
    public bool Saved => m_saved;

    public void ClickOnNpc()
    {
        m_saved = true;

        if (_handTutorial != null)
            Destroy(_handTutorial);

        _animator.Play("Free");
    }

}
