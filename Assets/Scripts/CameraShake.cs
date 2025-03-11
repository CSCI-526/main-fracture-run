using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera vcam; // Reference to the virtual camera
    private CinemachineBasicMultiChannelPerlin noise; // Noise for shaking
    public float shakeDuration = 0.5f; // How long the shake lasts
    public float shakeAmplitude = 1f; // Intensity of the shake
    public float shakeFrequency = 50f; // Speed of the shake

    private float shakeTimer = 0f; // Tracks remaining shake time

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>(); // Get the virtual camera
        if (vcam == null)
        {
            Debug.LogError("CameraShake: No CinemachineVirtualCamera found on this GameObject!");
            return;
        }

        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>(); // Get noise component
        if (noise == null)
        {
            Debug.LogError("CameraShake: No CinemachineBasicMultiChannelPerlin found on Virtual Camera!");
            return;
        }

        if (noise.m_NoiseProfile == null)
        {
            Debug.LogError("No Noise Profile assigned! Assign one in the editor.");
        }
    }

    public void Shake()
    {
        Debug.Log("CameraShake applied to: " + GetComponent<CinemachineVirtualCamera>().name);
        Debug.Log("Shake method called!");
        
        shakeTimer = shakeDuration; // Start shake timer
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            Debug.Log("set noise parameters for shake");
            shakeTimer -= Time.deltaTime; // Reduce shake time
            float elapsedTime = shakeDuration - shakeTimer; 
            float randomFactor = 1f + Random.Range(-0.25f, 0.25f); 

            float dynamicAmplitude = shakeAmplitude * randomFactor * 
                                    Mathf.Sin(Mathf.PI * elapsedTime / shakeDuration);
            noise.m_AmplitudeGain = dynamicAmplitude; 
            Debug.Log("dynamicAmplitude: " + dynamicAmplitude);
            noise.m_FrequencyGain = shakeFrequency * (1f + Mathf.Sin(elapsedTime * 2f)); 
            Debug.Log("shakeFrequency: " + shakeFrequency);
        }
        else 
        {
            //Debug.Log("set noise parameters to 0");
            noise.m_AmplitudeGain = 0f; 
            noise.m_FrequencyGain = 0f; 
        }
    }
}
