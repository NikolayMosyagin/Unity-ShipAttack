using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour {

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnStartNewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("new_game");
    }

    public void OnSettings()
    {

    }
}
