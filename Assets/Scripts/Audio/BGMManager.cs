using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
    private AudioSource audioSource;

    private static BGMManager instance;

    void Awake()
    {
        // 保证只有一个 BGMManager 存在
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.volume = 0.6f;
        audioSource.Play();
    }
}
