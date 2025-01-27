using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{

    private static CreditsController m_instance;
    public static CreditsController Instance => m_instance;

    [SerializeField] private CanvasGroup _canvas;
    [SerializeField] private CanvasGroup _subCanvas;

    private void Awake()
    {
        _canvas.alpha = 0f;
        _canvas.gameObject.SetActive(false);

        _subCanvas.alpha = 0f;
        _subCanvas.gameObject.SetActive(false);

        if (m_instance == null)
            m_instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowCredits()
    {
        StartCoroutine(ShowCreditsCoroutine());
    }

    private IEnumerator ShowCreditsCoroutine(float duration = 1f)
    {
        _canvas.gameObject.SetActive(true);
        _canvas.alpha = 0f;
        _subCanvas.gameObject.SetActive(true);
        _subCanvas.alpha = 1f;

        float timer = 0f;

        while (timer <= duration)
        {
            timer += Time.deltaTime;
            var proportion = Mathf.Lerp(0f, 1f, timer / duration);

            _canvas.alpha = proportion;

            yield return null;
        }

        _canvas.alpha = 1f;

        yield return new WaitForSeconds(6f);

        timer = 0f;

        while (timer <= duration)
        {
            timer += Time.deltaTime;
            var proportion = Mathf.Lerp(1f, 0f, timer / duration);

            _subCanvas.alpha = proportion;

            yield return null;
        }

        _subCanvas.alpha = 0f;

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Menu");
    }

}
