using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject[] PotentialEnemyTypes;

	public EnemyRoom enemyRoom;

	// Use this for initialization
	void Start () {
		Destroy (GetComponent<SpriteRenderer> ());
	}

	public void spawnEnemy() {
		print ("SpawnEnemy");
		GameObject go = Instantiate (PotentialEnemyTypes [Random.Range (0, PotentialEnemyTypes.Length)], transform.position, Quaternion.identity) as GameObject;
		print (go);
		Enemy thisEnemy = go.GetComponent<Enemy> ();
		thisEnemy.room = enemyRoom;

		Destroy (gameObject);
	}
}
