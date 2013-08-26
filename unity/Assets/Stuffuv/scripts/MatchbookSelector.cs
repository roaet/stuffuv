using UnityEngine;
using System.Collections;

public class MatchbookSelector : MonoBehaviour {
	
	public GameObject whiteMatch;
	public GameObject greenMatch;
	public GameObject blueMatch;
	public GameObject redMatch;
	
	public tk2dTextMesh whiteCount;
	public tk2dTextMesh redCount;
	public tk2dTextMesh blueCount;
	public tk2dTextMesh greenCount;
	
	private int r, g, b, w;
	
	public Player player;
	
	private bool noMatches;
	private MatchColor selection;
	
	private tk2dSprite sprite;
	
	void Awake() {
		sprite = GetComponent<tk2dSprite>();
		noMatches = true;
		r = g = b = w = -1;
	}
	
	private void MoveSelectTo(GameObject match) {
		Vector3 start = transform.position;
		Vector3 end = match.transform.position;
		end.z = start.z;
		transform.position = end;
		selection = MatchColor.Invalid;
	}
	
	private void SelectByColor(MatchColor color) {
		GameObject target = null;
		switch(color) {
		case MatchColor.White:
			target = whiteMatch;
			break;
		case MatchColor.Red:
			target = redMatch;
			break;
		case MatchColor.Blue:
			target = blueMatch;
			break;
		case MatchColor.Green:
			target = greenMatch;
			break;
		}
		if(target) {
			MoveSelectTo(target);
			selection = color;
		}
	}
	
	public MatchColor GetSelection() {
		return selection;	
	}
	
	private bool CheckSelectionIsZero(MatchColor col) {
		switch(col) {
		case MatchColor.White:
			return player.WhiteMatches() == 0;
		case MatchColor.Red:
			return player.RedMatches() == 0;
		case MatchColor.Blue:
			return player.BlueMatches() == 0;
		case MatchColor.Green:
			return player.GreenMatches() == 0;
		}
		return false;
	}
	
	void Update() {
		sprite.renderer.enabled = player.HasAnyMatches();
		
		if(w != player.WhiteMatches()) {
			w = player.WhiteMatches();
			whiteCount.text = player.WhiteMatches().ToString();
			whiteCount.Commit();
		}
		if(r != player.RedMatches()) {
			r = player.RedMatches();
			redCount.text = player.RedMatches().ToString();
			redCount.Commit();
		}
		if(b != player.BlueMatches()) {
			b = player.BlueMatches();
			blueCount.text = player.BlueMatches().ToString();
			blueCount.Commit();
		}
		if(g != player.GreenMatches()) {
			g = player.GreenMatches();
			greenCount.text = player.GreenMatches().ToString();
			greenCount.Commit();
		}
		
		if(!player.HasAnyMatches()) {
			selection = MatchColor.Invalid;
			noMatches = true;
			return;
		}
		
		if(noMatches || CheckSelectionIsZero(selection)) {
			noMatches = false;
			if(player.WhiteMatches() > 0)
				SelectByColor(MatchColor.White);
			else if(player.RedMatches() > 0)
				SelectByColor(MatchColor.Red);
			else if(player.BlueMatches() > 0)
				SelectByColor(MatchColor.Blue);
			else if(player.GreenMatches() > 0)
				SelectByColor(MatchColor.Green);
		}
		
		if(Input.GetKeyUp (KeyCode.Alpha1) && player.WhiteMatches() > 0) {
			SelectByColor(MatchColor.White);
		}
		
		if(Input.GetKeyUp (KeyCode.Alpha2) && player.RedMatches() > 0) {
			SelectByColor(MatchColor.Red);
		}
		
		if(Input.GetKeyUp (KeyCode.Alpha3) && player.BlueMatches() > 0) {
			SelectByColor(MatchColor.Blue);
		}
		
		if(Input.GetKeyUp (KeyCode.Alpha4) && player.GreenMatches() > 0) {
			SelectByColor(MatchColor.Green);
		}		
	}
	
	
}
