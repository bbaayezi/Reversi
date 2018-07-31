using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIEvent : MonoBehaviour 
{
	public static event Vendor.ModelEvent OnRestartUpdate;
	public void BtnClick()
	{
		// Restart the game
		Initializer.Initialize();
		// Update view
		
		if (OnRestartUpdate != null)
		{
			OnRestartUpdate();
		}
	}
}
