using UnityEngine;
using System.Collections;

public class GeneManager : MonoBehaviour {
    // パラメータに関する情報をまとめたクラス
    public class Param
    {
        // 個体数
        public int playerNum;
        // プレイ時間(1 秒ごとに 15 個の動作を考える)
        public int playTime = 10;
        // 突然変位率
        public float mutationRate;
    }
    public static Param param;
    // player に関する情報をまとめたクラス
    public class Player
    {
        public sbyte[] gene;
        public float score;
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
            players[i].gene = new sbyte[15 * param.playTime];
            for (int j = 0; j < 15 * param.playTime; j++)
            {
                players[i].gene[j] = random[Random.Range(0, 3)];
            }
        }
    }

    // Use this for initialization
    void Start () {
        param = new Param();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
