using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SCGameController : MonoBehaviour {
    public UnityEngine.UI.Text scoreLabel;
    public UnityEngine.UI.Text aliveLabel;
    public GameObject sakeruButtonObject;
    public SCPlayerController playerPrefab;
    public int finalFrame; // 今ゲームの最終フレーム．PlayerオブジェクトなどがGeneManagerに依存するのはよくないのでgcが情報を持っておく
    private int firstFrame; // frame数計測用

    void Start()
    {
        // インフラ周り
        firstFrame = Time.frameCount;
        finalFrame = GeneManager.param.playFrame-1;

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
            // sbyteの既定値は0なので自動的に全部0が入る（リアルタイムに更新していく）
            // https://msdn.microsoft.com/ja-jp/library/83fhsxwc.aspx
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
        return getCurrentFrame()+1; // 生き残っているフレーム数
    }
}


