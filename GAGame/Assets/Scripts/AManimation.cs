﻿using UnityEngine;
using System.Collections;

public class AManimation : MonoBehaviour {
	// Use this for initialization
	int[] score;
	object[] scoreobject;

	//遺伝子の配列情報（-1,0,1を適当な色に置き換えておく）
	int[][] collorarray;

	object[][] generationgenes;
	object[] geneobject;

	//親となる遺伝子の（子を作る）順番を記した配列
	int[] fatherarray;
	int[] motherarray;

	object father;
	object mother;
	object child;

	void Start () {
		//親の遺伝子集団の作成(並べるだけ)
		for (int i=0; i<25; i++){
			scoreobject [i] = Instantiate (prefub, new Vector3 (i*15, 0, 0), Quaternion.identity) as GameObject;
			for (int k = 0; k < 30; k++) {
				geneobeject [k] = Instantiate (prefub, new Vector3 (i*15, 7+ k*10, 0), Quaternion.identity) as GameObject;
			}
			generationgenes [i] = geneobject;
			scoreobeject [i].GetComponent<AMGroup> ().setScore (score[i]);
			geneobject [i].GetComponent<AMGroup> ().setColor (colorarray [i]);
		}
		for (int i=25; i<50; i++){
			scoreobject [i] = Instantiate (prefub, new Vector3 ((i-25)*15, 320, 0), Quaternion.identity) as GameObject;
			for (int k = 0; k < 30; k++) {
				geneobeject [k] = Instantiate (prefub, new Vector3 ((i-25)*15, 327+ k*10, 0), Quaternion.identity) as GameObject;
			}
			generationgenes [i] = geneobject;
			scoreobeject [i].GetComponent<> ().setScore (score[i]);
			geneobject [i].GetComponent<> ().setColor (colorarray [i]);
		}
		//交叉の開始
		StartCoroutine ("cross");
	}

	private IEnumerator cross(){
		//親の移動
		　for (int i=0; i<50; i++){
			father = generationgenes [fatherarray [i]];
			mother = generationgenes [motherarray [i]];
			yield return StartCoroutine (father.GetComponent<AMGroup> ().move (new Vector3 (-45, 0, 0), 1f));
			yield return StartCoroutine (mother.GetComponent<AMGroup> ().move (new Vector3 (-15, 0, 0), 1f));
		}
	}