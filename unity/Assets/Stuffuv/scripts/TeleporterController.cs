using UnityEngine;
using System.Collections;

public class TeleporterController : MonoBehaviour {
	public GameObject endMark = null;
	public GameObject redMark = null;
	public GameObject blueMark = null;
	public GameObject greenMark = null;
	public bool makeStuffuvs = true;
	public GameObject areaspawner = null;
	public GameObject teleportToSpawner = null;
	
	public void DoTeleport(Player player, MatchColor color) {
		if(teleportToSpawner) {
			Spawner s = teleportToSpawner.GetComponent<Spawner>();
			if(s) {
				player.SetSpawner(teleportToSpawner);
				s.Respawn(player);	
			}
		} else PerformTeleport(player, color);	
	}
	
	private void PerformTeleport(Player player, MatchColor color) {
		GameObject target = null;
		switch(color) {
		case MatchColor.White:
			target = endMark;
			break;
		case MatchColor.Red:
			target = redMark;
			break;
		case MatchColor.Blue:
			target = blueMark;
			break;
		case MatchColor.Green:
			target = greenMark;
			break;
		}
		if(target) {
			Vector3 start = player.transform.position;
			Vector3 end = target.transform.position;
			end.z = start.z;
			player.transform.position = end;
			player.stuffuvSpawner.DestroyStuffuvs();
			if(areaspawner)
				player.SetSpawner(areaspawner);
			if(makeStuffuvs) 
				player.stuffuvSpawner.SpawnStuffuvsAroundTarget();
		}
	}
}
