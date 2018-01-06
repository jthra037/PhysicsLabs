using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GameEnder : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GetComponent<Collider>().isTrigger = true;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().WinnerGagnon();
            Destroy(gameObject);
        }
    }
}
