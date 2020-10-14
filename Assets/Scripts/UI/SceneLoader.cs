using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject pauseMenu;

    public void ReloadGame()
    {
        resetTimeScale();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Scene Reload");
    }

    public void Pause()
    {
        Debug.Log("Pause");
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        resetTimeScale();
        pauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        resetTimeScale();
        SceneManager.LoadScene(0);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(6);
    }

    public void LoadNextScene()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void resetTimeScale()
    {
        Time.timeScale = 1;
    }
}
