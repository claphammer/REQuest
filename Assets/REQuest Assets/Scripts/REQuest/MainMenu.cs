// ====================================================================================================================
// Test Gui Script
// RE-Quest  2013
// Nate Josway
// ====================================================================================================================

using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	
	private Rect winRect2 = new Rect(Screen.width/2, Screen.height/2, 180f, 80f);
		
	void Start()
	{
		
	}
	
    void OnGUI() 
	{
        winRect2 = GUILayout.Window(1, winRect2, DoMyWindow2, "");
    }
	
	void DoMyWindow2(int windowID)
	{
	// Main Menu Buttons
		//Slot 1: New Game
		GUILayout.Space(10f);
		if(GUILayout.Button("New Game")){
			Application.LoadLevel("REQuestDemo02");
			Debug.Log("New Game Pressed");
		}
		
		//Slot 2: Load/Continue Game
		GUILayout.Space(10f);
		if(GUILayout.Button("Load Game")){
			
			Debug.Log("Load Game Pressed");
		}
		/*
		//Slot 3: Exit Game
		//DO NOT ENABLE BUTTON FOR iOS, will also not work in Web player nor Editor
		GUILayout.Space(10f);
		if(GUILayout.Button("Exit Game")){
			Application.Quit();
			Debug.Log("Exit Game Pressed");
		}*/
	}
}	