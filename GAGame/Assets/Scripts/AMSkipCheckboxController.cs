using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AMSkipCheckboxController : MonoBehaviour {
    GeneManager.ViewParam vp = GeneManager.viewParam;

	// Use this for initialization
	void Start () {
        GetComponent<Toggle>().onValueChanged.AddListener(OnValueChanged); // 登録しないと呼ばれない
        GetComponent<Toggle>().isOn = vp.isSkipping;
    }

    void OnValueChanged(bool toggle) {
        vp.isSkipping = toggle;
	}
}
