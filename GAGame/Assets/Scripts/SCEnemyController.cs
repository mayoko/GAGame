using UnityEngine;
using System.Collections;

public class SCEnemyController : MonoBehaviour {
	public static float enemySpeed = 1;

	void Update () {
		transform.Translate (-1 * 0.0165f * enemySpeed, 0, 0);
	}
}
