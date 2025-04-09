using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class SendToGoogle : MonoBehaviour
{
    [SerializeField]private string URL;

    private long _sessionID;
    
    private int _hitObstaclesNums;
    private bool _hitGateKey;

    public string _scene;
    public string _gameOverReason;
    public int _totalBalls;
    public float _distance;
    public string _StartSceneCount;
    public string _JYSceneCount;
    public string _ElasSceneCount;
    public string _JiayuSceneCount;
    public string _JingxuanSceneCount;
    public string _SerenaSceneCount;
    public string _ShujieSceneCount;

    public void Send() {
        //_totalBalls = 12;
        _hitObstaclesNums = 0;
        _hitGateKey = false;
        //_gameOverReason = "fall";

        StartCoroutine(Post(_sessionID.ToString(), _scene.ToString(), _totalBalls.ToString(), _hitObstaclesNums.ToString(), _hitGateKey.ToString(), _gameOverReason.ToString(), _distance.ToString()));
    }

    private void Awake() {
        _sessionID = DateTime.Now.Ticks;
        //_sessionID = 0000001;
        //Send();
    }

    private IEnumerator Post(string sessionID, string scene, string totalBalls, string hitObstaclesNums, string hitGateKey, string gameOverReason, string distance) {
        WWWForm form = new WWWForm();

        // Debug.Log(sessionID);
        // Debug.Log(totalBalls);
        Debug.Log(scene);
        form.AddField("entry.1239619260", sessionID);
        form.AddField("entry.1641318088", scene);
        form.AddField("entry.1827859925", totalBalls);
        form.AddField("entry.1032588362", hitObstaclesNums);
        form.AddField("entry.301892217", hitGateKey);
        form.AddField("entry.204214296", gameOverReason);
        form.AddField("entry.996612829", distance);
        // scene count
        form.AddField("entry.1542345612", _StartSceneCount);
        form.AddField("entry.2086177145", _JingxuanSceneCount);
        form.AddField("entry.276954649", _ShujieSceneCount);
        form.AddField("entry.71217696", _SerenaSceneCount);
        form.AddField("entry.825175678", _JYSceneCount);
        form.AddField("entry.711519537", _ElasSceneCount);
        form.AddField("entry.1458266563", _JiayuSceneCount);


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
