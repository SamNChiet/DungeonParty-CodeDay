using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

	public enum Direction { Up = 0,Down,Left,Right };

	public Direction dir;

	float speed = 5;


	// Use this for initialization
	void Start () {
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		Vector2 vel = Vector2.zero;

		switch (dir) {
		case Direction.Down:
			vel.y = -speed;
			break;
		case Direction.Left:
			vel.x = -speed;
			break;
		case Direction.Up:
			vel.y = speed;
			break;
		case Direction.Right:
			vel.x = speed;
			break;
		}

		rb.velocity = vel;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D( Collider2D other ) {

		//The bullet object is on the players layer and as such does not collide with other players.
		if (other.gameObject.tag != "Player" && other.isTrigger == false ) {
			Health healthComponent = other.gameObject.GetComponent<Health> ();
			if (healthComponent != null) {
				healthComponent.takeHit ();
			}
			Destroy (gameObject);
		}


	}


}
