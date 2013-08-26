using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public MatchBook[] matches;
	public MovableBlock[] blocks;
	public bool makeStuffuvs = true;
	public bool makeDaylight = false;
	
	void Awake() {
	
		var renderers = gameObject.GetComponentsInChildren<Renderer>();
		foreach(Renderer r in renderers) {
			r.enabled = false;
		}		
	}
	
	public void Respawn(Player player) {
		PerformTeleport(player);	
	}
	public void ResetMatches() {
		foreach(MatchBook mb in matches) {
			mb.Reset();	
		}
	}
	public void ResetBlocks() {
		foreach(MovableBlock b in blocks) {
			b.ResetToStart();	
		}
	}
	
	private void PerformTeleport(Player player) {
		player.EmptyMatches();
		Vector3 start = player.transform.position;
		Vector3 end = transform.position;
		end.z = start.z;
		player.transform.position = end;
		player.stuffuvSpawner.DestroyStuffuvs();
		if(makeStuffuvs && !makeDaylight) 
			player.stuffuvSpawner.SpawnStuffuvsAroundTarget();
		ResetMatches();
		ResetBlocks();
		player.HandleMatchFromRespawn();
		if(makeDaylight) {
			player.GoDaytime();
		}
	}
}
