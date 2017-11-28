using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Lab3_101009388 : MonoBehaviour {

    [SerializeField]
    private GameObject refObject;
    //[SerializeField]
    //private float pushTime = Mathf.NegativeInfinity;

    private Rigidbody rb;

    private float D;
    private float Ti;
    private float Ff;
    private float Af;
    private float Vi;
    private float Fp;

    private bool isRunning = false;
    private float elapsedTime = 0;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        D = getDisplacement();
        Ff = getFrictionForce();

        //if (pushTime == Mathf.NegativeInfinity)
        //{        
            Ti = solveForTime();
            Fp = setForcePush();

            Debug.Log("D: " + D);
            Debug.Log("Ti: " + Ti);
            Debug.Log("Ff: " + Ff);
            Debug.Log("Fp: " + Fp);
        //}
        //else // This is a more complicated version of this problem, it works on paper but Unity doesn't agree
        //{
        //    Ti = pushTime;
        //    Af = -Ff / rb.mass;
        //    Vi = solveForVelocity();
        //    Fp = ((Vi * rb.mass) / Ti);
        //
        //    Debug.Log("D: " + D);
        //    Debug.Log("Ti: " + Ti);
        //    Debug.Log("Ff: " + Ff);
        //    Debug.Log("Af: " + Af);
        //    Debug.Log("Vi: " + Vi);
        //    Debug.Log("Fp: " + Fp);
        //}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            isRunning = true;
            Debug.Log("isRunning: " + isRunning);
        }
	}

    private void FixedUpdate()
    {
        // Apply force after space is hit
        if (isRunning)
        {
            rb.AddForce(transform.forward * Fp, ForceMode.Force);
            elapsedTime += Time.fixedDeltaTime;

            // Stop applying force when push time is reached
            if (elapsedTime > Ti)
            {
                Debug.Log("elapsedTime: " + elapsedTime);
                isRunning = false;
                Debug.Log("isRunning: " + isRunning);
            }
        }
    }

    /// <summary>
    /// Finds distance from self to ref object
    /// </summary>
    /// <returns>Distance to ref object</returns>
    private float getDisplacement()
    {
        return (refObject.transform.position - transform.position).z;
    }

    /// <summary>
    /// Ff: Force of Friction
    /// Ff = MU * Ag * m
    /// Or in Unity's case:
    /// Ff = mysteryCoefficient * MU * Ag * m
    /// </summary>
    /// <returns>Force of friction</returns>

    private float getFrictionForce()
    {
        return 3 * GetComponent<BoxCollider>().material.dynamicFriction * rb.mass * -Physics.gravity.magnitude;
    }


    /// <summary>
    /// Solve for the time to push the object halfway to the goal 
    /// with Fnet = -Ff
    /// </summary>
    /// <returns></returns>
    private float solveForTime()
    {
        return Mathf.Sqrt(D / ((-2 * Ff) / rb.mass));
    }

    //private float solveForVelocity()
    //{
    //    return 0.5f * ((Mathf.Sqrt(Af) * Mathf.Sqrt((Af * Ti * Ti) + (8 * D))) - (Af * Ti));
    //}

    /// <summary>
    /// If exactly half the distance to be travelled, is covered while accelerating:
    /// Fp = -2 * Ff
    /// </summary>
    /// <returns>Force of Push</returns>
    private float setForcePush()
    {
        return -2 * Ff;
    }
}
