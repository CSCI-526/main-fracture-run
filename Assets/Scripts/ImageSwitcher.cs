using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageSwitcher : MonoBehaviour
{
    public Image image; 
    public Sprite secondImage;
    private float switchDelay = 8f; 
    private float disappearDelay = 5f; 

    void Start()
    {
        StartCoroutine(SwitchImage());
    }

    private IEnumerator SwitchImage()
    {
        yield return new WaitForSeconds(switchDelay); 
        image.sprite = secondImage; 

        yield return new WaitForSeconds(disappearDelay); 
        image.gameObject.SetActive(false); 
    }
}
