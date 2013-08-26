using UnityEngine;
using System.Collections;

public class BorderController : MonoBehaviour {

	public Transform target;
	private float borderOffsetX;
	
	void Awake() {
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
