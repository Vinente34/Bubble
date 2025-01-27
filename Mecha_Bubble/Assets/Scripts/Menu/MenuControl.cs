using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{

    private static MenuControl m_instance;
    public static MenuControl Instance => m_instance;

    [SerializeField] private Image[] _lifes;

    public Animator anim;

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

    public void MakePlayerJump()
    {
        StartCoroutine(MakePlayerJumpProccess());
    }

    IEnumerator MakePlayerJumpProccess()
    {
        anim.SetBool("GameStarted", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);
    }

    public void BackToBoss()
    {
        Debug.Log("Back To Boss");
        SceneManager.LoadScene(4);
    }

    public void BackToMenu()
    {
        Debug.Log("Back To Menu");
        SceneManager.LoadScene(1);
    }

    public void toCredits()
    {
        SceneManager.LoadScene(5);
    }

    public void GameStarts()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif

        Application.Quit();
    }
}
