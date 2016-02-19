using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
 * 
 * 
 * 
 * Fill a 2D array with zeros (type: integer, 0 means nothing to place there)
Set the place of the starting room (for example at [5,5])
Select a random array element in this 2 dimensional array.
If the selected element has a room next to it, make a room there!
(fill the array element with not zero, I think different rooms will have different numbers, 
0 will be the starter room where the player will spawn, 1 will be one room type, 2 will be an other,...)
If there is no room next to the selected element, do step 3 again. 
We shouldn't make room if the selected empty place has 2 or more neighbours! 
This will make the level a little bit different (figure 1, 2nd image)
Do it until we want: if we want 15 room we need to do step 3 until we have 15 rooms (don't forget, we already have a starter room!)
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * */



public class NewGenerator : MonoBehaviour {

	//Enum
	public enum RoomType { InitialRoom, EnemyRoom, BossRoom }

	//Generation Params
	public int size = 10;
	public int roomsToGen = 20;

	//Room Object
	public GameObject initialRoom;
	public GameObject[] enemyRooms;

	//Room Information
	public Vector2 roomScale = new Vector2(20,10);

	//Map
	Room[,] map;
	List<Coordinate> coords = new List<Coordinate>();

	// Use this for initialization
	void Start () {

		map = new Room[size,size];

		if (roomsToGen > size * size) {
			print ("More Rooms Than Cells");
		} else { 
			Generate ();
			buildDoors ();
		}

	}
	void Generate () {

		//Create Inital Room
		Room initialRoom = CreateRoomAtCoordinate(RoomType.InitialRoom, size/2,size/2);
		initialRoom.isInitialRoom = true;
		coords.Add (new Coordinate(size / 2, size / 2));

		int generatedRooms = 1;

		//Generate Other Rooms Until Done
		while (generatedRooms < roomsToGen) {
			int x = Random.Range (0, size - 1);
			int y = Random.Range (0, size - 1);

			//If this coordinate is empty
			if (map [x, y] == null) {

				//If there's a room adjacent to this space
				if (roomAt (x + 1, y) || roomAt (x - 1, y) || roomAt (x, y + 1) || roomAt (x, y - 1)) {

					CreateRoomAtCoordinate (RoomType.EnemyRoom, x, y);
					coords.Add(new Coordinate(x,y));
					generatedRooms++;

				}
			}

		}
			
	}

	void buildDoors() {
		//foreach (Room room in 
		foreach (Coordinate coord in coords) {
			int x = coord.x;
			int y = coord.y;

			Room thisRoom = map [coord.x, coord.y];
			Transform roomBlocks = thisRoom.transform.FindChild("Blocks");
			Transform roomDoors = thisRoom.transform.FindChild ("Doors");

			//Disable all doors
			roomDoors.FindChild ("Right").gameObject.SetActive (false);
			roomDoors.FindChild ("Left").gameObject.SetActive (false);
			roomDoors.FindChild ("Top").gameObject.SetActive (false);
			roomDoors.FindChild ("Bottom").gameObject.SetActive (false);


			//If there is a room to our right
			if (roomAt (x + 1, y)) { 
				roomBlocks.FindChild ("Right").gameObject.SetActive (false);

				Door door = roomDoors.FindChild ("Right").GetComponent<Door> ();
				door.gameObject.SetActive (true);
				door.room = thisRoom;

			}

			//If there is a room to our left
			if (roomAt (x - 1, y)) { 
				roomBlocks.FindChild ("Left").gameObject.SetActive (false);

				Door door = roomDoors.FindChild ("Left").GetComponent<Door> ();
				door.gameObject.SetActive (true);
				door.room = thisRoom;

			}

			//If there is a room above us
			if (roomAt (x, y + 1)) { 
				roomBlocks.FindChild ("Top").gameObject.SetActive (false);

				Door door = roomDoors.FindChild ("Top").GetComponent<Door> ();
				door.gameObject.SetActive (true);
				door.room = thisRoom;

			}

			//If there is a room below us
			if (roomAt (x, y - 1)) { 
				roomBlocks.FindChild ("Bottom").gameObject.SetActive (false);

				Door door = roomDoors.FindChild ("Bottom").GetComponent<Door> ();
				door.gameObject.SetActive (true);
				door.room = thisRoom;

			}
		}
	}

	bool coordInBounds( int x, int y ) {
		return !(x < 0 || x > size - 1 || y < 0 || y > size - 1);
	}

	bool roomAt( int x, int y ) {
		return (coordInBounds (x, y) && map [x, y] != null);
	}

	Room CreateRoomAtCoordinate( RoomType type, int x, int y ) {
		//Generate Room Position
		Vector2 roomPos = new Vector2 (x*roomScale.x, y*roomScale.y);
		roomPos.x -= size / 2 * roomScale.x;
		roomPos.y -= size / 2 * roomScale.y;

		//Get Room Type
		GameObject roomToGenerate = null;;

		switch (type) {
		case RoomType.InitialRoom:
			roomToGenerate = initialRoom;
			break;
		case RoomType.EnemyRoom:
			roomToGenerate = enemyRooms [Random.Range (0, enemyRooms.Length)];
			break;
		case RoomType.BossRoom:
			//Put Boss Room Here
			break;
		}

		//Create a room
		GameObject roomGO = Instantiate (roomToGenerate, roomPos, Quaternion.identity) as GameObject;
		Room thisRoom = roomGO.GetComponent<Room> ();
		map [x, y] = thisRoom;
		return thisRoom;
	}
}

//Integer Coordinate
public class Coordinate { public int x,y; public Coordinate(int _x, int _y) { x = _x; y = _y; } }
