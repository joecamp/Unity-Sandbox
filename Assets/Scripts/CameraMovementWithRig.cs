using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementWithRig : MonoBehaviour {

	public float MovementSpeed;
	public float MovementSensitivity;
	public float ZoomSpeed;
	public float ZoomSensitivity;
	public float MaxZoom;
	public float MinZoom;

	private float _zoom;


	void Start()
	{
		_zoom = Camera.main.orthographicSize;
	}


	void LateUpdate () 
	{
		MoveCamera ();
		ZoomCamera ();
	}


	void MoveCamera() 
	{
		float movementX = 0;
		float movementZ = 0;

		if (Input.GetKey (KeyCode.A))
		{
			movementX -= MovementSpeed;
			movementZ -= MovementSpeed;
		}
		if (Input.GetKey (KeyCode.S))
		{
			movementX += MovementSpeed;
			movementZ -= MovementSpeed;
		}
		if (Input.GetKey (KeyCode.D))
		{
			movementX += MovementSpeed;
			movementZ += MovementSpeed;
		}
		if (Input.GetKey (KeyCode.W))
		{
			movementX -= MovementSpeed;
			movementZ += MovementSpeed;
		}

		movementX = Mathf.Clamp (movementX, -MovementSpeed, MovementSpeed);
		movementZ = Mathf.Clamp (movementZ, -MovementSpeed, MovementSpeed);

		movementX *= Time.deltaTime;
		movementZ *= Time.deltaTime;

		float newPosX = transform.position.x + movementX;
		float newPosZ = transform.position.z + movementZ;

		transform.position = new Vector3(newPosX, transform.position.y, newPosZ);
		transform.position = Vector3.Lerp (transform.position, new Vector3 (newPosX, transform.position.y, newPosZ), Time.deltaTime * MovementSensitivity);
	}


	void ZoomCamera() 
	{
		_zoom -= Input.GetAxis ("Mouse ScrollWheel") * ZoomSensitivity;
		_zoom = Mathf.Clamp (_zoom, MinZoom, MaxZoom);

		Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, _zoom, Time.deltaTime * ZoomSpeed);
	}
}