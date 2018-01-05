using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TurretObserver : MonoBehaviour {

    private Turret[] turrets;
    private Collider myCollider;

	// Use this for initialization
	void Start () {
        turrets = GetComponentsInChildren<Turret>();
        myCollider = GetComponent<Collider>();
        myCollider.isTrigger = true;
	}

    public void TurnTurretsOff()
    {
        foreach (Turret turret in turrets)
        {
            turret.On = false;
        }
    }

    public void TurnTurretsOn()
    {
        foreach (Turret turret in turrets)
        {
            turret.On = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TurnTurretsOn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TurnTurretsOff();
        }
    }
}
