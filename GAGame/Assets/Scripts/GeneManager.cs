using UnityEngine;
using System.Collections;

public class GeneManager : MonoBehaviour {
    // パラメータに関する情報をまとめたクラス
    public class Param
    {
        // 個体数
        public int playerNum;
        // プレイ時間(フレーム数)
        public int playFrame;
        // 突然変異率
        public float mutationRate;
    }
    public static Param param = new Param();
    // player に関する情報をまとめたクラス
    public class Player
    {
        public sbyte[] gene;
        public float score;
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
