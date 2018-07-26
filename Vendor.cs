using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour 
{
	public delegate void ModelEvent();
	public static event ModelEvent OnModelUpdate;

	private GameObject[] units;
	// Use this for initialization
	void Start () 
	{
		units = GameObject.FindGameObjectsWithTag("Unit");
	}
	
	private void OnEnable() 
	{
		MouseManager.OnNextRound += HandleNextRound;
	}

	private void OnDisable()
	{
		MouseManager.OnNextRound -= HandleNextRound;
	}

	private void HandleNextRound(string rowName, int column, int color)
	{
		Debug.Log(rowName + ", col: " + column + ", color number: " + color);
		char c = rowName.Split('_')[1][0]; // Row_D, split by '_' and take D
		int rowIndex = (int) c - 65; // the ASCII code for 'A' is 65, thus covert the character to ASCII and minus by 65
		DataModel.chessBoard[rowIndex,column - 1] = color;
		if (units[rowIndex * 8 + 8 - column].transform.GetComponent<ChessManager>().currentColor == 0)
		{
			units[rowIndex * 8 + 8 - column].transform.GetComponent<ChessManager>().currentColor = color;
			Initializer.gameRounds++;
		}
		
		if (OnModelUpdate != null)
		{
			OnModelUpdate();
		}
	}

	private bool CheckSiblings()
	{

		return true;
	}
}
