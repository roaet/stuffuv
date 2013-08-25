using UnityEngine;
using System.Collections;

public class MatchSounder : MonoBehaviour {

	public AudioSource[] lightsounds;
	public AudioSource[] hisssounds;
	public AudioSource[] strikesounds;
	
	public void PlayHissSound() {
		int idx = Random.Range(0, hisssounds.Length);
		hisssounds[idx].Play();
	}
	
	public void PlayLightSound() {
		int idx = Random.Range(0, lightsounds.Length);
		lightsounds[idx].Play();
	}
	
	public void PlayStrikeSound() {
		int idx = Random.Range(0, strikesounds.Length);
		strikesounds[idx].Play();
		
	}
}
