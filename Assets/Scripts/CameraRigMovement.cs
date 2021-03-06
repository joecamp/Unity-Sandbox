﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRigMovement : MonoBehaviour {

	[Header("Rig Settings")]
	public float RigSpeed = 6f;

	[Header("References")]
	public EnvironmentGenerator EnvironmentGenerator;

	private Vector3 _movement;


	void Start() 
	{
		Vector3 startPosition = new Vector3(
			EnvironmentGenerator.SizeX / 2f,
			0,
			EnvironmentGenerator.SizeZ / 2f
		);

		transform.position = startPosition;
	}


	void LateUpdate () {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		MoveCameraRig (h, v);
	}


	void MoveCameraRig(float h, float v)
	{
		float camAngle = Camera.main.transform.eulerAngles.y * 2f * Mathf.PI / 360f;

		// Factors in the rotation of the camera
		_movement.Set (
			h * Mathf.Cos (-camAngle) + v * Mathf.Sin (camAngle),
			0f,
			h * Mathf.Sin (-camAngle) + v * Mathf.Cos (camAngle)
		);
			
		_movement = _movement.normalized * RigSpeed * Time.deltaTime;

		Vector3 newPosition = transform.position + _movement;

		transform.position = newPosition;
	}
}