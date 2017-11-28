using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarController : MonoBehaviour {

    [Range(4, 9)]
    [SerializeField] private int sizeMin;

    [Range(1, 5)]
    [SerializeField] private int speedMax;

    [SerializeField] private int speed;
    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        transform.localScale = new Vector3(1, Random.Range(sizeMin, 10), 1);
        speed = Random.Range(1, speed);
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(0, 0, -speed);
    }
}
