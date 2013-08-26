using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	tk2dSpriteAnimator anim;
	public StuffuvController stuffuvSpawner;
	float moveX = 0.0f;
	float moveY = 0.0f;
	float speed = 100.0f;
	bool lockMovement = false;
	string toStand = "stand_down";
	public PlayerSounder playerSounds;
	public ShadowFollow darkness;
	
	public GameObject spawner;
	
	private bool isAwake;
	
	private bool onTeleporter;
	private TeleporterController tp;
	
	private MatchColor heldMatch;
	
	private int white, red, blue, green;
	
	void Awake() {
		anim = GetComponent<tk2dSpriteAnimator>();
		anim.Play("stand_down");
		onTeleporter = false;
		isAwake = false;
		ResetMatches();
		Application.targetFrameRate = 60;
		//Spawner s = spawner.GetComponent<Spawner>();
		
	}
	
	public void GoDaytime() {
		darkness.DisableDarkness();
		isAwake = true;
	}
	
	public void ResetMatches() {
		Debug.Log("Match resets");
		heldMatch = MatchColor.White;
		white = red = blue = green = 0;	
	}
	
	public void SetSpawner(GameObject s) {
		spawner = s;
	}
	
	public void KillPlayer() {
		white = red = blue = green = 0;
		heldMatch = MatchColor.Invalid;
		Spawner s = spawner.GetComponent<Spawner>();
		if(s) {
			s.Respawn(this);
		}
	}
	
	public void HandleMatchFromRespawn() {
		heldMatch = MatchColor.White;	
		darkness.MaxLight();
	}
	
	public void SnuffMatch() {
		Debug.Log("Match snuffed");
		heldMatch = MatchColor.Invalid;
	}
	
	public bool HoldingMatch() {
		return heldMatch != MatchColor.Invalid;
	}
	
	public MatchColor GetLitMatchColor() {
		return heldMatch;
	}
	
	public void EmptyMatches() {
		white = red = blue = green = 0;	
	}
	
	public void HandleMatchLight(MatchColor color) {
		heldMatch = color;
		if(onTeleporter) {
			tp.DoTeleport(this, color);
		}
	}
	
	public int WhiteMatches() {
		return white;
	}
	
	public int RedMatches() {
		return red;	
	}
	
	public int GreenMatches() {
		return green;		
	}
	
	public int BlueMatches() {
		return blue;	
	}
	
	public void UseMatch(MatchColor color) {
		switch(color) {
		case MatchColor.White:
			white--;
			break;
		case MatchColor.Red:
			red--;
			break;
		case MatchColor.Blue:
			blue--;
			break;
		case MatchColor.Green:
			green--;
			break;
		}
	}
	
	public bool HasAnyMatches() {
		int sum = white + red + blue + green;
		return sum != 0;
	}
	
	void OnTriggerStay(Collider other) {
		tp = other.gameObject.GetComponent<TeleporterController>();
		if(tp) {
			onTeleporter = true;
			return;
		}
	}
	
	void OnTriggerExit(Collider other) {
		tp = other.gameObject.GetComponent<TeleporterController>();
		if(tp) {
			onTeleporter = false;
			return;
		}
	}

	void OnTriggerEnter(Collider other) {
		tp = other.gameObject.GetComponent<TeleporterController>();
		if(tp) {
			onTeleporter = true;
			return;
		}
		MatchBook mb = other.gameObject.GetComponent<MatchBook>();
		if(mb) {
			bool pickedup = mb.PickUp();
			if(pickedup) {
				Debug.Log("Picked up a matchbook");	
				MatchColor col = mb.GetColor();
				switch(col) {
				case MatchColor.White:
					white += mb.quantity;
					break;
				case MatchColor.Red:
					red += mb.quantity;
					break;
				case MatchColor.Blue:
					blue += mb.quantity;
					break;
				case MatchColor.Green:
					green += mb.quantity;
					break;
				}
			}
		}
		Stuffuv stuffuv = other.gameObject.GetComponent<Stuffuv>();
		if(stuffuv) {
			KillPlayer();
		}
	}
	
	void Update() {
		if(Debug.isDebugBuild) {
			if(Input.GetKeyDown(KeyCode.G)) {
				isAwake = !isAwake;
			}
			if(Input.GetKeyDown(KeyCode.X)) {
				KillPlayer();	
			}
			if(Input.GetKeyDown(KeyCode.Alpha9)) {
				white = green = blue = red = 99;	
			}
		}
		if(Input.GetKeyDown(KeyCode.Escape)) {
			KillPlayer();	
		}
		if(lockMovement) return;
		float x = 0.0f;
		float y = 0.0f;
		
		bool UP = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
		bool DOWN = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
		bool LEFT = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
		bool RIGHT = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

		if (RIGHT) x = 1;
		if (LEFT) x = -1;
		if (!RIGHT && !LEFT) x = 0;
		
		if (UP) y = 1;
		if (DOWN) y = -1;
		if (!UP && !DOWN) y = 0;
			
		moveX = x;
		moveY = y;
		string toPlay = toStand;
		bool walking = true;
		if(moveX == 1 && moveY == 0) {
			toPlay = "walk_right";
			toStand = "stand_right";
		} else if(moveX == -1 && moveY == 0) {
			toPlay = "walk_left";
			toStand = "stand_left";
		} else if(moveX == 0 && moveY == 1) {
			toPlay = "walk_up";
			toStand = "stand_up";
		} else if(moveX == 0 && moveY == -1) {
			toPlay = "walk_down";
			toStand = "stand_down";
		} else if(moveX == 1) {
			toPlay = "walk_right";
			toStand = "stand_right";
		} else if(moveX == -1) {
			toPlay = "walk_left";
			toStand = "stand_left";
		} else if (moveY == 1) {
			toPlay = "walk_up";
			toStand = "stand_up";
		} else if(moveY == -1) {
			toPlay = "walk_down";
			toStand = "stand_down";
		} else {
			walking = false;
			toPlay = toStand;
		}
		
		if(isAwake) {
			toPlay = "awake_" + toPlay;
		}
		anim.Play(toPlay);
		anim.AnimationCompleted = null;
		
		if(walking) {
			playerSounds.PlayWalkSound();
		} else {
			playerSounds.StopWalkSound();	
		}
		
	}

	void FixedUpdate () {
		if(moveX != 0) {
			rigidbody.AddForce(new Vector3(moveX * speed, 0, 0) * Time.deltaTime, ForceMode.Impulse);
		}
		if(moveY != 0) {
			rigidbody.AddForce(new Vector3(0, moveY * speed, 0) * Time.deltaTime, ForceMode.Impulse);
		}
	}
	
}
