using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AManimation : MonoBehaviour {
	// Use this for initialization
	int[] score;
	GameObject[] scoreobject;
	public GameObject element;

	//遺伝子の配列情報（-1,0,1を適当な色に置き換えておく）
	int[][] colorarray;

	GameObject[][] generationgenes;
	GameObject[] geneobject;

	//親となる遺伝子の（子を作る）順番を記した配列
	int[] fatherarray,motherarray;

	GameObject[] father,mother,child;

	//交叉ポイント
	int r;

	void Start () {
		//親の遺伝子集団の作成(並べるだけ)
		for (int i=0; i<25; i++){
			scoreobject [i] = Instantiate (element, new Vector3 (i*15, 0, 0), Quaternion.identity) as GameObject;
			for (int k = 0; k < 30; k++) {
				geneobject [k] = Instantiate (element, new Vector3 (i*15, 7+ k*10, 0), Quaternion.identity) as GameObject;
			}
			generationgenes [i] = geneobject;
			scoreobject [i].GetComponent<AMGroup> ().setScore (score[i]);
			geneobject [i].GetComponent<AMGroup> ().setColor (colorarray [i]);
		}
		for (int i=25; i<50; i++){
			scoreobject [i] = Instantiate (element, new Vector3 ((i-25)*15, 320, 0), Quaternion.identity) as GameObject;
			for (int k = 0; k < 30; k++) {
				geneobject [k] = Instantiate (element, new Vector3 ((i-25)*15, 327+ k*10, 0), Quaternion.identity) as GameObject;
			}
			generationgenes [i] = geneobject;
			scoreobject [i].GetComponent<AMGroup> ().setScore (score [i]);
			geneobject [i].GetComponent<AMGenePieces> ().setColor (colorarray [i]);
		}
		//交叉の開始
		StartCoroutine ("cross");
	}

	private IEnumerator cross ()
	{
		//親の移動
		　
		for (int i = 0; i < 50; i++) {
			father = generationgenes [fatherarray [i]];
			mother = generationgenes [motherarray [i]];
			yield return StartCoroutine (father[0].GetComponent<AMGroup> ().move (new Vector3 (-45, 0, 0), 1f));
			yield return StartCoroutine (mother[0].GetComponent<AMGroup> ().move (new Vector3 (-15, 0, 0), 1f));
			GameObject go1 = father[0].GetComponent<AMGroup> ().getSegment (0, r);
			GameObject go2 = mother[0].GetComponent<AMGroup> ().getSegment (r, 30 - 1);
			yield return StartCoroutine (go1.GetComponent<AMGroup> ().move (new Vector3 (-30, 0, 0), 1f));
			yield return StartCoroutine (go2.GetComponent<AMGroup> ().move (new Vector3 (-30, 7 + r * 10, 0), 1f));
			father[0].GetComponent<AMGenePieces>().delete ();
			mother[0].GetComponent<AMGenePieces> ().delete ();
			go1.GetComponent<AMGenePieces> ().delete ();
			go2.GetComponent<AMGenePieces> ().delete ();
		}
	}
 }