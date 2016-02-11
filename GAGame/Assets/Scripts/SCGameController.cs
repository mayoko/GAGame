using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SCGameController : MonoBehaviour {
    public UnityEngine.UI.Text scoreLabel;
    public int score;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }
    void Update()
    {
        // スコア算出
        float timeElapsed = Time.time - startTime;
        score = Mathf.RoundToInt(timeElapsed * 100);

        // スコア表示
        scoreLabel.text = "Score: " + score.ToString();
    }
}
