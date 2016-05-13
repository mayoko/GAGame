using UnityEngine;
using System.Collections;
using System;

public class PramaeterManager : MonoBehaviour {
	public UnityEngine.UI.Text GroupSizeValue;
	public UnityEngine.UI.Slider GroupSizeSlider;
	public UnityEngine.UI.Text MutationRateValue;
	public UnityEngine.UI.Slider MutationRateSlider;
	public UnityEngine.UI.Text SuviverNumValue;
	public UnityEngine.UI.Slider SuviverNumSlider;
	public UnityEngine.UI.Text FPGValue;
	public UnityEngine.UI.Slider FPGSlider;
	public UnityEngine.UI.Dropdown CrossDropdown;
	public UnityEngine.UI.Dropdown SelectDropdown;
	public UnityEngine.UI.Dropdown DifficultyDropdown;
	public UnityEngine.UI.Dropdown PatternDropdown;
	// Use this for initialization
	void Start () {
		/*
		//GroupSize
		GeneManager.param.playerNum = 50;
		GroupSizeValue.text = GeneManager.param.playerNum.ToString ();
		//GroupSizeSlider.maxValue = 100;
		//GroupSizeSlider.minValue = 20;
		//GroupSizeSlider.value = (float)GeneManager.param.playerNum;
		//MutationRate
		GeneManager.param.mutationRate = 0.01f;
		MutationRateValue.text = GeneManager.param.mutationRate.ToString ("#0.##%");
		//MutationRateSlider.maxValue = 0;
		//MutationRateSlider.minValue = -3;
		//MutationRateSlider.value = (float)Math.Log10(GeneManager.param.mutationRate);
		//SuviverNum
		GeneManager.param.surviverNum = 5;
		SuviverNumValue.text = GeneManager.param.surviverNum.ToString ();
		//SuviverNumSlider.maxValue = 30;
		//SuviverNumSlider.minValue = 1;
		//SuviverNumSlider.value = GeneManager.param.surviverNum;
		//FPG
		GeneManager.param.framePerGene = 5;
		FPGValue.text = GeneManager.param.framePerGene.ToString ();
		//FPGSlider.maxValue = 30;
		//FPGSlider.minValue = 1;
		//FPGSlider.value = GeneManager.param.framePerGene;
		//CrossMode
		GeneManager.param.crossingMode=1;
		CrossDropdown.value = GeneManager.param.crossingMode;
		//SelectMode
		GeneManager.param.selectionMode=1;
		SelectDropdown.value = GeneManager.param.selectionMode;
		//Difficulty
		GeneManager.param.difficulty=1;
		DifficultyDropdown.value = GeneManager.param.difficulty;
		*/
		PatternDropdown.value = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	public void changeGroupSize()
	{
		GeneManager.param.playerNum=(int)GroupSizeSlider.value;
		GroupSizeValue.text = GeneManager.param.playerNum.ToString ();
		PatternDropdown.value = 3;
	}
	public void changeMutationRate()
	{
		GeneManager.param.mutationRate = (float)Math.Pow (10.0d, MutationRateSlider.value);
		MutationRateValue.text = GeneManager.param.mutationRate.ToString("#0.##%");
		PatternDropdown.value = 3;
	}
	public void changeSurviverNum()
	{
		GeneManager.param.surviverNum=(int)SuviverNumSlider.value;
		SuviverNumValue.text = GeneManager.param.surviverNum.ToString ();
		PatternDropdown.value = 3;
	}
	public void changeFPG()
	{
		GeneManager.param.framePerGene=(int)FPGSlider.value;
		GeneManager.param.playFrame = 750 / GeneManager.param.framePerGene;
		FPGValue.text = GeneManager.param.framePerGene.ToString();
		PatternDropdown.value = 3;
	}
	public void changeCrossMode()
	{
		GeneManager.param.crossingMode=CrossDropdown.value;
		PatternDropdown.value = 3;
	}
	public void changeSelectMode()
	{
		GeneManager.param.selectionMode=SelectDropdown.value;
		PatternDropdown.value = 3;
	}
	public void changeDifficulty()
	{
		GeneManager.param.difficulty=DifficultyDropdown.value;
		PatternDropdown.value = 3;
	}
	public void changePattern()
	{
		switch (PatternDropdown.value) {
		case 0:
			GeneManager.param.playerNum = 50;
			GroupSizeValue.text = GeneManager.param.playerNum.ToString ();
			GroupSizeSlider.value = (float)GeneManager.param.playerNum;

			GeneManager.param.mutationRate = 0.01f;
			MutationRateValue.text = GeneManager.param.mutationRate.ToString ("#0.##%");
			MutationRateSlider.value = (float)Math.Log10 (GeneManager.param.mutationRate);

			GeneManager.param.surviverNum = 5;
			SuviverNumValue.text = GeneManager.param.surviverNum.ToString ();
			SuviverNumSlider.value = GeneManager.param.surviverNum;

			GeneManager.param.framePerGene = 5;
			FPGValue.text = GeneManager.param.framePerGene.ToString ();
			FPGSlider.value = GeneManager.param.framePerGene;

			GeneManager.param.crossingMode = 1;
			CrossDropdown.value = GeneManager.param.crossingMode;

			GeneManager.param.selectionMode = 1;
			SelectDropdown.value = GeneManager.param.selectionMode;

			GeneManager.param.difficulty = 1;
			DifficultyDropdown.value = GeneManager.param.difficulty;
			PatternDropdown.value = 0;
			break;
		}
	}
}
