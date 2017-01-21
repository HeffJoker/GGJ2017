using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : SingletonBase<GameStateManager> {

    public Canvas PauseScreen;


    public void Pause()
    {
        PauseScreen.enabled = true;
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        PauseScreen.enabled = false;
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
