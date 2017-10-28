using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer), typeof(Collider))]
public class Rock : MonoBehaviour {
    private int value = 0;
    private Rigidbody rb;
    public int Value
    {
        get { return value; }
    }
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ring"))
            value++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ring"))
            value--;
    }
}
