using UnityEngine;
using System.Collections;

public class PlayerSounder : MonoBehaviour {

	public AudioSource[] walksounds;
	
	public AudioSource currentSound;
	
	public void Awake() {
		currentSound = null;	
	}
	
	public void PlayWalkSound() {
		int idx = Random.Range(0, walksounds.Length);
		if(currentSound && currentSound.isPlaying) return;
		currentSound = walksounds[idx];
		walksounds[idx].Play();
	}
	
	public void StopWalkSound() {
		if(currentSound && currentSound.isPlaying)
			currentSound.Stop();
		currentSound = null;
			
	}
}
