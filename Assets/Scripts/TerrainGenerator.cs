using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

	public int SizeX, SizeZ;
	public int MinHeight, MaxHeight;

	public GameObject GrassCubePrefab;

	public TCube[,] TCubes;

	private System.Random rng = new System.Random ();
	private Vector3 _generatorStartPoint;


	void Start () 
	{
		_generatorStartPoint = transform.position;
		TCubes = new TCube[SizeX, SizeZ];
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

				TCubes [x, z] = new TCube (x, currentHeight, z, 0);
				DrawTCube(TCubes[x, z]);
			}
		}
	}
		

	void DrawTCube(TCube cube) 
	{
		GameObject newCube;

		if (cube.Type == 0)
		{
			newCube = Instantiate (GrassCubePrefab) as GameObject;
			newCube.name = "GrassCube" + cube.X + ". " + cube.Y + "." + cube.Z;
			newCube.transform.SetParent (transform);
			Vector3 newPosition = new Vector3 (GrassCubePrefab.transform.localScale.x * cube.X, GrassCubePrefab.transform.localScale.y * cube.Y, GrassCubePrefab.transform.localScale.z * cube.Z);

			newCube.transform.localPosition = newPosition;
		}
	}
}
