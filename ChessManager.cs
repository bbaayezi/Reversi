using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessManager : MonoBehaviour 
{
	public int currentColor = 0;

	private void OnEnable() 
	{
		Vendor.OnModelUpdate += UpdateView;
	}

	private void OnDisable()
	{
		Vendor.OnModelUpdate -= UpdateView;
	}

	private void UpdateView()
	{
		if (currentColor != 0)
		{
			if (currentColor == 1 && currentColor != -1)
			{
				transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().enabled = true; // total 3 layers
				// Unit_A-1 -> Chess_Black -> Chess_Unit
			}
			else if (currentColor == -1 && currentColor != 1)
			{
				transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().enabled = true;
			}
		}
	}
}
