using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testbullet : MonoBehaviour {
    Rigidbody ri;
	// Use this for initialization
	void Start () {
        ri = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        ri.velocity = transform.forward * 10;
	}
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
