﻿using UnityEngine;
using System.Collections;

// アニメーションでひとつのオブジェクトを動かすためのクラス
public class AMElement : MonoBehaviour {
	// オブジェクトを動かす周期
	public float interval;
	// 進捗管理するための変数
	public int progress;
	// move が呼ばれるたびに動くベクトル
	private Vector3 dr;

	void Awake() {
		interval = 0.01f;
		progress = 0;
	}
	// to に向かって今の位置から t 秒で進む
	// 速度調整するときは t を調整すれば良い
	// 単体で動かす場合はこちらを使う
	// AMGroup に動くのを任せる場合は move を使う
	public IEnumerator moveWith(Vector3 to, float t) {
		// 動きが終わるまでは while から抜けないこと
		while (true)
		{
			// 初めて move が呼ばれたときは, 毎周期進む距離を dr で求めておく
			if (progress == 0)
			{
				dr = (to - transform.position) / t;
				dr *= interval;
				progress = -1;
			}

			Vector3 old = transform.position;
			transform.position = transform.position + dr;
			// 目的地にいけてるなら終了フラグを立てる
			if (Vector3.Dot(to - old, to - transform.position) <= 0)
			{
				progress = 1;
				yield break;
			}
			yield return new WaitForSeconds(interval);
		}
	}
	// to に向かって今の位置から t 秒で進む
	// 速度調整するときは t を調整すれば良い
	public void move(Vector3 to, float t) {
		// 初めて move が呼ばれたときは, 毎周期進む距離を dr で求めておく
		if (progress == 0)
		{
			dr = (to - transform.position) / t;
			dr *= interval;
			progress = -1;
		}

		Vector3 old = transform.position;
		transform.position = transform.position + dr;
		// 目的地にいけてるなら終了フラグを立てる
		if (Vector3.Dot(to - old, to - transform.position) <= 0)
		{
			progress = 1;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}