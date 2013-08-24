using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

	tk2dCamera cam;
	public Transform target;
	public float followSpeed = 1.0f;

	public float minZoomSpeed = 20.0f;
	public float maxZoomSpeed = 40.0f;

	public float maxZoomFactor = 0.6f;

	void Awake() {
		cam = GetComponent<tk2dCamera>();
	}

	void FixedUpdate() {
		Vector3 start = transform.position;
		Vector3 end = target.position;
		end.z = start.z;
		transform.position = end;
	}
}
