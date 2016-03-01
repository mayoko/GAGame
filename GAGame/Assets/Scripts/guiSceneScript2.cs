using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class guiSceneScript2 : MonoBehaviour {
	public Canvas canvas;
	public Text text;
	public Slider slider;

	// Use this for initialization
	void Start () {
		canvas.enabled = false;
		text.text = "ready...";
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
			canvas.enabled = true;
		}
	}

	public void OnsliderChanged ()
	{
		//text.text = "突然変異率 = " + slider.value;
		float x = slider.value;
		x = (int)(x / 0.01f); 
		text.text = "突然変異率 = " + x.ToString() + "%";
	}
}