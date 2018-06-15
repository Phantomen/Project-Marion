using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement1 : MonoBehaviour {

	[SerializeField]
	private float walkingSpeedMax	= 20;
	[SerializeField]
	private float walkingSpeedTime	= 1;	//Time from 0 to max
	private float walkingAccel;


	[SerializeField]
	private float runningSpeed	= 7.5f;

	[SerializeField]
	private float jumpHeight	= 2;


	private Vector3 movement;

	private Rigidbody playerRigidBody;

	// Use this for initialization
	void Start ()
	{
		walkingAccel = walkingSpeedMax / walkingSpeedTime;
		playerRigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);
	}

	private void Move(float h, float v)
	{
		Vector3 newVel = new Vector3 (playerRigidBody.velocity.x, 0f, playerRigidBody.velocity.z);
		
		if (h != 0)
		{
			newVel.x += h * walkingAccel * Time.deltaTime;
		}

		if (v != 0)
		{
			newVel.z += v * walkingAccel * Time.deltaTime;
		}


		//If faster than walking speed, slow down
		if (newVel.magnitude > walkingSpeedMax)
		{
			Vector3 newVelDecel = (newVel.normalized * 2 * walkingAccel * Time.deltaTime);
			if (newVel.x != 0)
			{
				newVel.x -= newVelDecel.x;

				if (newVel.x > 0 && newVelDecel.x < 0
					||
					newVel.x < 0 && newVelDecel.x > 0)
				{
					newVel.x = 0;
				}
			}


			if (newVel.z != 0)
			{
				newVel.z -= newVelDecel.z;

				if (newVel.z > 0 && newVelDecel.z < 0
					||
					newVel.z < 0 && newVelDecel.z > 0)
				{
					newVel.z = 0;
				}
			}


			//If speed is slower than walking speed but you want to move in the "same" direction
			//Set magnitude of velocity to max walking speed
			if (newVel.magnitude < walkingSpeedMax)
			{
				if (newVel.x * h >= 0
				    && newVel.z * v >= 0)
				{
					newVel = newVel * (walkingSpeedMax / newVel.magnitude);
				}
			}
		}

		newVel.y = playerRigidBody.velocity.y;

		playerRigidBody.velocity = newVel;
	}

}


//Work with later
public class PlayerWalk
{
	[SerializeField] private float movementSpeedMax;
	[SerializeField] private float movementSpeedTime;	//Time from 0 to max
	private float movementAccel;

	public float MovementSpeedMax
	{
		get { return movementSpeedMax; }
	}

	public float MovementSpeedTime
	{
		get { return movementSpeedTime; }
	}

	public float MovementAccel
	{
		get { return movementSpeedMax / movementSpeedTime; }
	}
}
