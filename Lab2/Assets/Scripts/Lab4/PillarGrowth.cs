using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarGrowth : MonoBehaviour {

    [Range(4, 9)]
    public int sizeMin;

	// Use this for initialization
	void Start () {
        transform.localScale = new Vector3(1, Random.Range(sizeMin, 10), 1);
    }
}
