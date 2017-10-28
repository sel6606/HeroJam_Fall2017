using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{


    public Vector3 velocity;
    public Vector3 direction;
    public Vector3 turning;
    public Vector3 acceleration;
    public Vector3 vehiclePosition;

    public Vector3 gravity; 
    public float mass;
    public float angleToRot;
    public float speed;

	// Use this for initialization
	void Start () {
        vehiclePosition = transform.position;
        

    }
	
	// Update is called once per frame
	void Update () {

        //direction = new Vector3(Input.GetAxis("Vertical"),0,0);
        direction = gameObject.transform.forward;
        direction *= speed;
        //ApplyForce(direction);

       
        RotatePlaneForward(angleToRot);
        
        
        
        
        
        
        
        
        
        // Add accel to velocity
        velocity += direction * Time.deltaTime;

        //velocity = velocity *  direction * Time.deltaTime;

        // Add velocity to pos
        vehiclePosition += velocity * Time.deltaTime;

        // Derive direction from velocity
        //direction = velocity.normalized;

        // "Draw" object at its position
        transform.position = vehiclePosition;

        // Start fresh each frame
        acceleration = Vector3.zero;
    }

    
    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    public void RotatePlaneForward(float angle)
    {
        transform.Rotate(transform.right, angle);
    }
    public void RotatePlaneSide(float angle)
    {
        transform.Rotate(transform.forward, angle);
    }

}
