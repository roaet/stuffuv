using UnityEngine;
using System.Collections;

public class BorderController : MonoBehaviour {

	tk2dSprite sprite;
	public Transform target;
	
	void Awake() {
		sprite = GetComponent<tk2dSprite>();
	}
	
	
	
	void Update() {
		
	}

	void FixedUpdate() {
		Vector3 start = transform.position;
		Vector3 end = target.position;
		end.z = start.z;
		transform.position = end;
	}
}
