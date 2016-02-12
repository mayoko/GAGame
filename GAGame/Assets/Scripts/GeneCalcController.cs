using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class GeneCalcController : MonoBehaviour {

	Scene mainScene;

	GeneManager.Player[] players;//[groupsize]
	sbyte[][] childGene;//[childnum][genesize]

	int[] mothers, fathers;//[childnum]
	int[][] crossArrays;//[childnum][]
	Dictionary<int,sbyte>[] mutationDicts;//[childnum]

	GeneManager.Param param;
	int selectionOption=0;
	int groupSize = 30;
	int surviveNum=4;
	int geneSize=100;
	int topSelectNum=10;
	int crossOption=1;
	float mutationRate=0.08f;

	// Use this for initialization
	void Start () {
		mainScene = SceneManager.GetSceneByName ("GameMain");
		//players=
		geneCalc();
		//  =newPlayerParam
	}

	// Update is called once per frame
	void Update () {

	}

	void geneCalc(){
		Array.Sort (players, 
			delegate(GeneManager.Player p1, GeneManager.Player p2) {
				return p1.score.CompareTo (p2.score);
			}
		);

		float[] selectScoreAccumArray;
		selectPrepare (out selectScoreAccumArray);

		int childNum=groupSize-surviveNum;
		mothers = new int[childNum];
		fathers = new int[childNum];

		for (int i = 0; i < childNum; i++) {
			SelectParentRandom (selectScoreAccumArray, out mothers [i], out fathers [i]);
		}

		childGene = new sbyte[childNum] [];
		crossArrays = new int[childNum][];
		for (int i = 0; i < childNum; i++) {
			cross (players [mothers [i]].gene, players [fathers [i]].gene, out childGene [i],out crossArrays[i]);
		}
		mutationDicts = new Dictionary<int, sbyte>[childNum];
		for (int i = 0; i < childNum; i++) {
			mutation (ref childGene [i], out mutationDicts [i]);
		}
	}

	void selectPrepare(out float[] selectScoreAccumArray)
	{
		selectScoreAccumArray = new float[groupSize];
		switch (selectionOption) {
		case 0: //top random
			for (int i = 0; i < Math.Min (topSelectNum, groupSize); i++) {
				selectScoreAccumArray [i] = (float)(i + 1);
			}
			for (int i = Math.Min (topSelectNum, groupSize); i < groupSize; i++) {
				selectScoreAccumArray [i] = selectScoreAccumArray [i - 1];
			}
			break;
		case 1: //proportion to score
			selectScoreAccumArray [0] = players [0].score;
			for (int i = 1; i < groupSize; i++) {
				selectScoreAccumArray [i] = selectScoreAccumArray [i - 1] + players [i].score;
			}
			break;
		}
	}

	void SelectParentRandom(float[] scoreAccum,out int mIndex,out int fIndex)
	{
		float rand = UnityEngine.Random.value * scoreAccum [groupSize - 1];
		int a = 0;
		int b = groupSize - 1;
		int c;

		if (rand < scoreAccum [0]) {
			mIndex = 0;
		} else {
			while (a + 1 < b) {
				c = (a + b) / 2;
				if (scoreAccum[c] <= rand)
					a = c;
				else
					b = c;
			}
			mIndex = b;
		}

		float offset=scoreAccum[mIndex] - ((mIndex > 0) ? scoreAccum[mIndex - 1] : 0.0f);
		rand = UnityEngine.Random.value * (scoreAccum [groupSize - 1] - offset);
		a = 0;
		b = groupSize - 1;
		if (mIndex != 0 && rand < scoreAccum[0])
		{
			fIndex = 0;
		}
		else
		{
			while (a + 1 < b)
			{

				c = (a + b) / 2;
				if ((mIndex > c && scoreAccum[c] <= rand)
					|| (mIndex <= c && scoreAccum[c] - offset <= rand)) a = c;
				else b = c;
			}
			fIndex = b;
		}
	}

	void cross(sbyte[] father, sbyte[] mother, out sbyte[] child, out int[] cross_array) {
		child = new sbyte[geneSize];
		switch (crossOption) {
		case 1: 
			int onecross_point = UnityEngine.Random.Range(0, geneSize);
			child = one_cross(father, mother, onecross_point);
			cross_array = new int[1];
			cross_array[0] = onecross_point;
			break;
		case 2:
			int crossstart = UnityEngine.Random.Range(0, geneSize);
			int crossgoal = UnityEngine.Random.Range(crossstart, geneSize);
			child = double_cross(father, mother, crossstart, crossgoal);
			cross_array = new int[2];
			cross_array[0] = crossstart;
			cross_array[1] = crossgoal;
			break;
		default:
			int crosssize = UnityEngine.Random.Range(0, geneSize);
			cross_array = new int[crosssize];
			cross_array = multicross_array(crosssize);
			child = multi_cross(father, mother, cross_array,crosssize);
			break;
		}
	}

	//一点交叉
	sbyte[] one_cross(sbyte[] father, sbyte[] mother, int crosspoint)
	{
		sbyte[] child = new sbyte[geneSize];
		for (int i = 0; i < crosspoint; i++) {
			child[i] = mother[i];
		}
		for (int i = crosspoint; i < geneSize; i++) {
			child [i] = father [i];
		}
		return child;
	}


	//二点交叉
	sbyte[] double_cross(sbyte[] father, sbyte[] mother, int crossstart, int crossgoal)
	{
		sbyte[] child = new sbyte[geneSize];
		for (int i = 0; i < crossstart; i++) {
			child [i] = father [i];
		}
		for (int i = crossstart ; i < crossgoal; i++) {
			child[i] = mother[i];
		}
		for (int i = crossgoal; i < geneSize; i++) {
			child [i] = father [i];
		}
		return child;
	}

	//多点交叉
	sbyte[] multi_cross(sbyte[] father, sbyte[] mother, int[] crossarray,int crosssize)
	{
		sbyte[] child = new sbyte[geneSize];
		for (int i = 0; i < geneSize; i++) {
			child [i] = father [i];
		}
		int k;
		for (int i = 0; i < crosssize; i++) {
			k = crossarray[i];
			child[k] = mother[k];
		}
		return child;
	}

	void mutation(ref sbyte[] gene, out Dictionary<int, sbyte> mutationpoint)
	{
		mutationpoint = new Dictionary<int, sbyte>();
		for (int i = 0; i < geneSize; i++) {
			if (UnityEngine.Random.value < mutationRate)
			{
				gene[i] = (sbyte)UnityEngine.Random.Range(-1, 2);
				mutationpoint.Add (i, gene [i]);
			}
		}
	}

	int[] multicross_array(int crosssize)
	{
		int[] tmparray = new int[geneSize];
		int[] randomarray = new int[crosssize];
		for (int i = 0; i < geneSize; i++) {
			tmparray[i] = i;
		}
		for (int i = 0; i < geneSize; i++) {
			int k = UnityEngine.Random.Range(0, geneSize);
			int tmp = tmparray[i];
			tmparray[i] = tmparray[k];
			tmparray[k] = tmp;
		}
		for (int i = 0; i < crosssize; i++) {
			randomarray[i] = tmparray[i];
		}
		return randomarray;
	}
}
