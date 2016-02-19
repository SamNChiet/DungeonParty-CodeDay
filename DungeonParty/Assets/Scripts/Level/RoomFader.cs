using UnityEngine;
using System.Collections;

public class RoomFader : MonoBehaviour {
	

	SpriteRenderer fader;
	float alpha;
	float fadeTime = 0.5f;

	public bool fading = false;

	Color col = new Color(0,0,0,255);


	// Use this for initialization
	void Start () {

		fader = GetComponent<SpriteRenderer> ();
		alpha = 1;
		fader.color = col;

	
	}

	void Update() {

		if (!fading) {
			return;
		}

		alpha -= Time.deltaTime / fadeTime;

		if (alpha <= 0) {
			Destroy (gameObject);
		}

		col.a = alpha;
		fader.color = col;
	}
}
