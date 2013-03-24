// ====================================================================================================================
// Test Gui Script
// RE-Quest  2013
// Will Clapham & Nate Josway
// ====================================================================================================================

using UnityEngine;
using System.Collections;

public class DemoGui : MonoBehaviour 
{
	private GameController game;
	private Rect winRect = new Rect(10f, 10f, 180f, 50f);
	
	private Inventory inv;
	
	void Start()
	{
		game = gameObject.GetComponent<GameController>();
		inv = gameObject.GetComponent<Inventory>();
	}

	void OnGUI()
	{
		winRect = GUILayout.Window(0, winRect, theWindow, "RE-Quest Test GUI");
	}

	private void theWindow(int id)
	{
	// FIRST: Check if the Player is selected
		if (game.selectedUnit == null)
		{
			GUILayout.Label(string.Format("No Player.  Please Select The Character"));
		}
		else
		{
		// Create Slots for Player input and feedback
			//Slot 1: Who is the Player?
			GUILayout.Space(10f);
			GUILayout.Label(string.Format("Player = " + game.selectedUnit));
			
			//Slot 2a: Toggle Inventory Window
			GUILayout.Space(10f);
			inv.setWindow1 = GUILayout.Toggle(inv.setWindow1, "Inventory toggle");
			
			
			//Slot 2: Use Turns Checkbox
			GUILayout.Space(10f);
			game.useTurns = GUILayout.Toggle(game.useTurns, "USE TURNS");
				
			if (game.useTurns)
			{
			
			//Slot 3: Moves Left (currMoves) Display
				GUILayout.Space(10f);
				GUILayout.Label(string.Format("Moves Left: "+ game.selectedUnit.currMoves));
			//Slot 4: Max Moves (maxMoves) Display
				GUILayout.Label(string.Format("Max moves (Roll) this turn: " + game.selectedUnit.maxMoves));
			//Slot 5: Reset Turn...SOON TO BE ROLL DIE	
				if (game.allowInput)
				{
					GUILayout.Space(10f);
					if (GUILayout.Button("Reset Turn / Roll Die")) game.ChangeTurn();
				}
			}
		}
	}	
}
