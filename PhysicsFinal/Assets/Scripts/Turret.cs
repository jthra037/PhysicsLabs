using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private bool isOn = false;
    private bool isFiring = false;

    public bool On
    {
        get { return isOn; }
        set
        {
            if (isOn != value)
            {
                isOn = value;
                if (isOn)
                {
                    FiringRoutine = StartCoroutine(RunFiringSequence());
                }
            }
        }
    }
        
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float fireRate = 1.2f;
    [SerializeField]
    private GameObject ammo;

    private Rigidbody targetRB;

    private Coroutine FiringRoutine;


	// Use this for initialization
	void Start () {
		if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }

        if (target == null || ammo == null)
        {
            Debug.Log("Missing something!");
            Debug.Break();
        }

        targetRB = target.GetComponent<Rigidbody>();
	}

    private void FireAtTarget()
    {
        GameObject myProjectile = Instantiate(ammo, transform.position, transform.rotation) as GameObject;
        myProjectile.GetComponent<Rigidbody>().velocity = 
            FIN.FindTrajectoryVelocity(transform.position,
            target.transform.position,
            targetRB == null ? Vector3.zero : Random.Range(0.5f, 1.5f) * targetRB.velocity, // have some randomization on accuracy
            1.7f); // pretty aggressive curve
    }

    private IEnumerator RunFiringSequence()
    {
        yield return new WaitForSeconds(Random.Range(0.2f, 0.9f)); // Don't fire right away
        while(isOn)
        {
            FireAtTarget();
            yield return new WaitForSeconds(Random.Range(fireRate, 2*fireRate));
        }
    }
}
