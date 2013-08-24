using UnityEngine;
using System.Collections;

public class MatchSounder : MonoBehaviour {

	public AudioSource[] lightsounds;
	public AudioSource[] strikesounds;
	
	public void PlayLightSound() {
		int idx = Random.Range(0, lightsounds.Length);
		lightsounds[idx].Play();
	}
	
	public void PlayStrikeSound() {
		int idx = Random.Range(0, strikesounds.Length);
		strikesounds[idx].Play();
		
	}
}
