using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

	public AudioClip shoot;
	public AudioClip doorUnlock;


	AudioSource shootSource;
	AudioSource unlockSource;

	// Use this for initialization
	void Start () {
		shootSource = gameObject.AddComponent<AudioSource> ();
		shootSource.playOnAwake = false;

		unlockSource = gameObject.AddComponent<AudioSource> ();
		unlockSource.playOnAwake = false;
	}

	public void PlayShootSound() {
		shootSource.PlayOneShot (shoot);
	}

	public void PlayDoorSound() {
		unlockSource.PlayOneShot (doorUnlock);
	}
}
