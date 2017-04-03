using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	[Header("Camera Settings")]
	public float ZoomSpeed;
	public float ZoomSensitivity;
	public float MaxZoom;
	public float MinZoom;
	public float RotationDegrees;
	public float RotationTime;

	[Header("Input Settings")]
	public KeyCode RotateLeftKey;
	public KeyCode RotateRightKey;

	private Transform _cameraRig;
	private float _zoom;
	private Vector3 _offset;


	void Start()
	{
		_cameraRig = transform.parent;
		_zoom = Camera.main.orthographicSize;
		_offset = transform.position - _cameraRig.position;
	}


	void LateUpdate () 
	{
		RotateCamera ();
		ZoomCamera ();
	}


	void ZoomCamera() 
	{
		_zoom -= Input.GetAxis ("Mouse ScrollWheel") * ZoomSensitivity;
		_zoom = Mathf.Clamp (_zoom, MinZoom, MaxZoom);

		Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, _zoom, Time.deltaTime * ZoomSpeed);
	}


	void RotateCamera()
	{
		if (Input.GetKeyDown (RotateLeftKey)) 
		{
			StartCoroutine (Rotate (RotationDegrees));
		}
		if (Input.GetKeyDown (RotateRightKey)) 
		{
			StartCoroutine (Rotate (-RotationDegrees));
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
			transform.position = _cameraRig.position + _offset; 
			transform.LookAt(_cameraRig.position);
			_offset = transform.position - _cameraRig.position;

			// Increment the percentage rotated thus far
			percentRotated += degreesToRotate/rotationAngle;
			yield return null;
		}
	}
}