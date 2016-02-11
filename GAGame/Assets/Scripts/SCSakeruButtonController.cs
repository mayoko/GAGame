using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SCSakeruButtonController : MonoBehaviour {
    public void GoToGeneCalc()
    {
        SceneManager.LoadScene("GeneCalc");
    }
}
