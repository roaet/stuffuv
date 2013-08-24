using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	tk2dSprite sprite;
	float moveX = 0.0f;
	float moveY = 0.0f;
	float speed = 0.25f;
	bool lockMovement = true;
	
	void Awake() {
		sprite = GetComponent<tk2dSprite>();

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
	}

	void FixedUpdate () {
		transform.Translate(moveX * speed, moveY * speed, 0);
	}

	void OnTriggerEnter(Collider other) {
		Destroy( other.gameObject );
	}
}
