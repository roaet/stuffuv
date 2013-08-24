using UnityEngine;
using System.Collections;

public class Striker : MonoBehaviour {

	public tk2dSprite strikeZone;
	public tk2dSprite strikeMeter;
	public tk2dSprite strikeBubble;
	public MatchSounder matchSounds;
	public ShadowFollow darkness;
	
	public Transform target;
	
	private float meterWidth;
	private float bubbleWidth;
	private float bubbleLocation;
	private float bubbleDirection;
	private float bubbleSpeed;
	private bool strikerEnabled;
	
	private const int RIGHT = 1;
	private const int LEFT = -1;
	private const float MIN_SPEED = 0.1f;
	private const float SPEED_STEP = 0.05f;
	private const float MAX_SPEED = 0.2f;
	
	
	void Awake() {
		meterWidth = strikeMeter.CurrentSprite.GetBounds().size.x;
		bubbleWidth = strikeBubble.CurrentSprite.GetBounds().size.x;
		Reset(true);
		strikerEnabled = false;
	}
	
	void Update() {
		if(!strikerEnabled) {
			var renderers = gameObject.GetComponentsInChildren<Renderer>();
			foreach(Renderer r in renderers) {
				r.enabled = false;
			}
			if(Input.GetKeyDown(KeyCode.Space)) {
				strikerEnabled = true;
				matchSounds.PlayStrikeSound();
				darkness.FlashDarkness();
			}
			return;
		}
		else {
			var renderers = gameObject.GetComponentsInChildren<Renderer>();
			foreach(Renderer r in renderers) {
				r.enabled = true;
			}
			if(Input.GetKeyDown(KeyCode.Space)) {
				darkness.FlashDarkness();
				if(!CheckStrike()) {
					Fail ();	
				} else Success();
			}
		}
	}
	
	void Reset(bool firstRun) {
		if(firstRun) {
			bubbleLocation = 0.0f;
		} else {
			SetBubbleToStart();
		}
		bubbleDirection = RIGHT;
		bubbleSpeed = MIN_SPEED;
	}
	
	void SetBubbleToStart() {
		Vector3 meterPosition = strikeMeter.transform.position;
		float meterOriginOffset = meterWidth / 2;
		float meterStart = meterPosition.x - meterOriginOffset;
		
		Vector3 bubble = strikeBubble.transform.position;
		bubble.x = meterStart;
		strikeBubble.transform.position = bubble;
		
	}
	
	void Fail() {
		SetBubbleToStart();
		matchSounds.PlayStrikeSound();
		bubbleDirection = RIGHT;
		if(bubbleSpeed < MAX_SPEED) 
			bubbleSpeed += SPEED_STEP;
	}
	
	void DisableStriker() {
		var renderers = gameObject.GetComponentsInChildren<Renderer>();
		foreach(Renderer r in renderers) {
			r.enabled = false;
		}
	}
	
	void Success() {
		strikerEnabled = false;
		DisableStriker();
		Reset(false);
		matchSounds.PlayLightSound();
		darkness.MaxLight();
	}
	
	bool CheckStrike() {
		float bubbleSz = strikeBubble.renderer.bounds.size.x / 2;
		float zoneSz = strikeZone.renderer.bounds.size.x / 2;
		Vector3 bubble = strikeBubble.transform.position;
		Vector3 zone = strikeZone.transform.position;
		
		float nzs = zone.x - zoneSz;
		float pzs = zone.x + zoneSz;
		
		float nss = bubble.x - bubbleSz;
		float pss = bubble.x + bubbleSz;
		
		if(pss < nzs || nss > pzs) {
			return false;	
		}
		return true;
	}

	void FixedUpdate() {
		if(!strikerEnabled) return;
		Vector3 start = transform.position;
		Vector3 end = target.position;
		end.z = start.z;
		transform.position = end;
		
		Vector3 meterPosition = strikeMeter.transform.position;
		Vector3 bubble = strikeBubble.transform.position;
		float meterOriginOffset = meterWidth / 2;
		float meterStart = meterPosition.x - meterOriginOffset;
		
		if(bubble.x <= meterStart + bubbleWidth && bubbleDirection == LEFT) {
			bubbleDirection = RIGHT;
			if(bubbleSpeed < MAX_SPEED) 
				bubbleSpeed += SPEED_STEP;
		}
		if(bubble.x >= meterStart + meterWidth - bubbleWidth && bubbleDirection == RIGHT) {
			bubbleDirection = LEFT;	
		}
		
		bubble.x = bubble.x + (bubbleSpeed * bubbleDirection);
		strikeBubble.transform.position = bubble;
	}
}
