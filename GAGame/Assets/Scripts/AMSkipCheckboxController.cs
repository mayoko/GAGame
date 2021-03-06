﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AMSkipCheckboxController : MonoBehaviour {
    public AMtest ta;
    GeneManager.ViewParam vp = GeneManager.viewParam;

	// Use this for initialization
	void Start () {
        GetComponent<Toggle>().isOn = vp.isSkipping;
        if (ta && vp.isSkipping) ta.Skip(); // スキップモードなら即スキップ
        GetComponent<Toggle>().onValueChanged.AddListener(OnValueChanged); // 登録しないと呼ばれない
    }

    void OnValueChanged(bool toggle) {
        vp.isSkipping = toggle;
        if (ta && toggle) ta.Skip(); // SakeruCheeseではtaが無い
	}
}
