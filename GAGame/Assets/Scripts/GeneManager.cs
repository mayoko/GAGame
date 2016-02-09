using UnityEngine;
using System.Collections;

public class GeneManager : MonoBehaviour {
    // 個体数
    public static int playerNum;
    // プレイ時間(1 秒ごとに 15 個の動作を考える)
    public static int playTime;
    // 突然変位率
    public static float mutationRate;
    // player に関する情報をまとめたクラス
    public class PlayerParam
    {
        public sbyte[] gene;
        public float score;
    }
    // 各プレイヤーの情報をまとめた配列
    public static PlayerParam[] playerParam;

    // Use this for initialization
    void Start () {
        sbyte[] random = { -1, 0, 1 };
        playerParam = new PlayerParam[playerNum];
        for (int i = 0; i < playerNum; i++)
        {
            playerParam[i].gene = new sbyte[15 * playTime];
            for (int j = 0; j < 15*playTime; j++)
            {
                playerParam[i].gene[j] = random[Random.Range(0, 3)];
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
