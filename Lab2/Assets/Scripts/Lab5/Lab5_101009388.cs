using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Lab5_101009388 : MonoBehaviour {

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private SliderBarMovement sliderBar;

    [SerializeField]
    private GameObject wall;

    private Rigidbody rb;
    private bool hit = false;
    private float hitPercentage;
    private Vector3 wallDisplacement;
    private Vector3 shotNeeded;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        wallDisplacement = findDisplacement(transform.position, wall.transform.position);

        float zPositionOffset = Random.Range(-30, 30);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + zPositionOffset);

        float hitViY = findViY(0, Physics.gravity.y, 6.5f);
        float peakT = findT(0, hitViY, Physics.gravity.y);
        float hitViZ = findViX((wallDisplacement.z / 2) + 5, peakT);

        shotNeeded = new Vector3(0, hitViY, hitViZ);
        //float postJumpDistance = findNextD(distance, speed, jumpT);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && sliderBar != null)
        {
            Debug.Log(sliderBar.hitPercentage);
            hitPercentage = sliderBar.hitPercentage;
            Destroy(sliderBar);
            canvas.enabled = false;
            hit = true;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
	}

    private void FixedUpdate()
    {
        if (hit)
        {
            hit = false;
            rb.AddForce(shotNeeded * (hitPercentage + 0.15f), ForceMode.VelocityChange);
        }
    }

    private Vector3 findDisplacement(Vector3 a, Vector3 b)
    {
        return new Vector3(b.x - a.x, b.y - a.x, b.z - a.z);
    }

    private float findViY(float vf, float a, float d)
    {
        return Mathf.Sqrt((vf * vf) - (2 * a * d));
    }

    private float findT(float vf, float vi, float a)
    {
        return (vf - vi) / a;
    }

    private float findNextD(float dy, float v, float t)
    {
        return dy + (v * t);
    }

    private float findViX(float dx, float t)
    {
        return dx / t;
    }
}
