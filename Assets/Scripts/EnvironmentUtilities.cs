using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// functions related to manipulating environmental cubes goes here

public class EnvironmentUtilities : MonoBehaviour {

	[Header("Utilities Settings")]
	public float CubeMoveSpeed;
	public Color HighlightColor;

	[Header("References")]
	public EnvironmentGenerator EnvironmentGenerator;

	private float _minHeight, _maxHeight;


	void Start()
	{
		_minHeight = EnvironmentGenerator.MinHeight;
		_maxHeight = EnvironmentGenerator.MaxHeight;
	}


	public void HighlightCube(GameObject cube)
	{
		if (cube.tag == "Environmental Cube")
		{
			Renderer cubeRenderer = cube.GetComponent<Renderer>();
			Material cubeMat = cubeRenderer.material;

			cubeMat.SetColor ("_EmissionColor", HighlightColor);
		}
	}


	public void UnHighlightCube(GameObject cube)
	{
		if (cube.tag == "Environmental Cube")
		{
			Renderer cubeRenderer = cube.GetComponent<Renderer>();
			Material cubeMat = cubeRenderer.material;

			cubeMat.SetColor ("_EmissionColor", Color.black);
		}
	}


	public void MoveCubeY(GameObject cube, float distance) 
	{
		if (!((cube.transform.position.y + distance < _minHeight) || (cube.transform.position.y + distance > _maxHeight)))
		{
			Vector3 newPosition = new Vector3 (
				cube.transform.position.x,
				cube.transform.position.y + distance,
				cube.transform.position.z
			);

			StartCoroutine(CubeYOverTime(cube, newPosition));
		}
	}


	IEnumerator CubeYOverTime(GameObject cube, Vector3 target) 
	{
		float step;

		while (cube.transform.position != target)
		{
			step = CubeMoveSpeed * Time.deltaTime;
			cube.transform.position = Vector3.MoveTowards (cube.transform.position, target, step);
			yield return null;
		}
	}


	public void MoveAllCubesByDistance(float distance) 
	{
		for (int i = 0; i < EnvironmentGenerator.Cubes.Count; i++)
		{
			MoveCubeY (EnvironmentGenerator.Cubes [i], distance);
		}
	}
}