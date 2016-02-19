using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

	float speed = 6;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D> ().velocity = transform.forward * speed;
		transform.rotation = new Quaternion (0, 0, transform.rotation.z, transform.rotation.w);
	}
	
	void OnTriggerEnter2D( Collider2D other ) {
		
		if (other.gameObject.tag != "Enemy" && !other.isTrigger) {
			Health healthComponent = other.gameObject.GetComponent<Health> ();
			if (healthComponent != null) {
				healthComponent.takeHit ();
			}

			Destroy (gameObject);
		}


	}

}
