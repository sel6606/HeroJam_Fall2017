using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementForces : MonoBehaviour
{
	public Vector3 position;//position of the object
	public Vector3 direction;//where we are facing
	public Vector3 velocity;//current velocity moving the object
	public Vector3 acceleration;//sum of forces acting on the object
	
	//public Vector3 wind = new Vector3(1.0f, 0.0f, 0.0f);//force of wind
	//public Vector3 gravity = new Vector3(0.0f, -1.0f, 0.0f);//force of gravity
	
	public float mass = 1.0f;// mass of the object
	public float maxSpeed = 5.0f;//maximum speed of vehicle

	private BehaviourManager behaviourMngr;//behaviour manager to calculate forces
	private Vector3 worldSize;//store the world size
	private GameObject target = null;//object to interact with

	public bool seeking = true;

	// Use this for initialization
	void Start ()
	{
		GameObject gameMngr = GameObject.Find("GameManager");
		if(null == gameMngr)
		{
			Debug.Log("Error in " + gameObject.name + 
			          ": Requires a GameManager object in the scene.");
			Debug.Break();
		}
		position = transform.position;
		behaviourMngr = gameMngr.GetComponent<BehaviourManager>();
		worldSize = behaviourMngr.worldSize;

		//Check that mas is initialized to something. Mass cannot be negative
		if (mass <= 0.0f)
		{
			mass = 0.01f;
		}
	}

	public void SetTarget(GameObject theTarget)
	{
		target = theTarget;
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdatePosition ();//Update the position based on forces
		BounceBoundry();//Bounce to stay within the terrain
		SetTransform();//Set the transform before render
	}

	// Update the position based on the velocity and acceleration
	void UpdatePosition()
	{
		//Step 0: update position to current tranform
		position = transform.position;

		//Step 0.5: seek the target
		if (seeking) {
			Vector3 seekingForce = Seek (target.transform.position);
			ApplyForce (seekingForce);
		} else 
		{
			Vector3 fleeingForce = Flee (target.transform.position);
			ApplyForce (fleeingForce);
		}

		//Step 1: Add Acceleration to Velocity * Time
		velocity += acceleration * Time.deltaTime;
		//Step 2: Add vel to position * Time
		position += velocity * Time.deltaTime;
		//Step 3: Reset Acceleration vector
		acceleration = Vector3.zero;
		//Step 4: Calculate direction (to know where we are facing)
		direction = velocity.normalized;
	}


	//Apply a force to the vehicle
	void ApplyForce(Vector3 force)
	{
		acceleration += force / mass;
	}

	Vector3 Seek(Vector3 targetPosition)
	{
		//Step 1: Calculate the desired unclamped velocity
		//which is from this vehicle to target's position
		Vector3 desiredVelocity = targetPosition - position;

		//Step 2: Calculate maximum speed
		//so the vehicle does not move faster than it should
		desiredVelocity.Normalize ();
		desiredVelocity *= maxSpeed;

		//Step 2 Alternative:
		//desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, maxSpeed);

		//Step 3: Calculate steering force
		Vector3 steeringForce = desiredVelocity - velocity;

		//Step 4: return the force so it can be applied to this vehicle
		return steeringForce;
	}

	Vector3 Flee(Vector3 targetPosition)
	{
		//Step 1: Calculate the desired unclamped velocity
		//which is from this vehicle to target's position
		Vector3 desiredVelocity = position - targetPosition;
		
		//Step 2: Calculate maximum speed
		//so the vehicle does not move faster than it should
		desiredVelocity.Normalize ();
		desiredVelocity *= maxSpeed;
		
		//Step 2 Alternative:
		//desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, maxSpeed);
		
		//Step 3: Calculate steering force
		Vector3 steeringForce = desiredVelocity - velocity;
		
		//Step 4: return the force so it can be applied to this vehicle
		return steeringForce;
	}
		
	//Apply friction to the vehicle based on the coefficient
	void ApplyFriction(float coeff)
	{
		// Step 1: Oposite velocity
		Vector3 friction = velocity * -1.0f;
		// Step 2: Normalize so is independent of velocity
		friction.Normalize ();
		// Step 3: Multiply by coefficient
		friction = friction * coeff;
		// Step 4: Add friction to acceleration
		acceleration += friction;
	}
	
	//Apply the trasformation
	void SetTransform()
	{
		transform.position = position;
		//orient the object
		transform.right = direction;
	}

	void OnRenderObject()
	{
        /*
		GL.PushMatrix ();
		// Finding target position
		behaviourMngr.matWhite.SetPass (0);
		GL.Begin (GL.LINES);
		GL.Vertex (position);
		GL.Vertex (target.transform.position);
		GL.End ();
		GL.PopMatrix ();
		// Finding velocity
		behaviourMngr.matRed.SetPass (0);
		GL.Begin (GL.LINES);
		GL.Vertex (position);
		GL.Vertex (position + velocity * 5.0f);
		GL.End ();
		GL.PopMatrix ();
        */
	}
	
	// Position the object inside of the screen
	void WrapBoundry()
	{	
		//Check within X
		if(position.x > worldSize.x)
			position.x = 0;
		else if(position.x < 0)
			position.x = worldSize.x;
		
		//check within Z
		if(position.z > worldSize.z)
			position.z = 0;
		else if(position.z < 0)
			position.z = worldSize.z;
	}

	// Position the object inside of the screen
	void KeepBoundry()
	{	
		//Check within X
		if(position.x > worldSize.x)
			position.x = worldSize.x;
		else if(position.x < 0)
			position.x = 0;
		
		//check within Z
		if(position.z > worldSize.z)
			position.z = worldSize.z;
		else if(position.z < 0)
			position.z = 0;
	}

	// Bounce the object inside of the screen
	void BounceBoundry()
	{	
		//Check within X
		if(position.x > worldSize.x)
		{
			position.x = worldSize.x;
			velocity.x *= -1;
		}
		else if(position.x < 0)
		{
			position.x = 0;
			velocity.x *= -1;
		}
		
		//check within Z
		if(position.z > worldSize.z)
		{
			position.z = worldSize.z;
			velocity.z *= -1;
		}
		else if(position.z < 0)
		{
			position.z = 0;
			velocity.z *= -1;
		}
	}
}
