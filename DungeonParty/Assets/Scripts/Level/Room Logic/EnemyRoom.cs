using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyRoom : MonoBehaviour {
	public Room room;

	public EnemySpawner[] enemySpawners;
	public int enemiesInRoom;

	public List<GameObject> playersInRoom = new List<GameObject>();


	// Use this for initialization
	void Start () {
		room = GetComponent<Room> ();
		enemiesInRoom = enemySpawners.Length;

		foreach (EnemySpawner spawner in enemySpawners) {
			spawner.enemyRoom = this;
		}

	}

	void RoomStart() {
		print ("RoomStart");
		room.doorsOpen = false;

		foreach (EnemySpawner spawner in enemySpawners) {
			spawner.spawnEnemy ();
		}
	}

	// Update is called once per frame
	void Update () {
		playersInRoom = room.playersInRoom;

		if (enemiesInRoom <= 0 && !room.doorsOpen) {
			room.doorsOpen = true;
			GameObject.FindObjectOfType<Sound> ().PlayDoorSound ();
		}
	}
}
