using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    // Use this for initialization


    public float m_angleIncrement = 2.0f;
    public float m_time = 1.0f;
    public GameObject m_targetObject = null;

    public GameObject m_bulletRef = null;
	void Start () {
		
	}

    //this returns net velocity, not Xvel or Yvel, but rather the net
    float CalculateInitialVelocity(float angle)
    {
        float displacement = (transform.position - m_targetObject.transform.position).magnitude;
        Debug.Log("Displacement: " + displacement);
        float xVelocity = displacement / m_time;
        Debug.Log("Angle: " + angle);
        
        float yVelocity = Mathf.Tan(Mathf.Deg2Rad * angle) * xVelocity;
        return Mathf.Sqrt(xVelocity*xVelocity + yVelocity*yVelocity);
    }

	// Update is called once per frame
	void Update () {
        float rotation = Input.GetKey(KeyCode.W) ? m_angleIncrement * -Time.deltaTime : 0.0f;
        rotation += Input.GetKey(KeyCode.S) ? m_angleIncrement * Time.deltaTime : 0.0f;
        transform.Rotate(rotation, 0.0f, 0.0f);
        if(Mathf.Abs(rotation) > Mathf.Epsilon)
        {
            Debug.Log("Rotation: " + transform.rotation.eulerAngles);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            //launch projectile
            Rigidbody rb = m_bulletRef.GetComponent<Rigidbody>();
            rb.useGravity = true;
            Vector3 forward = new Vector3(0.0f, 0.0f, 1.0f);
            float angle = transform.rotation.eulerAngles.x -90.0f;
            float initialVelocity = CalculateInitialVelocity(angle);
            Vector3 direction = Quaternion.Euler(angle, 0.0f, 0.0f) * forward;
            Vector3 impulse = direction * (initialVelocity * rb.mass / Time.fixedDeltaTime);
            rb.AddForce(impulse, ForceMode.Force);
            //rb.velocity = velocity;
            Debug.Log("Cannon Impulse: " + impulse);
            m_bulletRef.layer = LayerMask.GetMask("Default");
            Debug.Log("Cannon Rotation: " + this.transform.rotation.eulerAngles);
        }
    }
}
