using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SCGameController : MonoBehaviour {
    public UnityEngine.UI.Text scoreLabel;
    public UnityEngine.UI.Text aliveLabel;
    public GameObject sakeruButtonObject;
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
        // 残存Player数カウント
        int alive = GameObject.FindGameObjectsWithTag("Player").Length;

        // UI表示
        scoreLabel.text = "Score: " + getScore().ToString();
        aliveLabel.text = "Alive: " + alive.ToString();

        // ゲーム終了処理
        if (alive == 0) finishGame();
    }

    void finishGame()
    {
        // Scoreの更新はひとまず止めなくていいや
        // 高速実行の際は即終了するようなオプションをParamにつけてもらいたい予定
        if (true)
        {
            sakeruButtonObject.SetActive(true);
        } else
        {
            // SceneManager.LoadScene("GeneCalc");
        }
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


