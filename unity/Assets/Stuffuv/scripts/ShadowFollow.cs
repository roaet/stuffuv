using UnityEngine;
using System.Collections;

public class ShadowFollow : MonoBehaviour {

	tk2dCamera cam;
	tk2dSprite sprite;
	public Transform target;
	
	private int minDarkness = 1;
	private int maxDarkness = 6;
	private float FLASH_TIME = 0.01f;
	private float LIGHT_SPILL = 0.1f;
	private float MATCH_DEATH = 20.0f;
	private int FLASH_LEVEL = 4;
	private int darkLevel;
	private bool doFlash;
	private bool maxLight;
	private float flashTimeStart;
	private float maxTimeStart;
	private float deathTimeSteps;
	private float litMatchTimeStart;
	
	void Awake() {
		darkLevel = minDarkness;
		sprite = GetComponent<tk2dSprite>();
		doFlash = false;
		maxLight = true;
		deathTimeSteps = MATCH_DEATH/maxDarkness;
	}
	
	public int GetDarkLevel() {
		return darkLevel;
	}
	
	public void FlashDarkness() {
		if(maxLight || darkLevel > FLASH_LEVEL || doFlash) return;
		doFlash = true;
		flashTimeStart = Time.time;
	}
	
	public void MaxLight() {
		if(maxLight) return;
		maxLight = true;
		maxTimeStart = Time.time;
	}
	
	
	void Update() {
		if(Debug.isDebugBuild) {
			if(Input.GetKeyDown(KeyCode.Q)) {
				if(darkLevel > minDarkness) darkLevel--;
			}
			else if(Input.GetKeyDown(KeyCode.E)) {
				if(darkLevel < maxDarkness) darkLevel++;
			}
		}
		if(litMatchTimeStart + deathTimeSteps < Time.time) {
			if(darkLevel > minDarkness) {
				darkLevel--;
				litMatchTimeStart = Time.time;
			}
		}
		
		if(maxLight) {
			if(maxTimeStart + LIGHT_SPILL < Time.time) {
				if(darkLevel < maxDarkness)	 darkLevel++;
				maxTimeStart = Time.time;
			}
			if(darkLevel == maxDarkness) {
				maxLight = false;
				litMatchTimeStart = Time.time;
			}
		}
		sprite.spriteId = darkLevel;
		if(doFlash) {
			if(darkLevel < maxDarkness)
				sprite.spriteId = Random.Range(darkLevel, FLASH_LEVEL);
			if(flashTimeStart + FLASH_TIME < Time.time)
				doFlash = false;
		}
		
	}

	void FixedUpdate() {
		Vector3 start = transform.position;
		Vector3 end = target.position;
		end.z = start.z;
		transform.position = end;
	}
}
