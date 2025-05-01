using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public Sprite[] pages; 
    private int currentPage = 0;

    public Button nextButton;
    public Button prevButton;
    public Button mainMenuButton;

    public Image displayImage; 

    void Start()
    {
   
        if (displayImage == null)
        {
            return;
        }

        if (pages == null || pages.Length == 0)
        {
            return;
        }

    
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(NextPage);
        }
        else
        {
        }

        if (prevButton != null)
        {
            prevButton.onClick.AddListener(PrevPage);
        }
        else
        {
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        }
        else
        {
            Debug.LogError("Main Menu Button is not assigned!");
        }

    
        UpdatePage();
    }

    void UpdatePage()
    {
        if (displayImage != null && currentPage >= 0 && currentPage < pages.Length)
        {
            displayImage.sprite = pages[currentPage]; 
        }

        if (prevButton != null)
        {
            prevButton.interactable = currentPage > 0;
        }

        if (nextButton != null)
        {
            nextButton.interactable = currentPage < pages.Length - 1;
        }
    }

    void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            UpdatePage();
        }
    }

    void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePage();
        }
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("BeginScene");
    }
}