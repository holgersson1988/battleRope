using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePattern : MonoBehaviour {

    public Vector3 eulerAngleVelocity;
    public float rotationModifier;

    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        Debug.Assert(rb, "No rigidbody on rotating obstacle");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * rotationModifier * Utility.DeltaTime());
        rb.MoveRotation(rb.rotation * deltaRotation);


	}
}
