using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System; //Split,Single.Parse

public class SCGameController : MonoBehaviour {
    public UnityEngine.UI.Text scoreLabel;
    public UnityEngine.UI.Text aliveLabel;
    public UnityEngine.UI.Text generationLabel;
    public GameObject sakeruButtonObject;
    public SCPlayerController playerPrefab;
    public int geneSize;
    public int fpg = 5; // Frames per Gene. 1geneの指示する動作を何フレーム継続するか
    public int finalFrame; // 今ゲームの最終フレーム．PlayerオブジェクトなどがGeneManagerに依存するのはよくないのでgcが情報を持っておく
    //private int firstFrame; // frame数計測用
	int testFrame; //加速したりしてもスコアをカウントできるように

	public GameObject sakeruEnemyObject;
	public TextAsset enemyPattern0;
	public TextAsset enemyPattern1;
	public TextAsset enemyPattern2;
	public TextAsset enemyPattern3;
	public TextAsset enemyPattern;

	public float[][] enemyInfo; 
	//読み込んだEnemyPatternの情報を保持.[n]がn番目の敵の情報の配列.敵の情報は(縦の長さ,横の長さ,出現位置,速度,時間)
	public int enemyNum = 0; //次のEnemyが何番目かを保持.0から
	private int enemyFrame = 99999999; //次のEnemyが出現するフレームを保持
	private int enemyPop; //Enemyの数

	public int scDifficulty;

    void Start()
    {
        // インフラ周り
        GeneManager.viewParam.generation++; // ここに書くべきではない
        geneSize = GeneManager.param.playFrame; // 変数名はへんだがそう読み替えることになった
        //firstFrame = Time.frameCount;
        finalFrame = fpg*geneSize-1;
		fpg = GeneManager.param.framePerGene;
        testFrame = 0;

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
		enemyFrame = (int)enemyInfo [0] [4]; //最初に生成するEnemyのFrameを保持
    }

    void Update()
    {
        // 残存Player数カウント
        int alive = GameObject.FindGameObjectsWithTag("Player").Length;

        // UI表示
        scoreLabel.text = "Score: " + getScore().ToString() + " / " + getMaxScore().ToString();
        aliveLabel.text = "Alive: " + alive.ToString();
        generationLabel.text = "第" + GeneManager.viewParam.generation.ToString() + "世代";

        // ゲーム終了処理
        if (alive == 0) finishGame();

        //高速再生処理
        //if (Input.GetKey("space"))
        //{
        //    Time.timeScale = 4;
        //}
        //else {
        //    Time.timeScale = 1;
        //}
        Time.timeScale = GeneManager.viewParam.playSpeed;
        if (Input.GetKey("space")) Time.timeScale *= 4;

        // space キー押したらタイトルへ戻る
        if (Input.GetKey(KeyCode.Q))
        {
            SceneManager.LoadScene("GameMain");
        }
    }

    void FixedUpdate()
    {
        //Enemy生成処理
        if (getCurrentFrame() >= enemyFrame) EnemyAppear();

        testFrame++;
    }

    void finishGame()
    {
        // Scoreの更新はひとまず止めなくていいや
        // 高速実行の際は即終了するようなオプションをParamにつけてもらいたい予定
        if (!GeneManager.viewParam.isSkipping)
        {
            sakeruButtonObject.SetActive(true);
        } else
        {
            SceneManager.LoadScene("GeneCalc");
        }
    }

    // 現在のframe番号を自分で数えると他のオブジェクトとの実行順が気になるのでUnityに頼る
    public int getCurrentFrame()
    {
        //return Time.frameCount - firstFrame; // 0はじまり
        return testFrame;
    }

    // 今後の拡張性を考えて大げさにscore取得関数
    public int getScore()
    {
        return getCurrentFrame() + 1; // 生き残っているフレーム数
    }
    public int getMaxScore()
    {
        return finalFrame + 1;
    }

    //Enemyのデータを外部ファイルから読みだす
    void ReadEnemyPattern(){
		string[] patternInfo;
		if (GeneManager.param.difficulty == 0) {
			patternInfo = enemyPattern0.text.Split ("\n" [0]);
		} else if (GeneManager.param.difficulty == 1) {
			patternInfo = enemyPattern1.text.Split ("\n" [0]);
		} else if (GeneManager.param.difficulty == 2) {
			patternInfo = enemyPattern2.text.Split ("\n" [0]);
		} else if (GeneManager.param.difficulty == 3) {
			patternInfo = enemyPattern3.text.Split ("\n" [0]);
		} else {
			patternInfo = enemyPattern.text.Split ("\n" [0]);
		}
			
		enemyPop = patternInfo.Length;
		enemyInfo = new float[enemyPop][];

		string[] eachInfo;
		for (int i = 0; i < enemyPop; i++) {
			eachInfo = patternInfo [i].Split (","[0]);
			enemyInfo [i] = new float[] { Single.Parse (eachInfo [0]), Single.Parse (eachInfo [1]),
										  Single.Parse (eachInfo [2]), Single.Parse (eachInfo [3]),
										  Single.Parse (eachInfo [4]) }; 
		}
	}

    //enemyInfoは[n]がn番目の敵の情報の配列.敵の情報は(縦の長さ,横の長さ,出現位置,速度,時間)    
    void EnemyAppear()
    {
        float enemyX;
		Vector3 enemyAngle = new Vector3( 0.0f, 0.0f, 0.0f );
		if (enemyInfo [enemyNum] [3] > 0) {
			enemyX = 11.0f;
		}
		else {
			enemyX = -11.0f;
			enemyInfo [enemyNum] [3] = -enemyInfo [enemyNum] [3];
			enemyAngle[1] = 180.0f;
		}

        GameObject enemy = Instantiate(sakeruEnemyObject,
                                            new Vector3(enemyX, 0.0f, enemyInfo[enemyNum][2]),
			Quaternion.Euler(enemyAngle)) as GameObject;
        SCEnemyController scec = enemy.GetComponent<SCEnemyController>();
        scec.enemySpeed = enemyInfo[enemyNum][3];
        scec.transform.localScale = new Vector3(enemyInfo[enemyNum][1], 1.0f, enemyInfo[enemyNum][0]);
        enemyNum++;
        if (enemyNum < enemyPop) {
            enemyFrame = (int)enemyInfo[enemyNum][4];
        } else {
            enemyFrame = 99999999; //これ以上Enemyを生成しない
        }
    }
}