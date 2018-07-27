using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Vendor : MonoBehaviour 
{
	public delegate void ModelEvent();
	public static event ModelEvent OnModelUpdate;

	public static GameObject[] units;

	private int currentRowIndex;
	private int currentColumnIndex;
    private int color;
	void Start () 
	{
		units = GameObject.FindGameObjectsWithTag("Unit");
        DataModel.chessBoard[3, 3] = -1;
        DataModel.chessBoard[3, 4] = 1;
        DataModel.chessBoard[4, 3] = 1;
        DataModel.chessBoard[4, 4] = -1;
        if (OnModelUpdate != null)
        {
            OnModelUpdate();
        }
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
		
		char c = rowName.Split('_')[1][0]; // Row_D, split by '_' and take D
		int rowIndex = (int) c - 65; // the ASCII code for 'A' is 65, thus covert the character to ASCII and minus by 65
		// DataModel.chessBoard[rowIndex,column - 1] = color;

		currentRowIndex = rowIndex;
		currentColumnIndex = column - 1;
        this.color = color;
		//Debug.Log(currentRowIndex + ", col: " + currentColumnIndex + ", color number: " + color);


        // TODO: CheckMovement
        // @params: int currentRow, int currentColumn, int currentColor, out int[,] newChessBoard
        // checking directions including: North, South, East, West, Northeast, Northwest, Southeast, Southwest
        if (!CheckMovement(currentRowIndex, currentColumnIndex, color, out DataModel.chessBoard))
        {
            Debug.Log("Invalid operation!");
            Initializer.gameRounds--;
        }



        //if (units[rowIndex * 8 + 8 - column].transform.GetComponent<ChessManager>().currentColor == 0)
        //{
        //    units[rowIndex * 8 + 8 - column].transform.GetComponent<ChessManager>().currentColor = color;
        Initializer.gameRounds++;
        //}

        // update event
        if (OnModelUpdate != null)
		{
			OnModelUpdate();
		}
	}

	// CheckMovement method implementation
	private bool CheckMovement(int currentRow, int currentColumn, int currentColor, out int[,] newChessBoard)
	{
		// make a deep clone of the original chess board
		newChessBoard = (int[,])DataModel.chessBoard.Clone();
		int upperBounds;
		int lowerBounds;
        // implement the color
        //newChessBoard[currentRow, currentColumn] = color;
		// TODO: SearchVertical, SearchHorizontal
		// @params: int[,] chessBoard, out int upperBounds, out int lowerBounds

		// SearchVertical
		bool searchVer = SearchVertical(ref newChessBoard, out lowerBounds, out upperBounds);
		// immediate update
		//Debug.Log(upperBounds + ", " + lowerBounds);
		
		if (searchVer)
        {
            for (int i = lowerBounds; i <= upperBounds; i++)
            {
                newChessBoard[i, currentColumn] = color;
            }
        }

		// SearchHorizontal
		bool searchHor = SearchHorizontal(ref newChessBoard, out lowerBounds, out upperBounds);
		// variable updated
		// immediately update the chess board

		//Debug.Log(upperBounds + ", " + lowerBounds);
		if (searchHor)
        {
            for (int i = lowerBounds; i <= upperBounds; i++)
            {
                newChessBoard[currentRow, i] = color;
            }
        }
        bool searchObLeft = SearchObliqueLeft(ref newChessBoard, out lowerBounds, out upperBounds);

        // immediately update
        if (searchObLeft)
        {
            if (currentRow == upperBounds)
            {
                for (int i = upperBounds, j = currentColumn; i >= lowerBounds; i--, j--)
                {
                    newChessBoard[i, j] = color;
                }
            }
            else if (currentRow < upperBounds)
            {
                for (int i = lowerBounds, j = currentColumn; i <= upperBounds; i++, j++)
                {
                    newChessBoard[i, j] = color;
                }
            }
        }
        //

        bool searchObRight = SearChObliqueRight(ref newChessBoard, out lowerBounds, out upperBounds);

        if (searchObRight)
        {
            if (currentRow == upperBounds)
            {
                for (int i = upperBounds, j = currentColumn; i >= lowerBounds; i--, j++)
                {
                    newChessBoard[i, j] = color;
                }
            }
            else if (currentRow < upperBounds)
            {
                for (int i = lowerBounds, j = currentColumn; i <= upperBounds; i++, j--)
                {
                    newChessBoard[i, j] = color;
                }
            }
            
        }
        Debug.Log(lowerBounds);
        if (searchVer || searchHor || searchObLeft || searchObRight)
        {
            return true;
        }
        // TODO: SearchObliqueLeft, SearchObliqueRight
        // @params: 
        

        return false;
	}

	private bool SearchVertical(ref int[,] chessBoard, out int lowerBounds, out int upperBounds)
	{
		lowerBounds = upperBounds = currentRowIndex;
		// search upwards
		for (int i = currentRowIndex - 1; i >= 0; i--)
		{
            if (chessBoard[i, currentColumnIndex] == 0) break;
			if (color == chessBoard[i, currentColumnIndex])
            {
                lowerBounds = i + 1;
                break;
            }
		}
		// search downwards
		for (int j = currentRowIndex + 1; j < 8; j++)
		{
            if (chessBoard[j, currentColumnIndex] == 0) break;
            if (color == chessBoard[j, currentColumnIndex])
            {
                upperBounds = j - 1;
                break;
            }
        }

        return upperBounds == lowerBounds ? false : true;
	}
	private bool SearchHorizontal(ref int[,] chessBoard, out int lowerBounds, out int upperBounds)
	{
		lowerBounds = upperBounds = currentColumnIndex;
		// search left side
		for (int i = currentColumnIndex - 1; i >= 0; i--)
		{
            if (chessBoard[currentRowIndex, i] == 0) break;
			if (color == chessBoard[currentRowIndex, i])
            {
                lowerBounds = i + 1;
                break;
            }
        }
		// search right side
		for (int j = currentColumnIndex + 1; j < 8; j++)
		{
            if (chessBoard[currentRowIndex, j] == 0) break;
			if (color == chessBoard[currentRowIndex, j])
            {
                upperBounds = j - 1;
                break;
            }
        }
        return upperBounds == lowerBounds ? false : true;
    }

    private bool SearchObliqueLeft(ref int[,] chessBoard, out int lowerBounds, out int upperBounds)
    {
        lowerBounds = upperBounds = currentRowIndex;
        // check northeast
        try
        {
            for (int i = currentRowIndex, j = currentColumnIndex; i >= 0 && j >= 0; i--, j--)
            {
                if (chessBoard[i - 1, j - 1] == 0) break;
                if (chessBoard[i - 1, j - 1] == color)
                {
                    lowerBounds = i;
                    break;
                }
            }
        }catch(Exception e)
        {
            
        }
        // check southwest
        try
        {
            for (int i = currentRowIndex, j = currentColumnIndex; i < 8 && j < 8; i++, j++)
            {
                if (chessBoard[i + 1, j + 1] == 0) break;
                if (chessBoard[i + 1, j + 1] == color)
                {
                    upperBounds = i;
                    break;
                }
            }
        }catch(Exception e)
        {

        }
        return upperBounds == lowerBounds ? false : true;
    }
    private bool SearChObliqueRight(ref int[,] chessBoard, out int lowerBounds, out int upperBounds)
    {
        lowerBounds = upperBounds = currentRowIndex;
        // check northwest
        try
        {
            for (int i = currentRowIndex, j = currentColumnIndex; i >= 0 && j < 8; i--, j++)
            {
                if (chessBoard[i - 1, j + 1] == 0) break;
                if (chessBoard[i - 1, j + 1] == color)
                {
                    lowerBounds = i;
                    break;
                }
            }
        }catch(IndexOutOfRangeException e)
        {

        }


        // check southeast
        try
        {
            for (int i = currentRowIndex, j = currentColumnIndex; i < 8 && j >= 0; i++, j--)
            {
                if (chessBoard[i + 1, j - 1] == 0) break;
                if (chessBoard[i + 1, j - 1] == color)
                {
                    upperBounds = i;
                    break;
                }
            }
        }catch(IndexOutOfRangeException e)
        {

        }

        return upperBounds == lowerBounds ? false : true;
    }
}
