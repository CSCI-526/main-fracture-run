using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float destroyTime = 2.5f;  // Stays longer before disappearing
    public float moveSpeed = 20f;     // Slower movement upwards
    public float fadeSpeed = 0.5f;    // Slower fading effect

    private TextMeshProUGUI textMesh;
    private Color textColor;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textColor = textMesh.color;
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        // Move the text upwards slowly
        transform.localPosition += new Vector3(0, moveSpeed * Time.deltaTime, 0);

        // Gradually fade out the text
        textColor.a -= fadeSpeed * Time.deltaTime;
        textMesh.color = textColor;
    }

}
