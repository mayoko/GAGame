using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SCGameController : MonoBehaviour {
    public UnityEngine.UI.Text scoreLabel;
    public SCPlayerController playerPrefab;
    public int score;
    private int firstFrame; // frame数計測用

    void Start()
    {
        // インフラ周り
        firstFrame = Time.frameCount;

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
        // スコア算出
        score = getCurrentFrame();

        // スコア表示
        scoreLabel.text = "Score: " + score.ToString();
    }

    // 現在のframe番号を自分で数えると他のオブジェクトとの実行順が気になるのでUnityに頼る
    public int getCurrentFrame()
    {
        return Time.frameCount - firstFrame; // 0はじまり
    }
}


