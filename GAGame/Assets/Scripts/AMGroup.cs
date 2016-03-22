﻿using UnityEngine;
using System.Collections;

// アニメーションでいくつかのオブジェクトをまとめて動かすためのクラス
public class AMGroup : MonoBehaviour {
	// まとめて動かす対象のオブジェクト(1種類のみ)
	public GameObject prefab;
	// 何秒周期で動かすか
	public float interval;
	// オブジェクトをまとめておくための配列
	private GameObject[] gameObjects;
	// オブジェクトの個数(3000 個らへん超えるとかなり重くなる)
	public int geneSize;

	// instantiate されたときに呼ばれる関数
	// 要するに初期化してくれる
	void Awake() {
		interval = 0.01f;
		geneSize = 1000;
		gameObjects = new GameObject[geneSize];
		for (int i = 0; i < geneSize; i++)
		{
			// 指定した座標にオブジェクトを作る
			// (ここでは等間隔に並べている)
			gameObjects[i] = Instantiate(prefab, new Vector3(2*i, 0, 0), Quaternion.identity) as GameObject;
		}
	}

	// to に向かって今の位置から t 秒で進む
	// 速度調整するときは t を調整すれば良い
	public IEnumerator move(Vector3 to, float t)
	{
		// 動きが終わるまでは while から抜けないこと
		while (true)
		{
			// 各オブジェクトを目標位置に動かす
			for (int i = 0; i < geneSize; i++)
			{
				gameObjects[i].GetComponent<AMElement>().move(to + i * (new Vector3(2, 0, 0)), t);
			}
			// 目的地にいけてるなら終了フラグを立てる
			if (gameObjects [0].GetComponent<AMElement> ().progress == 1) {
				for (int i = 0; i < geneSize; i++)
					gameObjects [i].GetComponent<AMElement> ().progress = 0;
				Debug.Log ("finish!");
				yield break;
			}
			yield return new WaitForSeconds(interval);
		}
	}
	// Update is called once per frame
	void Update () {

	}
	// Use this for initialization
	void Start () {

	}
}