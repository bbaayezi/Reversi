using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectIndicator : MonoBehaviour 
{
	MouseManager mm;
	// Use this for initialization
	void Start () 
	{
		mm = GameObject.FindObjectOfType<MouseManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (mm.selectedObject != null)
		{
			Bounds bounds = mm.selectedObject.GetComponentInChildren<Renderer>().bounds;
			transform.position = new Vector3 (bounds.center.x, 7.21f, bounds.center.z);
			// Debug.Log(bounds + ", " + transform.position);
			
		}
	}
}
