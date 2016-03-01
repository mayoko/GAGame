using UnityEngine;
using System.Collections;

public class SCEnemyController : MonoBehaviour {
	public float enemySpeed;
	public float enemyDirection;

	void Update () {
		transform.Translate (-1 * 0.0165f * enemySpeed * enemyDirection, 0, 0);
	}
}
