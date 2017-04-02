using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public float ZoomSpeed;
	public float ZoomSensitivity;
	public float MaxZoom;
	public float MinZoom;
	public Transform CameraRig;
	public float RotationTime;


	private float _zoom;
	private Vector3 _offset;


	void Start()
	{
		_zoom = Camera.main.orthographicSize;
		_offset = transform.position - CameraRig.position;
	}

	void LateUpdate () 
	{
		MoveCamera ();
		RotateCamera ();
		ZoomCamera ();
	}


	void MoveCamera()
	{
		Vector3 targetCamPos = CameraRig.position + _offset;
		transform.position = targetCamPos;
	}


	void RotateCamera() 
	{
		if (Input.GetKey (KeyCode.Q))
		{
			Vector3 additionalRotation = new Vector3 (45f, 0f, 0f); 

			transform.Rotate (additionalRotation);
		}
	}


	void ZoomCamera() 
	{
		_zoom -= Input.GetAxis ("Mouse ScrollWheel") * ZoomSensitivity;
		_zoom = Mathf.Clamp (_zoom, MinZoom, MaxZoom);

		Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, _zoom, Time.deltaTime * ZoomSpeed);
	}

	void RotateCamera()
	{
		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			float rotationAngle = 45f;
			StartCoroutine (Rotate (rotationAngle));
		}
		if (Input.GetKeyDown (KeyCode.E)) 
		{
			float rotationAngle = -45f;
			StartCoroutine (Rotate (rotationAngle));
		}
	}

	IEnumerator Rotate(float rotationAngle)
	{
		float percentRotated = 0f;
		float percentToRotate = Time.deltaTime / RotationTime;

		while (percentRotated <= 1f) 
		{
			// Set amount to rotate this frame
			float degreesToRotate = rotationAngle * percentToRotate;

			// Check if rotation will overshoot the rotationAngle and adjust accordingly
			if (percentRotated + degreesToRotate/rotationAngle > 1f) 
			{
				percentToRotate = 1f - percentRotated;
				degreesToRotate = percentToRotate * rotationAngle;
			}

			// Perform rotation
			_offset = Quaternion.AngleAxis (degreesToRotate, Vector3.up) * _offset;
			transform.position = CameraRig.position + _offset; 
			transform.LookAt(CameraRig.position);
			_offset = transform.position - CameraRig.position;

			// Increment the percentage rotated thus far
			percentRotated += degreesToRotate/rotationAngle;
			yield return null;
		}
	}
}