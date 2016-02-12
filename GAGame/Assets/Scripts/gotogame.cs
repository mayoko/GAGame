using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class gotogame : MonoBehaviour {

	// シーンの移動(引数と入して渡されたシーンを呼ぶ)
	public void toMainScene()
	{
        SceneManager.LoadScene("SakeruCheese");
	}
}