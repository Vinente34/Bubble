using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreeController : MonoBehaviour
{

    [SerializeField] private Image _image;
    [SerializeField] private float _duration;

    private IEnumerator Start()
    {

        _image.transform.localScale = Vector3.one * 0f;

        var timer = 0f;        

        while (timer < _duration)
        {
            timer += Time.deltaTime;
            float proportion = timer / _duration;
            var value = Mathf.Lerp(0f, 1f, proportion);
            _image.transform.localScale = Vector3.one * value;

            yield return null;
        }

        _image.transform.localScale = Vector3.one * 1f;

        yield return new WaitForSeconds(_duration * 4f);

        timer = 0f;

        while (timer <= _duration / 2f)
        {
            timer += Time.deltaTime;
            float proportion = timer / (_duration / 2f);
            var value = Mathf.Lerp(0f, 1f, proportion);
            
            _image.color = Color.Lerp(Color.white, Color.clear, value);

            yield return null;
        }

        yield return null;
        yield return new WaitForSeconds(_duration);

        SceneManager.LoadScene("Menu");
    }

}
