using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Initializer : MonoBehaviour 
{

	public GameObject ChessWhite;
	public GameObject ChessBlack;
	public static int gameRounds;
	void Start ()
	{
		int parentIndex = 0;
		GameObject[] parents = GameObject.FindGameObjectsWithTag("Unit");
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				Instantiate<GameObject>(ChessBlack, GetCenterOf(i, j), Quaternion.Euler(0, 0, 0), parents[parentIndex].transform);
				Instantiate<GameObject>(ChessWhite, GetCenterOf(i, j), Quaternion.Euler(0, 0, 0), parents[parentIndex].transform);
				parentIndex++;
			}
		}
		Initialize();
	}

	private Vector3 GetCenterOf(int rowNum, int num)
	{		
		GameObject obj = GameObject.Find("Board_Positioning").transform.GetChild(rowNum).gameObject;
		return obj.transform.GetChild(num).GetComponent<Renderer>().bounds.center;
	}

	private void Initialize()
	{
		DataModel.chessBoard[3, 3] = -1;
        DataModel.chessBoard[3, 4] = 1;
        DataModel.chessBoard[4, 3] = 1;
        DataModel.chessBoard[4, 4] = -1;

		gameRounds = 1;
	}
}
