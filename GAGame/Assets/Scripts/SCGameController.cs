using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SCGameController : MonoBehaviour {
    public UnityEngine.UI.Text scoreLabel;
    public SCPlayerController playerPrefab;
    public int count = -1;
    public int score;

    void Start()
    {
        // Player周り
        for (int i=0; i<GeneManager.players.Length; i++)
        {
            // Playerを量産
            SCPlayerController player = Instantiate(playerPrefab);
            player.myNum = i;
            player.attr = GeneManager.players[i];
        }
    }

    void Update()
    {
        count++; // 0はじまり
        // スコア算出
        score = count;

        // スコア表示
        scoreLabel.text = "Score: " + score.ToString();
    }
}


