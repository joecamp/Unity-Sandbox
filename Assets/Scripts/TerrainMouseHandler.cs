using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class TerrainMouseHandler : MonoBehaviour {

	public TerrainGenerator TerrainGenerator;

	void Start () 
	{
		
	}
	

	void Update () 
	{
		if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject ())
		{
			HandleMouseInput ();
		}
	}


	void HandleMouseInput() 
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit))
		{
			print ("test");
		}
	}
}
