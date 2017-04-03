using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// initial environment generation happens here.  Also storing ECubes matrix (could this go somewhere else?  not sure.)  Need to keep
// matrix updated as changes to the environment occur.  should switch to 3D array before things get too messy

public class EnvironmentGenerator : MonoBehaviour {

	[Header("Map Settings")]
	public int SizeX;
	public int SizeZ;
	public int MinHeight;
	public int MaxHeight;

	[Header("Cube Prefabs")]
	public GameObject GrassCubePrefab;

	public ECube[,] ECubes;
	public List<GameObject> Cubes;

	private System.Random rng = new System.Random ();
	private Vector3 _generatorStartPoint;


	void Start () 
	{
		_generatorStartPoint = transform.position;
		ECubes = new ECube[SizeX, SizeZ];
		Cubes = new List<GameObject> ();
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

				ECubes [x, z] = new ECube (x, currentHeight, z, 0);
				DrawECube(ECubes[x, z]);
			}
		}
	}


	void DrawECube(ECube cube) 
	{
		GameObject newCube;

		if (cube.Type == 0)
		{
			newCube = Instantiate (GrassCubePrefab) as GameObject;
			//newCube.name = "GrassCube" + cube.X + ". " + cube.Y + "." + cube.Z;
			newCube.transform.SetParent (transform);
			Vector3 newPosition = new Vector3 (GrassCubePrefab.transform.localScale.x * cube.X, GrassCubePrefab.transform.localScale.y * cube.Y, GrassCubePrefab.transform.localScale.z * cube.Z);

			newCube.transform.localPosition = newPosition;

			Cubes.Add (newCube);
		}
	}
}