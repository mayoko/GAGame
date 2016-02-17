using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System; //Split,Single.Parse

public class SCGameController : MonoBehaviour {
    public UnityEngine.UI.Text scoreLabel;
    public UnityEngine.UI.Text aliveLabel;
    public GameObject sakeruButtonObject;
    public SCPlayerController playerPrefab;
    public int finalFrame; // 今ゲームの最終フレーム．PlayerオブジェクトなどがGeneManagerに依存するのはよくないのでgcが情報を持っておく
    private int firstFrame; // frame数計測用

	public GameObject sakeruEnemyObject;
	public TextAsset enemyPattern;
	public float[][] enemyInfo; 
	//読み込んだEnemyPatternの情報を保持.[n]がn番目の敵の情報の配列.敵の情報は(生成フレーム,生成位置,速度)
	public int enemyNum = 0; //次のEnemyが何番目かを保持.0から
	private int enemyFrame = 99999999; //次のEnemyが出現するフレームを保持
	private int enemyPop; //Enemyの数


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

            // UserのPlayerだけ色変更して特別な手前表示レイヤーに移動
            // ひとまず子がすべてsphereだと思って何も考えず書き換える
            // ちなみにmaterialをsharedMaterialにすると全Playerの見た目が変わる
            // http://kan-kikuchi.hatenablog.com/entry/Material
            foreach (Transform child in player.transform)
            {
                child.GetComponent<Renderer>().material.color = Color.green;
                child.gameObject.layer = 8; // User
            }
        }

		//EnemyのPatternの外部ファイルからの読み出し
		ReadEnemyPattern ();
		enemyFrame = (int)enemyInfo [0] [0]; //最初に生成するEnemyのFrameを保持
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

		//Enemy生成処理
		if (getCurrentFrame() > enemyFrame) {
			Instantiate (sakeruEnemyObject, new Vector3(6, 0, enemyInfo[enemyNum][1]), Quaternion.identity);
			enemyNum++;
			if (enemyNum < enemyPop) {
				enemyFrame = (int)enemyInfo[enemyNum][0];
			} else {
				enemyFrame = 99999999; //これ以上Enemyを生成しない
			}
		}
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

	//Enemyのデータを外部ファイルから読みだす
	void ReadEnemyPattern(){
		// ExternalFileフォルダ内のEnemyPattern.txtファイルを読み込む
		string[] patternInfo = enemyPattern.text.Split("\n"[0]);
		enemyPop = patternInfo.Length;
		enemyInfo = new float[enemyPop][];

		string[] eachInfo;
		for (int i = 0; i < enemyPop; i++) {
			eachInfo = patternInfo [i].Split (","[0]);
			enemyInfo [i] = new float[] { Single.Parse (eachInfo [0]), Single.Parse (eachInfo [1]), Single.Parse (eachInfo [2]) }; 
		}
	}
}


