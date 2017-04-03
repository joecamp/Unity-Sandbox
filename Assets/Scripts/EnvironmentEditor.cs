using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

// UI and input trigger functions go here

public class EnvironmentEditor : MonoBehaviour {

	public EnvironmentUtilities EnvironmentUtilities;
	public ToggleGroup TerrainToggleGroup;

	private bool _elevateToggle = false;
	private bool _depressToggle = false;
	// make this an arraylist once you implement bigger selector sizes 
	private GameObject _hoveredOverObject;


	public void setElevateToggle (bool toggle) 
	{
		_elevateToggle = toggle;
	}


	public void setDepressToggle(bool toggle) 
	{
		_depressToggle = toggle;
	}


	void Update ()
	{
		// Mouse left click; not over UI
		if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject ())
		{
			HandleMouseInput ();
		} 
		else if (!EventSystem.current.IsPointerOverGameObject () && TerrainToggleGroup.AnyTogglesOn ())
		{
			HandleHoverOverObject ();
		} 
		else if (!TerrainToggleGroup.AnyTogglesOn () && _hoveredOverObject != null)
		{
			EnvironmentUtilities.UnHighlightCube (_hoveredOverObject);
			_hoveredOverObject = null;
		}

		if (Input.GetKeyDown (KeyCode.N))
		{
			EnvironmentUtilities.MoveAllCubesByDistance (1);
		}
		if (Input.GetKeyDown (KeyCode.M))
		{
			EnvironmentUtilities.MoveAllCubesByDistance (-1);
		}
	}


	void HandleMouseInput() 
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit))
		{
			if (hit.collider.gameObject.tag == "Environmental Cube")
			{
				GameObject hitCube = hit.collider.gameObject;
				//print(EnvironmentGenerator.ECubes[(int)hitCube.transform.position.x, (int)hitCube.transform.position.z]);
				//EnvironmentUtilities.HighlightCube (hitCube);

				if (_elevateToggle)
				{
					EnvironmentUtilities.MoveCubeY (hitCube, 1);
				} 
				else if (_depressToggle)
				{
					EnvironmentUtilities.MoveCubeY (hitCube, -1);
				}
			}
		}
	}


	void HandleHoverOverObject() 
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit))
		{
			if (hit.collider.gameObject.tag == "Environmental Cube")
			{
				GameObject hitCube = hit.collider.gameObject;

				if (hitCube == _hoveredOverObject)
				{
					return;
				}

				if (_hoveredOverObject != null)
				{
					EnvironmentUtilities.UnHighlightCube (_hoveredOverObject);
				}

				_hoveredOverObject = hitCube;

				EnvironmentUtilities.HighlightCube (hitCube);
			}
		}
	}
}