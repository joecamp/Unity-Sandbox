using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// initial environment generation happens here.  Also storing ECubes matrix (could this go somewhere else?  not sure.)  Need to keep
// matrix updated as changes to the environment occur.  should switch to 3D array before things get too messy

public class EnvironmentGenerator : MonoBehaviour {

	public GameObject[] CubePrefabs;

	[Header("Map Settings")]
	public int SizeX;
	public int SizeZ;
	public int MinHeight;
	public int MaxHeight;

	[Header("Generation Settings")]
	[Range(0.0f, 1.0f)]
	public float[] CubeProbabilities;
	[Range(0.0f, 1.0f)]
	public float GenTypeNearPercent;
	public float GenStepDelay;

	public ECube[,] ECubes;
	public List<GameObject> Cubes;

	private System.Random rng = new System.Random ();
	private Vector3 _generatorStartPoint;


	void Start () 
	{
		_generatorStartPoint = transform.position;
		ECubes = new ECube[SizeX, SizeZ];
		Cubes = new List<GameObject> ();

		GenerateFlatTerrain();
		SetIsolatedCubesToDefault ();
		DrawCubes ();
	}


	public void RegenMapAttempt1() 
	{
		CleanUp ();
		GenerateFlatTerrain ();
		SetIsolatedCubesToDefault ();
		DrawCubes ();
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

				ECubes [x, z] = new ECube (x, currentHeight, z, rng.Next(0, CubePrefabs.Length));
				DrawECube(ECubes[x, z]);
			}
		}
	}


	void GenerateFlatTerrain()
	{
		for (int x = (int)_generatorStartPoint.x; x < SizeX; x++)
		{
			for (int z = (int)_generatorStartPoint.z; z < SizeZ; z++)
			{
				ECubes [x, z] = new ECube (x, 0, z, GetTypeBasedOnProbability());
			}
		}
	}


	int GetTypeBasedOnProbability() 
	{
		float randomPercent = Random.Range(0.0f, 1.0f);
		float runningTotalPercent = 0f;

		for (int i = 0; i < CubeProbabilities.Length; i++)
		{
			if (randomPercent > (1 - (CubeProbabilities [i] + runningTotalPercent)))
			{
				return i;
			}

			runningTotalPercent += CubeProbabilities [i];
		}

		return 0;
	}
		

	void SetIsolatedCubesToDefault() 
	{
		for (int x = 0; x < SizeX; x++)
		{
			for (int z = 0; z < SizeZ; z++)
			{
				if (((x - 1) > 0) && ECubes [x - 1, z].Type == ECubes [x, z].Type)
				{
					continue;
				}
				if (((x + 1) < SizeX) && ECubes [x + 1, z].Type == ECubes [x, z].Type)
				{
					continue;
				}
				if (((z + 1) < SizeZ) && ECubes [x, z + 1].Type == ECubes [x, z].Type)
				{
					continue;
				}
				if (((z - 1) > 0) && ECubes [x, z - 1].Type == ECubes [x, z].Type)
				{
					continue;
				}

				ECubes [x, z].Type = 0;
			}
		}
	}


	void DrawCubes()
	{
		for (int x = 0; x < SizeX; x++)
		{
			for (int z = 0; z < SizeZ; z++)
			{
				DrawECube (ECubes [x, z]);
			}
		}
	}


	void DrawECube(ECube cube) 
	{
		GameObject newCube;

		newCube = Instantiate (CubePrefabs[cube.Type]) as GameObject;

		newCube.transform.SetParent (transform);
		Vector3 newPosition = new Vector3 (CubePrefabs[cube.Type].transform.localScale.x * cube.X, CubePrefabs[cube.Type].transform.localScale.y * cube.Y, CubePrefabs[cube.Type].transform.localScale.z * cube.Z);
		newCube.transform.localPosition = newPosition;

		Cubes.Add (newCube);
	}


	void CleanUp() 
	{
		for (int i = 0; i < Cubes.Count; i++)
		{
			GameObject.Destroy (Cubes [i]);
		}
	}
}