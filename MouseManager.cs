using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;

public class MouseManager : MonoBehaviour 
{
	public GameObject selectedObject;

	public delegate void BoardEvent(string rowName, int column, int color);
	public static event BoardEvent OnNextRound;
	public Initializer initializer;
	public GameObject[] parents;
	// Use this for initialization
	void Start () 
	{
		initializer = GameObject.FindObjectOfType<Initializer>();
        parents = GameObject.FindGameObjectsWithTag("Unit");
    }
	
	// Update is called once per frame
	void Update () 
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;

		if (Physics.Raycast(ray, out hitInfo))
		{
			GameObject hitObject = hitInfo.transform.gameObject;
			
			SelectObject(hitObject);
		}
	}
	
	void SelectObject(GameObject obj)
	{
		// int col;
		if (selectedObject != null)
		{
			// if (obj == selectedObject) return;
		}
		selectedObject = obj;
		
		if (Input.GetMouseButtonDown(0))
		{
			string pattern = @"\d";
			string result = Regex.Match(obj.name, pattern).ToString();
			int col;
			int.TryParse(result, out col);

			if (OnNextRound != null)
			{
				OnNextRound(obj.transform.parent.gameObject.tag, col, Initializer.gameRounds % 2 == 0 ? 1 : -1); // sending the row information
			}
		}
	}
}
