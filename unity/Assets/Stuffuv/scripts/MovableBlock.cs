using UnityEngine;
using System.Collections;

public class MovableBlock : MonoBehaviour {

	// Use this for initialization
	private Vector3 originalPosition;
	void Awake() {
		originalPosition = transform.position;
	}
	
	public void ResetToStart() {
		transform.position = originalPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
