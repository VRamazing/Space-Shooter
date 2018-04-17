using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerControl : MonoBehaviour {

	public float speed;
	public Boundary boundary;
	public float tilt;
	public float fireRate;
	private float nextFire;

	public Transform shotSpawnParent; 
	public GameObject shot;
	public Transform shotSpawn;
	private AudioSource audio;
	private Quaternion calibrationQuaternion;
	public SimpleTouchPad touchPad;

	void Start(){
		audio = GetComponent<AudioSource>();	
		CaliberateAccelerometer ();
	}

	void FixedUpdate()
	{
//		float moveHorizontal = Input.GetAxis ("Horizontal");
//		float moveVertical = Input.GetAxis ("Vertical");
//		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);


//		Vector3 accelerationRaw = Input.acceleration;
//		Vector3 acceleration = FixAcceleration (accelerationRaw);
//		Vector3 movement = new Vector3 (acceleration.x, 0.0f, acceleration.y);
		Vector2 direction = touchPad.GetDirection();
		Vector3 movement = new Vector3 (direction.x, 0.0f, direction.y);
		Rigidbody playerRigidBody = GetComponent<Rigidbody> ();
		playerRigidBody.velocity = movement * speed;
		playerRigidBody.position = new Vector3 (
			Mathf.Clamp (playerRigidBody.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (playerRigidBody.position.z, boundary.zMin, boundary.zMax)
		);
		playerRigidBody.rotation = Quaternion.Euler (
			0.0f, 
			0.0f, 
			playerRigidBody.velocity.x * -tilt);
	}

	void Update(){
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			GameObject clone = Instantiate (shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
			clone.transform.SetParent (shotSpawnParent);
			audio.Play ();
		}
		
	}

	void CaliberateAccelerometer(){
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
	}

	Vector3 FixAcceleration  (Vector3 acceleration){
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
		


	}
}
