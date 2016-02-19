using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {
	int x,y;
	public bool isInitialRoom = false;
	//public int xScale = 20, yScale = 10;
	RoomFader fader;

	public bool doorsOpen = true;
	private bool roomActivated = false;

	//An active list of all the players in this current room
	public List<GameObject> playersInRoom = new List<GameObject>();

	// Use this for initialization
	void Start () {
		transform.FindChild ("Objects").gameObject.SetActive (false);
		roomActivated = false;

		fader = transform.FindChild ("Fader").GetComponent<RoomFader> ();
		if (isInitialRoom) {
			Destroy (fader.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D( Collider2D other ) {
		if (other.gameObject.tag == "Player") {

			//Fade in the room if we can
			if (fader != null) {
				fader.fading = true;
			}

			if (!roomActivated) {
				roomActivated = true;
				transform.FindChild ("Objects").gameObject.SetActive (true);
				BroadcastMessage ("RoomStart");
			}

			playersInRoom.Add (other.gameObject);

		}
	}

	void OnTriggerExit2D( Collider2D other ) {
		if (other.gameObject.tag == "Player") {
			playersInRoom.Remove (other.gameObject);
		}
	}




}
