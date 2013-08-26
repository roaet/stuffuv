using UnityEngine;
using System.Collections;

public class StuffuvController : MonoBehaviour {
	private ArrayList stuffuvs;
	public GameObject stuffuvContainer;
	public ShadowFollow darkness;
	public Transform target;
	public int stuffuvCount;
	public float stuffuvDistance;
	public Stuffuv stuffuvPrefab;
	public bool madeStuffuvs;
	
	// Use this for initialization
	void Start () {
		stuffuvs = new ArrayList();
		madeStuffuvs = false;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	Vector3 RandomCircle(Vector3 center, float radius) {
		int ang = Random.Range(0, 360);
		Vector3 pos = new Vector3(0, 0, 0);
		pos.x = center.x + radius * Mathf.Sin (ang * Mathf.Deg2Rad);
		pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		pos.z = center.z;
		return pos;
	}
	
	public void SpawnStuffuvsAroundTarget() {
		for(int i = 0; i < stuffuvCount; i++) {
			Stuffuv s = (Stuffuv) Instantiate(stuffuvPrefab);
			s.target = target;
			s.darkness = darkness;
			s.transform.parent = stuffuvContainer.transform;
			s.transform.position = RandomCircle(target.position, stuffuvDistance);
			stuffuvs.Add(s);
		}
	}
	
	public void DestroyStuffuvs() {
		foreach(Stuffuv s in stuffuvs) {
			Destroy (s.gameObject);	
		}
		stuffuvs.Clear();
	}
}
