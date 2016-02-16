using UnityEngine;
using System.Collections;

//C#
public class MenuWindow : MonoBehaviour {
	void OnGUI () {
		// ラベルを表示する
		GUI.Label(new Rect(10,10,100,100), "MenuWindow");
	}
}
