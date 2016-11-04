using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

	public int lifespan = 5;

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject , lifespan);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
