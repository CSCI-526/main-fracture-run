using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartTipManager : MonoBehaviour
{
    public TMP_Text startTipText;  
    public float displayDuration = 10f; 

    void Start()
    {
        ShowStartTips();
    }

    void ShowStartTips()
    {
        startTipText.gameObject.SetActive(true);
        Invoke("HideStartTips", displayDuration); 
    }

    void HideStartTips()
    {
        startTipText.gameObject.SetActive(false); 
    }
}