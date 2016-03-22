using UnityEngine;
using System.Collections;

// アニメーションのサンプル
public class AMTestAnimation : MonoBehaviour {
	public GameObject group, element;
	// Use this for initialization
	void Start () {
		// まず Instantiate 関数でオブジェクトを作る
		element = Instantiate (element, new Vector3 (-2, 0, 0), Quaternion.identity) as GameObject;
		group = Instantiate (group, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		// Sample にアニメーションの動きを次々に書いていく
		StartCoroutine ("Sample");
	}
	private IEnumerator Sample() {
		// 単体オブジェクトを動かす
		yield return StartCoroutine (element.GetComponent<AMElement> ().moveWith (new Vector3 (1, 1, 1), 1f));
		// オブジェクトをまとめて二段階に分けて動かす
		// 一段階目
		yield return StartCoroutine(group.GetComponent<AMGroup>().move(new Vector3(1, 1, 1), 1f));
		// 二段階目
		yield return StartCoroutine(group.GetComponent<AMGroup>().move(new Vector3 (1, 3, 1), 1f/5));
	}
	// Update is called once per frame
	void Update () {
	
	}
}
