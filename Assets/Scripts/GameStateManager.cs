using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : SingletonBase<GameStateManager> {

    public Canvas PauseScreen;
    public Canvas GameOverScreen;
    public Canvas PlayScreen;
    public Canvas WinScreen;
    public Canvas TitleScreen;

    public AudioSource Song;

    public void Pause()
    {
        PauseScreen.enabled = true;
        Time.timeScale = 0;
        Song.Pause();
    }

    public void UnPause()
    {
        PauseScreen.enabled = false;
        Time.timeScale = 1;
        Song.UnPause();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        PlayScreen.enabled = false;
        GameOverScreen.enabled = true;
        Song.Stop();
    }

    public void Restart()
    {
        TitleScreen.enabled = false;
        PlayScreen.enabled = true;
        GameOverScreen.enabled = false;
        Song.Play();
    }
    
    public void Win()
    {
        PlayScreen.enabled = false;
        WinScreen.enabled = true;
        Song.Stop();
    }

    public void LoadScene_Single(string sceneToChange)
    {
        // Load Scene
        SceneManager.LoadScene(sceneToChange, LoadSceneMode.Single);
    }

    public void LoadScene_Additive(string sceneToChange)
    {
        SceneManager.LoadScene(sceneToChange, LoadSceneMode.Additive);
    }

    public void UnloadScene(string sceneToUnload)
    {
        SceneManager.UnloadScene(sceneToUnload);
    }
}
