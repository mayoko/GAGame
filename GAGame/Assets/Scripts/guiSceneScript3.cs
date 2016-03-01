using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class guiSceneScript3 : MonoBehaviour {
	//public Canvas canvas;
	public Text text;
	public Slider slider;

	// Use this for initialization
	//void Start () {
		//canvas.enabled = false;
		//text.text = "ready...";
	//}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
			//canvas.enabled = true;
		}
	}

	public void OnsliderChanged ()
	{
		text.text = "フレーム数 = " + slider.value;
	}
}