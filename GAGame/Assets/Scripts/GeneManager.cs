using UnityEngine;
using System.Collections;

public class GeneManager : MonoBehaviour {
    // パラメータに関する情報をまとめたクラス
    public class Param
    {
        // 個体数
        public int playerNum;
        // 変数名はへんだがそう読み替えることになった #声に出して読みたい日本語
        // 遺伝子の長さです
        public int playFrame;
		//
		public int framePerGene;
        // 突然変異率
        public float mutationRate;
        // 交叉のやり方
        public int crossingMode;
        // 淘汰のやり方
        public int selectionMode;
        // ステージの難易度
        public int difficulty;
        // 生存者数
        public int surviverNum;
    }
    public static Param param = new Param();
    // AnimationとかSakeruCheeseとかにまたがるビューのパラメータをまとめたクラス
    public class ViewParam
    {
        // 再生速度
        public float playSpeed = 1.0f;
        // スキップモード
        public bool isSkipping = false;
        // 遺伝子の世代 (ここに置くべきではない)
        public int generation = 0;
    }
    public static ViewParam viewParam = new ViewParam();
    // player に関する情報をまとめたクラス
    public class Player
    {
        public sbyte[] gene;
        public float score=0;
        public void geneReset(int size) { gene = new sbyte[size]; }
    }
    // 各プレイヤーの情報をまとめた配列
    public static Player[] players;

    // 初期化する
    public static void init()
    {
        sbyte[] random = { -1, 0, 1 };
        players = new Player[param.playerNum];
        for (int i = 0; i < param.playerNum; i++)
        {
            players[i] = new Player();
            players[i].geneReset(param.playFrame);
            for (int j = 0; j < param.playFrame; j++)
            {
                players[i].gene[j] = random[Random.Range(0, 3)];
            }
        }
        viewParam.generation = 0;
    }

    // Use this for initialization
    void Start () {
        param = new Param();
        param.playFrame = 150;
    }
    
    // Update is called once per frame
    void Update () {
    
    }
}
