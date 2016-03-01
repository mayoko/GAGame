﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class gotogame : MonoBehaviour {
    public UnityEngine.UI.Slider playerNumBar;
    public UnityEngine.UI.Slider mutationRateBar;
    public UnityEngine.UI.Slider frameCountBar;
    // シーンの移動(引数と入して渡されたシーンを呼ぶ)
    public void toMainScene()
	{
        GeneManager.param.mutationRate = mutationRateBar.value;
        GeneManager.param.playerNum = (int)(playerNumBar.value);
        GeneManager.param.playFrame = (int)(frameCountBar.value);
        GeneManager.init();
        SceneManager.LoadScene("SakeruCheese");
    }

}