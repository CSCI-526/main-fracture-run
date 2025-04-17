using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Escape))
    //     {
    //         if (isPaused)
    //             Resume();
    //         else
    //             Pause();
    //     }
    // }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("BeginScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("退出游戏");
    }
}
