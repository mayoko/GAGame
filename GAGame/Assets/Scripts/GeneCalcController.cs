using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class GeneCalcController : MonoBehaviour {

	Scene mainScene;
	GeneManager.Player[] playerParam;//[groupsize]

	int[] mothers, fathers;//[childnum]
	int[][] crossArrays;//[childnum][genesize]
	Dictionary<int,int>[] mutationDicts;//[childnum]

	int selectionOption=0;
	int groupSize = 30;
	int surviveNum=4;

	// Use this for initialization
	void Start () {
		mainScene = SceneManager.GetSceneByName ("GameMain");
		//playerParam=
		geneCalc();
		//  =mutationPlayerParam
	}

	// Update is called once per frame
	void Update () {

	}

	void geneCalc(){
		Array.Sort (playerParam, 
			delegate(GeneManager.Player p1, GeneManager.Player p2) {
				return p1.score.CompareTo (p2.score);
			}
		);

		int[] selectScoreAccumArray;
		selectPrepare (out selectScoreAccumArray);

		int childNum=groupSize-surviveNum;
		mothers = new int[groupSize];
		fathers = new int[groupSize];
		for (int i = 0; i < groupSize; i++) {
			SelectParentRandom (selectScoreAccumArray, out mothers [i], out fathers [i]);
		}
		//crossedPlayerParam = new GeneManager.PlayerParam[groupSize];
		for (int i = 0; i < groupSize; i++) {
			//Cross(sortedPlayerParam[mothers[i]].gene,sortedPlayerParam[fathers[i]].gene,out crossedPlayerParam[i])
		}

		//mutationPlayerParam = new GeneManager.PlayerParam[groupSize];
		for (int i = 0; i < groupSize; i++) {
			//mutation(newPlayerParam[i],out mutationPlayerParam[i])
		}
	}

	void selectPrepare(out int[] selectScoreAccumArray)
	{
		selectScoreAccumArray = new int[groupSize];
		switch (selectionOption) {
		case 0: //top 10 random
			for (int i = 0; i < Math.Min (10, groupSize); i++) {
				selectScoreAccumArray [i] = i + 1;
			}
			for (int i = Math.Min (10, groupSize); i < groupSize; i++) {
				selectScoreAccumArray [i] = selectScoreAccumArray [i - 1];
			}
			break;
		case 1: //proportion to score
			selectScoreAccumArray [0] = (int)(100*playerParam [0].score);
			for (int i = 1; i < groupSize; i++) {
				selectScoreAccumArray [i] = selectScoreAccumArray [i - 1] + (int)(100*playerParam [i].score);
			}
			break;
		}
	}
	void SelectParentRandom(int[] scoreAccum,out int mIndex,out int fIndex)
	{
		int rand = UnityEngine.Random.Range (0, scoreAccum[groupSize - 1]);
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

		int offset=scoreAccum[mIndex] - ((mIndex > 0) ? scoreAccum[mIndex - 1] : 0);
		rand = UnityEngine.Random.Range (0, scoreAccum [groupSize - 1] - offset);
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
}
