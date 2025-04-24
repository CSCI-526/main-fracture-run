using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // 确保包括这个命名空间

public class EndSceneManager : MonoBehaviour
{
    public TMP_Text distanceText; // 引用距离文本组件

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "EndScene")
            distanceText.gameObject.SetActive(false);
        distanceText.fontSize = 80;
        //distanceText.text = "Distance: " + PlayerController.GameData.distanceTraveled.ToString("F2") + " m"; // 更新文本
        //distanceText.text = "Star: " + PlayerController.GameData.star_num.ToString();
        distanceText.text = " : " + PlayerController.GameData.star_num.ToString();
    }

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

        Debug.Log("Restart button clicked!");
        SceneManager.LoadScene("BeginScene");
    }
}