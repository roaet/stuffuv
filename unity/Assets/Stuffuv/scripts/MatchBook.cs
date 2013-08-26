using UnityEngine;
using System.Collections;

public enum MatchColor { Red, Blue, Green, White, Invalid };

public class MatchBook : MonoBehaviour {
	public MatchColor color;
	public int quantity;
	
	private bool pickedUp;
	
	void Awake() {
		pickedUp = false;	
	}
	
	public bool PickUp() {
		if(pickedUp) return false;
		pickedUp = true;
		return true;
	}
	
	public void Reset() {
		pickedUp = false;	
	}
	
	public MatchColor GetColor() {
		return color;
	}	
	
	
	void Update() {
		var renderers = gameObject.GetComponentsInChildren<Renderer>();
		foreach(Renderer r in renderers) {
			r.enabled = !pickedUp;
		}	
	}
}
