using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class gotogame : MonoBehaviour {
    public UnityEngine.UI.Slider playerNumBar;
    public UnityEngine.UI.Slider mutationRateBar;
    // シーンの移動(引数と入して渡されたシーンを呼ぶ)
    public void toMainScene()
	{
        GeneManager.param.mutationRate = mutationRateBar.value;
        GeneManager.param.playerNum = (int)(playerNumBar.value);
        GeneManager.init();
        SceneManager.LoadScene("SakeruCheese");
    }

}