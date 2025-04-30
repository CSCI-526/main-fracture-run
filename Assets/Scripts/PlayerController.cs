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
    public int ballCount = 10; //Start with 5 balls.
    public TMP_Text ballCountText; // Reference to the Text UI for curr
    
    private float gameOverY = -3.0f; // when player z value is smaller than -1 ----> game over

    public SendToGoogle googleForm;
    public ObjectSpawner objectSpawner;

    private Rigidbody rb; // Reference to player's Rigidbody.
    private Transform spawnTransform; // Reference to the spawn point.
    private bool BallCountCheckFlag = true;
    private float start_position;
    
    private float current_position;

    private TMP_Text distanceText;
    private float lastMessageDistance = 0f; 
    public static float distanceTraveled;
    public TMP_Text messageText;

    public Image starImage;   
    public float tolerance =10f;
    private bool starShown = false;
    private float starCooldown = 5f; 
    private float lastStarTime = 0f; 

    private Hashtable ht = new Hashtable();

    public TMP_Text starNumText;
    private int star_num = 0;
    public int ultimate_num = 0;
    public TMP_Text ultimateNumText;
    // Start is called before the first frame update
    public class GameData
    {
        public static float distanceTraveled;
        public static int star_num;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Access player's Rigidbody.
        GameData.star_num = star_num;
        UpdateBallCountUI();
        //UpdateDistanceUI();
        if (SceneManager.GetActiveScene().name == "SampleScene") {
            messageText.text = ""; 
            messageText.gameObject.SetActive(false); 
        } 
        googleForm._scene = "Start_Scene";
        if (SceneManager.GetActiveScene().name == "TutorialScene") {
            googleForm._scene = "Tutorial_scene";
        }
 

        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            start_position = 0;
        }
        else if (SceneManager.GetActiveScene().name == "TutorialScene")
        {
            start_position = 52.6f;
            ballCountText.fontSize = 28;
        }

        Debug.Log("{start_position}"+ start_position );

        // if (SceneManager.GetActiveScene().name == "SampleScene")
        // {
        //     distanceText.gameObject.SetActive(true); 
        // }
        


        GameObject spawnPoint = GameObject.FindGameObjectWithTag("Respawn");
        if (spawnPoint != null)
        {
            spawnTransform = spawnPoint.transform; // Access the spawn point's transform.
            Debug.Log("Spawn point found at: " + spawnTransform.position);
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
        ht.Add("StartScene", "0");

        if (starImage != null)
        {
            starImage.gameObject.SetActive(false);
        }
    }


    private void BallCountCheck()
    {
        if (ballCount == 0 && BallCountCheckFlag)
        {
            GameData.distanceTraveled = -transform.position.x+start_position;
            SceneManager.LoadScene("DeathScene1");
            if(objectSpawner.currPrefab != null)
                googleForm._scene = objectSpawner.currPrefab.name;
            googleForm._gameOverReason = "zeroball";
            googleForm._totalBalls = ballCount;
            current_position = transform.position.x;
            googleForm._distance = -(current_position - start_position);
            updateGoogleSceneCount();
            googleForm.Send();
            BallCountCheckFlag = false;
        }
    }


    private void Update() 
    {
        if (Time.timeScale == 0f) return;
        // game over
        float playerY = transform.position.y;
        if (playerY < gameOverY) {
            GameData.distanceTraveled = -transform.position.x + start_position;
            SceneManager.LoadScene("DeathScene");
            if(objectSpawner.currPrefab != null)
                googleForm._scene = objectSpawner.currPrefab.name;
            googleForm._gameOverReason = "fall";
            googleForm._totalBalls = ballCount;
            current_position = transform.position.x;
            googleForm._distance = -(current_position - start_position);
            updateGoogleSceneCount();
            googleForm.Send();
        }

        if (ballCount == 0)
        {
            Invoke("BallCountCheck", 1f);
            //BallCountCheck();
        }
        UpdateDistanceUI();
        CheckGreatMessage();

        // if(ballCount == 0){
        //     SceneManager.LoadScene("DeathScene1");
        //     googleForm._scene = SceneManager.GetActiveScene().name;
        //     googleForm._gameOverReason = "zeroball";
        //     googleForm.Send();
        // }

        // if(playerY < gameOverY || ballCount == 0) {
        //     ShowGameOverWithDelay();
        // }

        // restart game
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     RestartGame();
        // }

        // use end_cube to get star
        //GameObject[] endCubes = GameObject.FindGameObjectsWithTag("EndCube");
        // foreach (GameObject endCube in endCubes)
        // {
        //     Collider endCubeCollider = endCube.GetComponent<Collider>();
        //     if (endCubeCollider != null)
        //     {
        //         Vector3 playerPos = transform.position;


        //         if (endCubeCollider.bounds.Contains(playerPos) ||
        //             (Mathf.Abs(playerPos.x - endCube.transform.position.x) < tolerance &&
        //              playerPos.y <= endCube.transform.position.y + endCubeCollider.bounds.extents.y + tolerance &&
        //              playerPos.y >= endCube.transform.position.y - endCubeCollider.bounds.extents.y - tolerance))
        //         {
             
        //             if (!starShown || (Time.time - lastStarTime >= starCooldown))
        //             {
        //                 Debug.Log("Player reached EndCube!");
        //                 ShowStar();
        //                 lastStarTime = Time.time; 
        //                 starShown = true;
        //                 star_num++;
        //                 GameData.star_num = star_num; 
        //                 starNumText.text = star_num.ToString();
        //                 //Debug.Log("star_num++");
        //             }
        //             break;
        //         }
        //     }
        // }

        if (Time.time - lastStarTime >= starCooldown)
        {
            starShown = false; 
        }

    }
    private void ShowStar()
    {
        if (starImage != null)
        {
            starImage.gameObject.SetActive(true); 
            starImage.transform.localScale = Vector3.zero;
            StartCoroutine(AnimateStar());
        }
    }

    private IEnumerator AnimateStar()
    {
        float duration = 1f;
        float elapsed = 0f;


        while (elapsed < duration)
        {
            float scale = Mathf.Lerp(0f, 1.5f, elapsed / duration); 
            starImage.transform.localScale = new Vector3(scale, scale, scale);
            elapsed += Time.deltaTime;
            yield return null; 
        }

        yield return new WaitForSeconds(0.5f);
        starImage.gameObject.SetActive(false); 
    }

    private void UpdateDistanceUI()
    {

        if (distanceText != null)
        {
            current_position = transform.position.x;
            float distanceTraveled = -(current_position - start_position);
            distanceText.fontSize = 28;
            distanceText.text = "Distance: " + distanceTraveled.ToString("F2") + " m";

        }
        else
        {
            //Debug.LogError("Distance Count UI Text not assigned!");
        }

    }
    private void CheckGreatMessage()
    {
        float distanceTraveled = -(current_position - start_position);
        //Debug.Log("distanceTraveled" + distanceTraveled+ "lastMessageDistance" + lastMessageDistance);

        if (distanceTraveled >= lastMessageDistance + 200f)
        {
            StartCoroutine(ShowGreatMessage());
            lastMessageDistance += 200f;
            //Debug.Log("lastMessageDistance"+ lastMessageDistance);
        }
    }

    private IEnumerator ShowGreatMessage()
    {
        //Debug.Log("ShowGreatMessage called");

       
        messageText.gameObject.SetActive(true); 
        messageText.text = "Great!";
        messageText.color = Color.green; 
        messageText.transform.localScale = Vector3.one; 
        messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, 1f);

        Vector3 originalPosition = messageText.transform.localPosition;
        Vector3 targetPosition = originalPosition + new Vector3(0, 50, 0); 
        Vector3 originalScale = messageText.transform.localScale;
        Vector3 targetScale = originalScale * 2f; 

        float duration = 1f; 
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            messageText.transform.localPosition = Vector3.Lerp(originalPosition, targetPosition, t);
            messageText.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

       
        messageText.transform.localPosition = targetPosition;
        messageText.transform.localScale = targetScale;

       
        yield return new WaitForSeconds(1f);

        
        for (float fadeOutTime = 0; fadeOutTime < 1; fadeOutTime += Time.deltaTime)
        {
            Color color = messageText.color;
            color.a = Mathf.Lerp(1, 0, fadeOutTime); 
            messageText.color = color;
            yield return null; 
        }

        messageText.gameObject.SetActive(false); 
    }


    // Add to the ball count.
    public void AddBallCount(int amount)
    {
        ballCount += amount;
        Debug.Log("Ball count updated! Total balls: " + ballCount);
        UpdateBallCountUI(); // Ensure the UI updates whenever the ball count changes
        if (SceneManager.GetActiveScene().name == "TutorialScene")  return;
        if(objectSpawner.currPrefab == null)
            ChangeBallCountInHashTable("StartScene", amount);
        else {
            ChangeBallCountInHashTable(objectSpawner.currPrefab.name, amount);
        }
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
            //ballCountText.text = "Total Balls: " + ballCount;
            ballCountText.text = ": " + ballCount;
            if (ballCount < 5)
            {
                ballCountText.color = Color.red;
                ballCountText.fontSize = 40; 
            }
            else
            {

                ballCountText.color = Color.white;
                ballCountText.fontSize = 28;
                //Debug.Log("ballcount text font = 28"); 
            }
        }
        else
        {
            Debug.LogError("Ball Count UI Text not assigned!");
        }
    }


    public void ShowGameOver() {
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    public void ApplyPenalty(int change_score)
    {
        ballCount = Mathf.Max(0, ballCount + change_score);
        UpdateBallCountUI();
        if (SceneManager.GetActiveScene().name == "TutorialScene") return;
        if(objectSpawner.currPrefab == null)
            ChangeBallCountInHashTable("StartScene", change_score);
        else {
            ChangeBallCountInHashTable(objectSpawner.currPrefab.name, change_score);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // get star
        if(other.CompareTag("getStar")) {
            if (!starShown || (Time.time - lastStarTime >= starCooldown))
            {
                ShowStar();
                lastStarTime = Time.time; 
                starShown = true;
                star_num++;
                GameData.star_num = star_num; 
                starNumText.text = star_num.ToString();
                if(star_num % 2 == 0) {
                    ultimate_num++;
                    ultimateNumText.text = ultimate_num.ToString();
                }
                //Debug.Log("star_num++");
            }
        }


    }

    private void ChangeBallCountInHashTable(string scene_name, int amount) {
        if(!ht.Contains(scene_name)) {
            ht.Add(scene_name, "0");
        }
        //int scene_count = int.Parse(ht[scene_name]);
        string s = (string)ht[scene_name];
        int scene_count = int.Parse(s);
        scene_count += amount;
        ht[scene_name] = scene_count.ToString();
        Debug.Log("hashtable scene count");
        Debug.Log(scene_count);
    }

    private void updateGoogleSceneCount() {
        googleForm._StartSceneCount = ht.Contains("StartScene") ? (string)ht["StartScene"] : "none";
        googleForm._JiayuSceneCount = ht.Contains("JiayuScene(Clone)") ? (string)ht["JiayuScene(Clone)"] : "none";
        googleForm._JingxuanSceneCount = ht.Contains("JingxuanScene(Clone)") ? (string)ht["JingxuanScene(Clone)"] : "none";
        googleForm._JYSceneCount = ht.Contains("JYScene(Clone)") ? (string)ht["JYScene(Clone)"] : "none";
        googleForm._SerenaSceneCount = ht.Contains("SerenaScene(Clone)") ? (string)ht["SerenaScene(Clone)"] : "none";
        googleForm._ShujieSceneCount = ht.Contains("ShujieScene(Clone)") ? (string)ht["ShujieScene(Clone)"] : "none";
        googleForm._ElasSceneCount = ht.Contains("ElsaScene(Clone)") ? (string)ht["ElsaScene(Clone)"] : "none";
    }
}