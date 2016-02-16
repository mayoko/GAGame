using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SCGameController : MonoBehaviour {
    public UnityEngine.UI.Text scoreLabel;
    public SCPlayerController playerPrefab;
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
            player.mode = "auto";
            player.attr = GeneManager.players[i];
        }
        {
            // 手動入力のPlayer
            SCPlayerController player = Instantiate(playerPrefab);
            player.myNum = -1;
            player.mode = "manual";
            player.attr = new GeneManager.Player();
            player.attr.geneReset(GeneManager.param.playFrame); // このへんの生成はあとでGeneManagerに投げたい
        }
    }

    void Update()
    {
        // スコア表示
        scoreLabel.text = "Score: " + getScore().ToString();
    }

    // 現在のframe番号を自分で数えると他のオブジェクトとの実行順が気になるのでUnityに頼る
    public int getCurrentFrame()
    {
        return Time.frameCount - firstFrame; // 0はじまり
    }

    // 今後の拡張性を考えて大げさにscore取得関数
    public int getScore()
    {
        return getCurrentFrame(); // 生き残っているフレーム数そのまま
    }
}


