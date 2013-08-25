using UnityEngine;
using System.Collections;

public class BorderController : MonoBehaviour {

	tk2dSprite sprite;
	public Transform target;
	private float borderOffsetX;
	
	void Awake() {
		sprite = GetComponent<tk2dSprite>();
		borderOffsetX = 0.5f;
	}
	
	
	
	void Update() {
		
	}

	void FixedUpdate() {
		Vector3 start = transform.position;
		Vector3 end = target.position;
		end.z = start.z;
		end.x -= borderOffsetX;
		transform.position = end;
	}
}
