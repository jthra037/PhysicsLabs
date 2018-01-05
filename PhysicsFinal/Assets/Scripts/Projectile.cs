using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {
    [SerializeField]
    private float lifeTime = 10f;

    private void Start()
    {
        StartCoroutine(GetRidOfMe());
    }

    private IEnumerator GetRidOfMe()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
