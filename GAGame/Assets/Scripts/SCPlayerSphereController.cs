using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SCPlayerSphereController : MonoBehaviour {
    void OnTriggerEnter (Collider hit)
    {
        if (hit.CompareTag ("Enemy"))
        {
            // transformからたどる場合は出てくるものもtransformだからgameObjectを取らないといけない
            // SendMessageは重いので頻繁に呼ぶものには使わない
            // gameObject.transform.parent.gameObject.SendMessage("Die");
            gameObject.transform.parent.GetComponent<SCPlayerController>().Die();
        }
    }
}
