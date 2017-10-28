using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour {


    public Vector3 velocity;
    public Vector3 direction;
    public Vector3 acceleration;
    public Vector3 vehiclePosition;

    public float mass;
    public float angleToRot;

	// Use this for initialization
	void Start () {
        vehiclePosition = transform.position;


	}
	
	// Update is called once per frame
	void Update () {

        
        // Add accel to velocity
        velocity += acceleration * Time.deltaTime;

        // Add velocity to pos
        vehiclePosition += velocity * Time.deltaTime;

        // Derive direction from velocity
        direction = velocity.normalized;

        // "Draw" object at its position
        transform.position = vehiclePosition;

        // Start fresh each frame
        acceleration = Vector3.zero;
    }

    
    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    public void RotatePlaneSides(float angle)
    {
        transform.Rotate(transform.forward, angle);
    }
    public void RotatePlaneForward(float angle)
    {

    }

}
