using UnityEngine;
using System.Collections;

public class GenerateMarket : MonoBehaviour {
	public GameObject n, b, x, i, g, a, r, c, v, h;

	// Use this for initialization
	void Start () {
		ActualMarketGeneration.Start (n, b, x, i, g, a, r, c, v, h);
	}
}
