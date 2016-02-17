using UnityEngine;
using System.Collections;

public class SCEnemyController : MonoBehaviour {
	private float enemySpeed; //publicからprivateに変更.そうでないと全部の敵のスピードが等しくなる
	public SCGameController refSCGC;

	void Start(){
		refSCGC = GameObject.Find ("GameController").GetComponent<SCGameController>();
		enemySpeed = refSCGC.enemyInfo [refSCGC.enemyNum - 1] [2]; //呼び出すタイミングの問題か-1が必要
	}

	void Update () {
		transform.Translate (-1 * 0.0165f * enemySpeed, 0, 0);
	}
}
