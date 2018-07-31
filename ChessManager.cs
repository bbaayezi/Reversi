using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessManager : MonoBehaviour 
{
	public int currentColor = 0;

	private void OnEnable() 
	{
		Vendor.OnModelUpdate += UpdateView;
        GUIEvent.OnRestartUpdate += UpdateView;
	}

	private void OnDisable()
	{
		Vendor.OnModelUpdate -= UpdateView;
        GUIEvent.OnRestartUpdate -= UpdateView;
	}

	private void UpdateView()
	{
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (DataModel.chessBoard[i, j] == 1)
                {
                    Vendor.units[8 * i + 7 - j].transform.GetChild(1).GetChild(0)
                        .GetComponent<MeshRenderer>().enabled = true;
                    Vendor.units[8 * i + 7 - j].transform.GetChild(0).GetChild(0)
                        .GetComponent<MeshRenderer>().enabled = false;
                }
                else if (DataModel.chessBoard[i, j] == -1)
                {
                    Vendor.units[8 * i + 7 - j].transform.GetChild(0).GetChild(0)
                        .GetComponent<MeshRenderer>().enabled = true;
                    Vendor.units[8 * i + 7 - j].transform.GetChild(1).GetChild(0)
                        .GetComponent<MeshRenderer>().enabled = false;
                }
                else
                {
                    Vendor.units[8 * i + 7 - j].transform.GetChild(0).GetChild(0)
                        .GetComponent<MeshRenderer>().enabled = false;
                    Vendor.units[8 * i + 7 - j].transform.GetChild(1).GetChild(0)
                        .GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }
	}
}
