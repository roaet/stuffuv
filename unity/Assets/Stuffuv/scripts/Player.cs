using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	tk2dSprite sprite;
	tk2dSpriteAnimator anim;
	float moveX = 0.0f;
	float moveY = 0.0f;
	float speed = 0.10f;
	bool lockMovement = false;
	string toStand = "stand_down";
	
	void Awake() {
		sprite = GetComponent<tk2dSprite>();
		anim = GetComponent<tk2dSpriteAnimator>();
		anim.Play("stand_down");

		Application.targetFrameRate = 60;
	}
	
	void Update() {
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
		
		if(moveX == 1 && moveY == 0) {
			anim.Play("walk_right");
			anim.AnimationCompleted = null;
			toStand = "stand_right";
		} else if(moveX == -1 && moveY == 0) {
			anim.Play("walk_left");
			anim.AnimationCompleted = null;
			toStand = "stand_left";
		} else if(moveX == 0 && moveY == 1) {
			anim.Play("walk_up");
			anim.AnimationCompleted = null;
			toStand = "stand_up";
		} else if(moveX == 0 && moveY == -1) {
			anim.Play("walk_down");
			anim.AnimationCompleted = null;
			toStand = "stand_down";
		} else {
			anim.Play (toStand);	
		}
		
	}

	void FixedUpdate () {
		transform.Translate(moveX * speed, moveY * speed, 0);
	}

	void OnTriggerEnter(Collider other) {
		Destroy( other.gameObject );
	}
}
