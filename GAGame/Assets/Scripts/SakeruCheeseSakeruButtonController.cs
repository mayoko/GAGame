using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SakeruCheeseSakeruButtonController : MonoBehaviour {
    public void GoToGeneCalc()
    {
        SceneManager.LoadScene("GeneCalc");
    }
}
