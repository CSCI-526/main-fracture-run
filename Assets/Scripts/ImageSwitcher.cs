using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageSwitcher : MonoBehaviour
{
    public Image image;
    public Sprite secondImage;
    public Sprite thirdImage; 
    public Sprite fourthImage;
    private float switchDelay = 12f;
    private float disappearDelay = 14f;
    private float disappearDelayDoor = 4f;
    private float disappearDelayUltimateInstruction = 7f;
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
        image.sprite = fourthImage;

        yield return new WaitForSeconds(disappearDelayUltimateInstruction);
        image.gameObject.SetActive(false); 
    }
}