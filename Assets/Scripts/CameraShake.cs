using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera vcam; // Reference to the virtual camera
    private CinemachineBasicMultiChannelPerlin noise; // Noise for shaking
    public float shakeDuration = 1f; // How long the shake lasts
    public float shakeAmplitude = 20f; // Intensity of the shake
    public float shakeFrequency = 20f; // Speed of the shake

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
    }

    public void Shake()
    {
        Debug.Log("Shake method called!");
        //if (noise == null) return; // Prevent errors

        //noise.m_AmplitudeGain = shakeAmplitude; // Set shake intensity
        //noise.m_FrequencyGain = shakeFrequency; // Set shake speed
        shakeTimer = shakeDuration; // Start shake timer
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            Debug.Log("set noise parameters for shake");
            shakeTimer -= Time.deltaTime; // Reduce shake time
            noise.m_AmplitudeGain = shakeAmplitude; // Set shake intensity
            Debug.Log("shakeAmplitude: " + shakeAmplitude);
            noise.m_FrequencyGain = shakeFrequency; // Set shake speed
            Debug.Log("shakeFrequency: " + shakeFrequency);
        }
        else 
        {
            Debug.Log("set noise parameters to 0");
            noise.m_AmplitudeGain = 0f; 
            noise.m_FrequencyGain = 0f; 
        }
    }
}
