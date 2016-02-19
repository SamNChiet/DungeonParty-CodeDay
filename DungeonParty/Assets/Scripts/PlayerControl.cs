using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	//Player Values
	public float speed = 4;
	public float tightness = 20;

	//Input
	float inputX, inputY;

	//Components
	private Rigidbody2D rb;
	private HFTInput hftInput;
	private HFTGamepad hftGamepad;

	public TextMesh playerName;

	public GameObject bullet;


	//shooting stuff
	public float cooldown = 0.1f;
	float curCooldown;
	int shootDir = 0;

	//Sound
	Sound soundManager;

	// Use this for initialization
	void Start () {

		soundManager = GameObject.FindObjectOfType<Sound> ().GetComponent<Sound> ();

		//HFT Components
		hftInput = GetComponent<HFTInput> ();
		hftGamepad = GetComponent<HFTGamepad> ();

		//Other Components
		rb = GetComponent<Rigidbody2D> ();

		GetComponent<SpriteRenderer> ().color = hftGamepad.Color;

		playerName.text = hftGamepad.Name;

		curCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {

		//Movement
		inputX = Mathf.Lerp (inputX, hftInput.GetAxisRaw ("Horizontal"), tightness * Time.deltaTime);
		inputY = Mathf.Lerp (inputY, -hftInput.GetAxisRaw ("Vertical"), tightness * Time.deltaTime);

		rb.velocity = new Vector2 (inputX, inputY)*speed;

		//Shooting
		curCooldown-=Time.deltaTime;

		if (hftInput.GetButton("Fire1")) {
			if (curCooldown <= 0) {
				curCooldown = cooldown;
				Shoot ();
			}
		}
	}

	void Shoot() {
		shootDir++;
		if (shootDir > 3) {
			shootDir = 0;
		}

		GameObject thisBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
		PlayerBullet pbull = bullet.GetComponent<PlayerBullet> ();


		pbull.dir = (PlayerBullet.Direction)shootDir;

		soundManager.PlayShootSound ();


	}

	void Die() {
		Health h = GetComponent<Health> ();
		h.curHealth = h.maxHealth;
		transform.position = Vector2.zero;
	}
}
