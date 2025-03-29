using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageSwitcher : MonoBehaviour
{
    public Image image;
    public Sprite secondImage;
    public Sprite thirdImage; 
    private float switchDelay = 19f;
    private float disappearDelay = 5f;
    private float disappearDelayDoor = 4f;
    void Start()
    {
        StartCoroutine(SwitchImage());
    }

    private IEnumerator SwitchImage()
    {
       
        yield return new WaitForSeconds(switchDelay);
        image.sprite = secondImage;

      
        yield return new WaitForSeconds(disappearDelay); 
        image.sprite = thirdImage;

      
        yield return new WaitForSeconds(disappearDelayDoor);
        image.gameObject.SetActive(false); 
    }
}