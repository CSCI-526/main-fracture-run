using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class SendToGoogle : MonoBehaviour
{
    [SerializeField]private string URL;

    private long _sessionID;
    private int _totalBalls;
    private int _hitObstaclesNums;
    private bool _hitGateKey;
    public string _gameOverReason;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Send() {
        _totalBalls = 12;
        _hitObstaclesNums = 0;
        _hitGateKey = false;
        //_gameOverReason = "fall";

        StartCoroutine(Post(_sessionID.ToString(), _totalBalls.ToString(), _hitObstaclesNums.ToString(), _hitGateKey.ToString(), _gameOverReason.ToString()));
    }

    private void Awake() {
        _sessionID = DateTime.Now.Ticks;
        //_sessionID = 0000001;
        //Send();
    }

    private IEnumerator Post(string sessionID, string totalBalls, string hitObstaclesNums, string hitGateKey, string gameOverReason) {
        WWWForm form = new WWWForm();

        // Debug.Log(sessionID);
        // Debug.Log(totalBalls);
        // Debug.Log(hitObstaclesNums);
        form.AddField("entry.1239619260", sessionID);
        form.AddField("entry.1827859925", totalBalls);
        form.AddField("entry.1032588362", hitObstaclesNums);
        form.AddField("entry.301892217", hitGateKey);
        form.AddField("entry.204214296", gameOverReason);


        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) {
            yield return www.SendWebRequest();

            if(www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log("Form upload complete!");
            }
        }
    }
}
