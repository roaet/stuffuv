using UnityEngine;
using System.Collections;

public class Stuffuv : MonoBehaviour {
	
	public Transform target;
	public ShadowFollow darkness;
	public float followSpeed = 1.0f;

	public float minZoomSpeed = 20.0f;
	public float maxZoomSpeed = 40.0f;

	public float maxZoomFactor = 0.6f;
	
	private float PUSH_OUT_DELAY_MIN = 1.0f;
	private float PUSH_OUT_DELAY_MAX = 3.0f;
	private float pushOutDelay;
	private float pushOutStart;
	private bool pushedOut;

	// Use this for initialization
	void Awake () {
		pushedOut = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate() {
		Vector3 start = transform.position;
		float d = Vector3.Distance(start, target.position);
		float darkDistance = darkness.GetFollowDistance();
		
		Vector3 end = Vector3.MoveTowards(start, target.position, followSpeed * Time.deltaTime);
		bool doPushOut = false;
		if(pushedOut && pushOutStart + pushOutDelay < Time.time) {
			pushedOut = false;
		}
		if(d > 35) {
			end = Vector3.MoveTowards(start, target.position, 5 * followSpeed * Time.deltaTime);
		}
		if(d > 50) {
			end = Vector3.MoveTowards(start, target.position, 15 * followSpeed * Time.deltaTime);
		}
		if(d < darkDistance) {
			pushOutStart = Time.time;
			pushOutDelay = Random.Range (PUSH_OUT_DELAY_MIN, PUSH_OUT_DELAY_MAX);
			end = Vector3.MoveTowards(start, target.position, -10.0f * followSpeed * Time.deltaTime);
			pushedOut = true;
			doPushOut = true;
		}
		if (!pushedOut || doPushOut) {
			end.z = start.z;
			transform.position = end;
		}
	}
}
