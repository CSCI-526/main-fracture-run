using UnityEngine;
using TMPro;

public class TextSizeManager : MonoBehaviour
{
    public float globalFontSize = 40f;

    void Start()
    {
        
        TextMeshProUGUI[] texts = FindObjectsOfType<TextMeshProUGUI>();

        foreach (TextMeshProUGUI text in texts)
        {
            if(text.gameObject.name == "TipText") {
                continue;
            }
            text.fontSize = globalFontSize;
            text.alignment = TextAlignmentOptions.Center;
        }
    }
}
