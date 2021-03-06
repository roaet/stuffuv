﻿using UnityEngine;
using System.Collections;

public class ShadowFollow : MonoBehaviour {

	tk2dCamera cam;
	tk2dSprite sprite;
	public Transform target;
	public Striker striker;
	
	private static int minDarkness = 1;
	private static int maxDarkness = 6;
	private static float FLASH_TIME = 0.01f;
	private static float LIGHT_SPILL = 0.1f;
	private static float LIGHT_CONSUME = 0.1f;
	private float MATCH_DEATH = 10.0f;
	private int FLASH_LEVEL = 4;
	private int darkLevel;
	private bool doFlash;
	private bool maxLight;
	private bool minLight;
	private float flashTimeStart;
	private float maxTimeStart;
	private float minTimeStart;
	private float deathTimeSteps;
	private float litMatchTimeStart;
	
	
	private bool disableDarkness;
	
	void Awake() {
		darkLevel = minDarkness;
		sprite = GetComponent<tk2dSprite>();
		doFlash = false;
		maxLight = true;
		minLight = false;
		deathTimeSteps = MATCH_DEATH/maxDarkness;
		disableDarkness = false;
	}
	
	public static float SnuffTime() {
		return maxDarkness * LIGHT_CONSUME;
	}
	
	public bool IsDarkDisabled() {
		return disableDarkness;	
	}
	
	public int GetDarkLevel() {
		if(disableDarkness) return maxDarkness;
		return darkLevel;
	}
	
	public int GetMaxDarkLevel() {
		return maxDarkness;
	}
	
	public float GetFollowDistance() {
		switch(darkLevel) {
		case 1:
			return 0.0f;
		case 2:
			return 8.0f;
		case 3:
			return 10.0f;
		case 4:
			return 13.0f;
		case 5:
			return 18.0f;
		case 6:
			return 20.0f;
		}
		return 20.0f;
	}
	
	public void FlashDarkness() {
		if(maxLight || darkLevel > FLASH_LEVEL || doFlash) return;
		doFlash = true;
		flashTimeStart = Time.time;
	}
	
	public void MaxLight() {
		if(maxLight) return;
		maxLight = true;
		minLight = false;
		maxTimeStart = Time.time;
	}
	
	public void MinLight() {
		if(minLight) return;	
		minLight = true;
		maxLight = false;
		minTimeStart = Time.time;
	}
	
	public void DisableDarkness() {
		disableDarkness = true;	
	}
	
	
	void Update() {
		if(Debug.isDebugBuild) {
			if(Input.GetKeyDown(KeyCode.Q)) {
				if(darkLevel > minDarkness) darkLevel--;
		
				if(darkLevel == minDarkness) {
				}
			}
			else if(Input.GetKeyDown(KeyCode.E)) {
				if(darkLevel < maxDarkness) darkLevel++;
			}
			if(Input.GetKeyDown(KeyCode.R)) {
				disableDarkness = !disableDarkness;	
			}
		}
		if(litMatchTimeStart + deathTimeSteps < Time.time) {
			if(darkLevel > minDarkness) {
				darkLevel--;
				litMatchTimeStart = Time.time;
				if(darkLevel == minDarkness) {
					minLight = false;
				}
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
		
		if(minLight) {
			if(minTimeStart + LIGHT_CONSUME < Time.time) {
				if(darkLevel > minDarkness)	 darkLevel--;
				minTimeStart = Time.time;
			}
			if(darkLevel == minDarkness) {
				minLight = false;
			}
		}
		sprite.spriteId = darkLevel;
		if(doFlash) {
			if(darkLevel < maxDarkness)
				sprite.spriteId = Random.Range(darkLevel, FLASH_LEVEL);
			if(flashTimeStart + FLASH_TIME < Time.time)
				doFlash = false;
		}
		sprite.renderer.enabled = !disableDarkness;
	}
	
	void SnuffPlayerMatch() {
		Player p = target.GetComponent<Player>();
		if(p) {
			p.SnuffMatch();	
		}
	}

	void FixedUpdate() {
		Vector3 start = transform.position;
		Vector3 end = target.position;
		end.z = start.z;
		transform.position = end;
	}
}
