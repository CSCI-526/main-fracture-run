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
        // 播放音效：创建一个临时对象播放，不依赖按钮自身
        if (clickSound != null)
        {
            GameObject temp = new GameObject("TempAudio");
            AudioSource tempSource = temp.AddComponent<AudioSource>();
            tempSource.clip = clickSound;
            tempSource.volume = 1f;
            tempSource.Play();
            Destroy(temp, clickSound.length);  // 播完后销毁
        }

        // 延迟跳转场景
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
