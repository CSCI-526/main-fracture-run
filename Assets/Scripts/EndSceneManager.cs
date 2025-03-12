using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene"); // 重新加载游戏场景
    }

     public void StartTutorial()
    {
        SceneManager.LoadScene("TutorialScene"); 
    }

    public void restart()
    {
        SceneManager.LoadScene("BeginScene");
    }
}
