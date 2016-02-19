using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public EnemyRoom room;

	public float speed;

	Rigidbody2D rb;

	public PlayerControl target;

	public GameObject bullet;

	//shooting stuff
	float cooldown = 1.2f;
	float curCooldown;
	bool canShoot = false;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		AcquireTarget ();

		curCooldown = Random.Range(0,cooldown);
	}

	// Update is called once per frame
	void Update () {

		//Movement
		Vector2 vel = Vector2.zero;

		if (target != null) {
			if (target.transform.position.x > transform.position.x) {
				vel.x = speed;
			} else {
				vel.x = -speed;
			}

			if (target.transform.position.y > transform.position.y) {
				vel.y = speed;
			} else {
				vel.y = -speed;
			}
		} else {
			AcquireTarget ();
		}

		if (Vector2.Distance (transform.position, target.transform.position) > 10) {
			AcquireTarget ();
		}

		rb.velocity = vel;

		//Shooting
		curCooldown-=Time.deltaTime;
		if (curCooldown <= 0) {
			print ("cooldown 0");
			Shoot ();
			curCooldown = cooldown;
		}


	}

	void AcquireTarget() {
		if (room.playersInRoom.Count > 0) {
			target = room.playersInRoom [Random.Range (0, room.playersInRoom.Count - 1)].GetComponent<PlayerControl> ();
		} else {
			target = null;
		}
	}

	void Die() {
		room.enemiesInRoom--;
		Destroy (gameObject);
	}

	void Shoot() {
		print ("Shoot");
		GameObject b = Instantiate (bullet, transform.position, Quaternion.identity) as GameObject;
		b.transform.LookAt (target.transform);
	}
}
