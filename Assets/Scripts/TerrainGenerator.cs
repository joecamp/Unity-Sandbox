using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

	public int SizeX, SizeZ;
	public int MinHeight, MaxHeight;

	public GameObject GrassCubePrefab;

	public List<GameObject> TerrainCubes;

	private System.Random rng = new System.Random ();
	private Vector3 _generatorStartPoint;


	void Start () 
	{
		_generatorStartPoint = transform.position;
		GenerateRandomHeightTerrain ();
	}


	void GenerateRandomHeightTerrain() 
	{
		int currentHeight;

		for (int x = (int)_generatorStartPoint.x; x < SizeX; x++)
		{
			for (int z = (int)_generatorStartPoint.z; z < SizeZ; z++)
			{
				// for now, just randomly choose a height.  eventually add heuristics to smooth heights.
				currentHeight = rng.Next(MinHeight, MaxHeight);
				AddCube (x, currentHeight, z);
			}
		}
	}


	void AddCube(int x, int y, int z) 
	{
		GameObject newCube = Instantiate (GrassCubePrefab) as GameObject;
		newCube.transform.SetParent (transform);
		newCube.name = "GrassCube" + (x + 1) + "." + (z + 1);

		Vector3 newPosition = new Vector3 (GrassCubePrefab.transform.localScale.x * x, GrassCubePrefab.transform.localScale.y * y, GrassCubePrefab.transform.localScale.z * z);

		newCube.transform.localPosition = newPosition;

		TerrainCubes.Add (newCube);
	}
}
