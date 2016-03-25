using UnityEngine;
using System.Collections;

public class SCEnemyController : MonoBehaviour {
	public float enemySpeed;

	void Update () {
		transform.Translate (-1 * 0.0165f * enemySpeed, 0, 0);
	}

	void OnTriggerEnter (Collider hit)
	{
		if (hit.CompareTag ("EnemyWall"))
		{
			Destroy (gameObject);
		}
	}
}
