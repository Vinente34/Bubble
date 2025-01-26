using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{

    private static MenuControl m_instance;
    public static MenuControl Instance => m_instance;

    [SerializeField] private Image[] _lifes;

    private void Awake()
    {
        if (m_instance == null)
            m_instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateLifes(int lifes)
    {
        for (int i = 0; i < _lifes.Length; i++)
        {
            Image life = _lifes[i];
            life.gameObject.SetActive(i < lifes);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GameStarts()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif

        Application.Quit();
    }
}
