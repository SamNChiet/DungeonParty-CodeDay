using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	

	//Public Properties
	public Direction dir = Direction.Up;
	public bool open = true;
	public Sprite closeDoor,openDoor;

	public Room room;




	//Private Properties
	private float distToTeleport = 3;
	private SpriteRenderer spriteRenderer;

	void Start() {
		
		spriteRenderer = GetComponent<SpriteRenderer> ();
		open = true;
		OpenDoor ();

	}

	void Update() {
		if (room == null) {
			print ("no room");
			return;
		}
		if (room.doorsOpen != open) {
			open = room.doorsOpen;
			if (open) {
				OpenDoor ();
			} else {
				CloseDoor ();
			}
		}
	}

	void OnCollisionEnter2D( Collision2D other ) {
		print ("Collision Enter");
		//If we need to move the player
		if (other.gameObject.tag == "Player" && open) {
			print ("Player and Open");
			Vector2 newPos = other.transform.position;

			switch (dir) {
			case Direction.Down:
				newPos.y -= distToTeleport;
				break;
			case Direction.Up:
				newPos.y += distToTeleport;
				break;
			case Direction.Left:
				newPos.x -= distToTeleport;
				break;
			case Direction.Right:
				newPos.x += distToTeleport;
				break;
			}

			other.gameObject.transform.position = newPos;
		}
	}

	void OpenDoor() {
		open = true;
		spriteRenderer.sprite = openDoor;
	}

	void CloseDoor() {
		open = false;
		spriteRenderer.sprite = closeDoor;
	}

	public enum Direction { Up,Down,Left,Right };
}


