using UnityEngine;
using System.Collections;

public class PramaeterManager : MonoBehaviour {
	public UnityEngine.UI.Text GroupSizeValue;
	public UnityEngine.UI.Slider GroupSizeSlider;

	// Use this for initialization
	void Start () {
		GroupSizeValue.text = GeneManager.param.playerNum.ToString ();
		GroupSizeSlider.value = GeneManager.param.playerNum;
		GroupSizeSlider.maxValue = 100;
		GroupSizeSlider.minValue = 20;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	public void changeGroupSize()
	{
		GeneManager.param.playerNum=(int)GroupSizeSlider.value;
		GroupSizeValue.text = GeneManager.param.playerNum.ToString ();
	}
}
