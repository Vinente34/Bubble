using UnityEngine;

public class MusicController : MonoBehaviour
{

    private static MusicController m_instance;
    public static MusicController Instance => m_instance;

    [SerializeField] private AudioSource _audioSourceBegin;
    [SerializeField] private AudioSource _audioSourceLoop;
    [SerializeField] private AudioSource _audioSourceEnd;

    private bool m_loop = false;

    private void Awake()
    {
        if (m_instance == null)
        {
            PlayBegin();

            m_instance = this;
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (m_loop)
            return;

        if (!_audioSourceBegin.isPlaying)
        {
            m_loop = true;
            PlayLoop();
        }
    }

    private void PlayBegin()
    {
        _audioSourceBegin.Play();
        _audioSourceLoop.Stop();
        _audioSourceEnd.Stop();
    }

    private void PlayLoop()
    {
        _audioSourceBegin.Stop();
        _audioSourceLoop.Play();
        _audioSourceEnd.Stop();
    }

    public void PlayEnd()
    {
        _audioSourceBegin.Stop();
        _audioSourceLoop.Stop();
        _audioSourceEnd.Play();
    }

}
