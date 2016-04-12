using UnityEngine;
using System.Collections;

public class PramaeterManager : MonoBehaviour {
	public UnityEngine.UI.Text SelectedText;
	public UnityEngine.UI.Text SelectedValue;
	public UnityEngine.UI.Slider SelectedSlider;

	public UnityEngine.UI.Text GroupSizeValue;

	string selected;

	// Use this for initialization
	void Start () {
		GroupSizeValue.text = GeneManager.param.playerNum.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeSelected(string name)
	{
		if (name == selected) {
			return;
		}
		switch (name) 
		{
		case "GroupSize":
			SelectedText.text = "GroupSize";
			selected = "Setting";
			SelectedValue.text = GeneManager.param.playerNum.ToString ();
			SelectedSlider.interactable = true;
			SelectedSlider.value = (float)GeneManager.param.playerNum;
			SelectedSlider.minValue = (float)0;
			SelectedSlider.maxValue = (float)100;
			selected = "GroupSize";
			break;
		}
	}
	public void SliderValueChange()
	{
		switch (selected)
		{
		case "GroupSize":
			GeneManager.param.playerNum = (int)SelectedSlider.value;
			SelectedValue.text = GeneManager.param.playerNum.ToString();
			GroupSizeValue.text = GeneManager.param.playerNum.ToString();
			break;
		}
	}
}
