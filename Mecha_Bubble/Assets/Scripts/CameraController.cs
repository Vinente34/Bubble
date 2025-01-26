using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    private static CameraController m_instance;
    public static CameraController Instance => m_instance;

    [SerializeField] private SpriteRenderer _warningSign;

    private void Awake()
    {
        if (m_instance == null)
            m_instance = this;
        else
            Destroy(gameObject);

        _warningSign.gameObject.SetActive(false);
    }

    public void ShowWarningSign(float yPosition)
    {
        var position = _warningSign.transform.position;
        position.y = yPosition;
        _warningSign.transform.position = position;

        StartCoroutine(Flickering());
    }

    IEnumerator Flickering()
    {
        float flickeringTime = 2f;

        _warningSign.gameObject.SetActive(true);

        _warningSign.color = Color.white;
        yield return new WaitForSeconds(flickeringTime / 10);
        _warningSign.color = Color.clear;
        yield return new WaitForSeconds(flickeringTime / 10);
        _warningSign.color = Color.white;
        yield return new WaitForSeconds(flickeringTime / 10);
        _warningSign.color = Color.clear;
        yield return new WaitForSeconds(flickeringTime / 10);
        _warningSign.color = Color.white;
        yield return new WaitForSeconds(flickeringTime / 10);
        _warningSign.color = Color.clear;
        yield return new WaitForSeconds(flickeringTime / 10);
        _warningSign.color = Color.white;
        yield return new WaitForSeconds(flickeringTime / 10);
        _warningSign.color = Color.clear;
        yield return new WaitForSeconds(flickeringTime / 10);
        _warningSign.color = Color.white;

        _warningSign.gameObject.SetActive(false);
    }

}
