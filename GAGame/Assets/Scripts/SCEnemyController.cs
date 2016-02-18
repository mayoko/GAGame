using UnityEngine;
using System.Collections;

public class SCEnemyController : MonoBehaviour {
	public float enemySpeed;

	void Update () {
		transform.Translate (-1 * 0.0165f * enemySpeed, 0, 0);
	}
}
