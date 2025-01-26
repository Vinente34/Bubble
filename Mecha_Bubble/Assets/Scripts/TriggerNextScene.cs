using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerNextScene : MonoBehaviour
{

    [SerializeField] private string _nextSceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(_nextSceneName);
    }

}
