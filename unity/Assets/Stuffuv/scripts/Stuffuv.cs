using UnityEngine;
using System.Collections;

public class Stuffuv : MonoBehaviour {
	
	public Transform target;
	public ShadowFollow darkness;
	public float followSpeed = 4.0f;

	public float minZoomSpeed = 20.0f;
	public float maxZoomSpeed = 40.0f;

	public float maxZoomFactor = 0.6f;
	
	private float PUSH_OUT_DELAY_MIN = 0.5f;
	private float PUSH_OUT_DELAY_MAX = 1.5f;
	private float pushOutDelay;
	private float pushOutStart;
	private bool pushedOut;
	
	private int GROWL_ONE_OUT_OF = 5;
	private int GROWL_TIME_CHECK = 5;
	private float growlCheck;
	private bool checkGrowl;
	
	private int DARK_LONG_ENOUGH  = 1;
	private float darkTimer;
	private bool checkDark;
	
	public AudioSource growl;
	public AudioSource scream;

	// Use this for initialization
	void Awake () {
		pushedOut = false;
		checkGrowl = true;
		checkDark = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(checkGrowl) {
			checkGrowl = false;
			growlCheck = Time.time;
		}
		bool isDark = darkness.GetDarkLevel() == 1;
		if(isDark && checkDark) {
			darkTimer = Time.time;
			checkDark = false;
		}
		if(!isDark) {
			checkDark = true;	
		}
	}
	
	void FixedUpdate() {
		Vector3 start = transform.position;
		float d = Vector3.Distance(start, target.position);
		float darkDistance = darkness.GetFollowDistance();
		
		bool totalDark = false;
		if(darkness.GetDarkLevel() == 1 && darkTimer + DARK_LONG_ENOUGH < Time.time) {
			totalDark = true;
		} 
		int velocityMultiplier = 1;
		
		bool doPushOut = false;
		if(pushedOut && pushOutStart + pushOutDelay < Time.time) {
			pushedOut = false;
		}
		if(totalDark) {
			velocityMultiplier = 4;	
		}
		if(d > 35) {
			velocityMultiplier = 5;
		}
		if(d > 50) {
			velocityMultiplier = 15;
		}
		
		Vector3 end = Vector3.MoveTowards(start, target.position, velocityMultiplier * followSpeed * Time.deltaTime);
		
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
		
		if(growlCheck + GROWL_TIME_CHECK < Time.time) {
			int r = Random.Range(0, GROWL_ONE_OUT_OF + darkness.GetDarkLevel() - 1);
			if(r == 0) {
				if(totalDark) {
					scream.Play();
				} else	growl.Play();
			}
			
			checkGrowl = true;
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if(!other) return;
		Player player = other.gameObject.GetComponent<Player>();
		if(player) player.KillPlayer();
	}
}
