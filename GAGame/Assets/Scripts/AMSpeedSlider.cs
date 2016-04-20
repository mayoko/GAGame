using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class AMSpeedSlider : MonoBehaviour {
    GeneManager.ViewParam vp = GeneManager.viewParam;

	void Start ()
    {
        GetComponent<Slider>().onValueChanged.AddListener(OnValueChanged);
        GetComponent<Slider>().value = SpeedToValue(vp.playSpeed);
	}

    void OnValueChanged (float value)
    {
        vp.playSpeed = ValueToSpeed(value);
        transform.FindChild("Text").GetComponent<Text>().text = "x" + vp.playSpeed;
    }

    float ValueToSpeed (float value)
    {
        return (float)Math.Pow(2, value);
    }
    float SpeedToValue (float speed)
    {
        return (float)Math.Log(speed, 2);
    }
}
