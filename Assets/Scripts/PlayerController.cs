using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


// Controls player movement and rotation.
public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Set player's movement speed.
    public float strafeSpeed = 4.0f; // Set player's rotation speed.
    public int ballCount = 5; //Start with 5 balls.
    public TMP_Text ballCountText; // Reference to the Text UI for curr
    
    public TMP_Text gameOverText;
    private float gameOverY = -3.0f; // when player z value is smaller than -1 ----> game over

    public TMP_Text MovementText;
    public TMP_Text clickText;

    public SendToGoogle googleForm;

    private Rigidbody rb; // Reference to player's Rigidbody.
    private Transform spawnTransform; // Reference to the spawn point.
    

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Access player's Rigidbody.
        UpdateBallCountUI();
        gameOverText.gameObject.SetActive(false);
        clickText.gameObject.SetActive(false);


        GameObject spawnPoint = GameObject.FindGameObjectWithTag("Respawn");
        if (spawnPoint != null)
        {
            spawnTransform = spawnPoint.transform; // Access the spawn point's transform.
        }
        else
        {
            Debug.LogError("SpawnPoint not found in the scene!");
        }
        Teleport(spawnTransform);

        //cameraShake = FindObjectOfType<CameraShake>();
        //if (cameraShake == null)
        //{
            //Debug.LogError("CameraShake script not found in the scene!");
        //}
    }



    private void Update() 
    {
        // game over
        float playerY = transform.position.y;
        if (playerY < gameOverY) {
            SceneManager.LoadScene("DeathScene");
            googleForm._scene = SceneManager.GetActiveScene().name;
            googleForm._gameOverReason = "fall";
            googleForm.Send();
        }

        if(ballCount == 0){
            SceneManager.LoadScene("DeathScene1");
            googleForm._scene = SceneManager.GetActiveScene().name;
            googleForm._gameOverReason = "zeroball";
            googleForm.Send();
        }

        if(playerY < gameOverY || ballCount == 0) {
            ShowGameOverWithDelay();
        }

        // restart game
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        if(transform.position.x > -20f) {
            MovementText.gameObject.SetActive(true);
            clickText.gameObject.SetActive(false);
        }

        if(transform.position.x < -20f) {
            MovementText.gameObject.SetActive(false);
            clickText.gameObject.SetActive(true);
        }

        if(transform.position.x < -40f) {
            clickText.gameObject.SetActive(false);
        }
    }

    // Add to the ball count.
    public void AddBallCount(int amount)
    {
        ballCount += amount;
        Debug.Log("Ball count updated! Total balls: " + ballCount);
        UpdateBallCountUI(); // Ensure the UI updates whenever the ball count changes
    }

    // Handle physics-based movement and rotation.
    private void FixedUpdate()
    {

        // Player always moves forward
        Vector3 forwardMovement = transform.forward * speed * Time.fixedDeltaTime;

        // Left and Right movement (Strafing)
        float moveHorizontal = Input.GetAxis("Horizontal"); // A = -1, D = 1
        Vector3 strafeMovement = transform.right * moveHorizontal * strafeSpeed * Time.fixedDeltaTime;

        // Apply both movements
        rb.MovePosition(rb.position + forwardMovement + strafeMovement);

    }

    // Player teleported to the spawn point
    public void Teleport(Transform spawnTransform)
    {
        transform.position = spawnTransform.position;
        transform.rotation = spawnTransform.rotation;
        rb.velocity = Vector3.zero; 
    }

    private void UpdateBallCountUI()
    {
        if (ballCountText != null)
        {
            ballCountText.text = "Total Balls: " + ballCount;
        }
        else
        {
            Debug.LogError("Ball Count UI Text not assigned!");
        }
    }


    public void ShowGameOver() {
        gameOverText.gameObject.SetActive(true);
        gameOverText.alignment = TextAlignmentOptions.Center; // make the text apper in the center of the camera
        Time.timeScale = 0f;
        Debug.Log("Game Over!");
    }
    public void ShowGameOverWithDelay()
    {
        StartCoroutine(ShowGameOverCoroutine());
    }
    private IEnumerator ShowGameOverCoroutine() {
        yield return new WaitForSeconds(1f);
        ShowGameOver();
    }

    public void RestartGame()
    {
        Teleport(spawnTransform);
        gameOverText.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    public void ApplyPenalty(int change_score)
    {
        ballCount = Mathf.Max(0, ballCount + change_score);
        ballCountText.text = "Total Balls: " + ballCount;// Example: Reduce 2 balls, but not below 0
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Main"))
        {
            SceneManager.LoadScene("BeginScene"); // 切换到结算场景
            googleForm._scene = SceneManager.GetActiveScene().name;
            googleForm._gameOverReason = "Success";
            googleForm.Send();
        }
    }
}