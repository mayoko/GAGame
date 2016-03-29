using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SCSakeruButtonController : MonoBehaviour {
    public void GoToGeneCalc()
    {
//        Application.LoadLevel("SakeruCheese");
        SceneManager.LoadScene("GeneCalc");
    }
}
