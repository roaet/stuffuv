using UnityEngine;
using System.Collections;

public class ShadowFollow : MonoBehaviour {

	tk2dCamera cam;
	tk2dSprite sprite;
	public Transform target;
	public float followSpeed = 1.0f;
	
	private int minDarkness = 1;
	private int maxDarkness = 6;
	private int darkLevel;
	
	void Awake() {
		darkLevel = maxDarkness;
		sprite = GetComponent<tk2dSprite>();
	}
	
	void Update() {
		if(!Debug.isDebugBuild) return;
		if(Input.GetKeyDown(KeyCode.Q)) {
			if(darkLevel > minDarkness) darkLevel--;
		}
		else if(Input.GetKeyDown(KeyCode.E)) {
			if(darkLevel < maxDarkness) darkLevel++;
		}
		sprite.spriteId = darkLevel;
		
	}

	void FixedUpdate() {
		Vector3 start = transform.position;
		Vector3 end = target.position;
		end.z = start.z;
		transform.position = end;
	}
}
