using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int maxHealth;
	public int curHealth;

	void Start() {
		curHealth = maxHealth;
	}

	public void takeHit() {
		curHealth--;
		if (curHealth <= 0) {
			BroadcastMessage ("Die");
		}
	}
}
