using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerHint : MonoBehaviour {

    [SerializeField]
    private string hint;
    [SerializeField]
    private int lives = 1;

    private PlayerController player;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        GetComponent<Collider>().isTrigger = true;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.GiveHint(hint);
            lives--;

            if (lives <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
