using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SCPlayerSphereController : MonoBehaviour {
    void OnTriggerEnter (Collider hit)
    {
        if (hit.CompareTag ("Enemy"))
        {
            gameObject.transform.parent.gameObject.SendMessage("Die");
        }
    }
}
