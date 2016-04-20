using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AMSkipCheckboxController : MonoBehaviour {
    public AMTestAnimation ta;
    GeneManager.ViewParam vp = GeneManager.viewParam;

	// Use this for initialization
	void Start () {
        GetComponent<Toggle>().isOn = vp.isSkipping;
        if (vp.isSkipping) ta.Skip(); // スキップモードなら即スキップ
        GetComponent<Toggle>().onValueChanged.AddListener(OnValueChanged); // 登録しないと呼ばれない
    }

    void OnValueChanged(bool toggle) {
        vp.isSkipping = toggle;
        if (toggle) ta.Skip();
	}
}
