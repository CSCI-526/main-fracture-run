using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); 
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene("TutorialScene"); 
    }

    public void StartIntroduction()
    {
        SceneManager.LoadScene("IntroScene");
    }

}
