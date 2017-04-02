using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRigMovement : MonoBehaviour {

	public float speed = 6f;

	private Rigidbody CameraRigRigidBody;
	private Vector3 movement;

	void Start () {
		CameraRigRigidBody = GetComponent<Rigidbody> ();
	}

	// Not sure if FixedUpdate or LateUpdate would be best here?
	void FixedUpdate () {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);
	}

	void Move(float h, float v)
	{
		float camAngle = Camera.main.transform.eulerAngles.y * 2f * Mathf.PI / 360f;

		// Factors in the rotation of the camera
		movement.Set (
			h * Mathf.Cos(-camAngle) + v * Mathf.Sin(camAngle), 
			0f, 
			h * Mathf.Sin(-camAngle) + v * Mathf.Cos(camAngle)
		);
			
		movement = movement.normalized * speed * Time.deltaTime;

		CameraRigRigidBody.MovePosition (transform.position + movement);
	}
}
