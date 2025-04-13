using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(AudioSource))]
public class ButtonSoundAndScene : MonoBehaviour
{
    public AudioClip clickSound;

    public string sceneToLoad = "";  
    public float delayBeforeSceneLoad = 1.25f;

    private AudioSource audioSource;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            StartCoroutine(LoadSceneAfterDelay());
        }
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeSceneLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
