using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaultCheck : MonoBehaviour {

    [SerializeField]
    private LevelController lc;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        lc.Fault(other.gameObject);
    }
}
