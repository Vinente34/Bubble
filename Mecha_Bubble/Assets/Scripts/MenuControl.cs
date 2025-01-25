using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Debug.Log("Saiu do Jogo");
        Application.Quit();
    }
}
