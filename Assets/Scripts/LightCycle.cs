using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCycle : MonoBehaviour {

	[Header("Sun Settings")]
	public float SecondsInDay;
	[Range(0,1)]
	public float TimeOfDay;

	[Header("References")]
	public Light Sun;

	private float _initialIntensity;


	void Start () 
	{
		_initialIntensity = Sun.intensity;
	}
	

	void Update () 
	{
		UpdateSun ();
		TimeOfDay += (Time.deltaTime / SecondsInDay);
		if (TimeOfDay >= 1)
		{
			TimeOfDay = 0;
		}
	}


	void UpdateSun() 
	{
		Sun.transform.localRotation = Quaternion.Euler ((TimeOfDay * 360f) - 90, 170, 0);

		float intensityMultiplier = 1;
		if (TimeOfDay <= 0.23f || TimeOfDay >= 0.75f)
		{
			intensityMultiplier = 0;
		} 
		else if (TimeOfDay <= 0.25f)
		{
			intensityMultiplier = Mathf.Clamp01 ((TimeOfDay - 0.23f) * (1 / 0.02f));
		} 
		else if (TimeOfDay >= 0.73f)
		{
			intensityMultiplier = Mathf.Clamp01 (1 - ((TimeOfDay - 0.73f) * (1 / 0.02f)));
		}

		Sun.intensity = _initialIntensity * intensityMultiplier;
	}
}