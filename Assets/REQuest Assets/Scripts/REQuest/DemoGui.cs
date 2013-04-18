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
	private Inventory inv;
	private QuestLog quest;
	
	private Rect winRect1 = new Rect(10f, 10f, 200f, 80f);
	private Rect winRect2 = new Rect(10f, 200f, 200f, 80f);
	
	static public bool test = false;

	void Start()
	{
		game = gameObject.GetComponent<GameController>();
		inv = gameObject.GetComponent<Inventory>();
		quest = gameObject.GetComponent<QuestLog>();
	}
	
    void OnGUI() 
	{
        winRect1 = GUILayout.Window(0, winRect1, DoMyWindow1, "Instructions");
        winRect1 = GUILayout.Window(3, winRect1, DoMyWindow1, "Misc Info");
        winRect2 = GUILayout.Window(1, winRect2, DoMyWindow2, "RE-Quest Test GUI");
    }
	
    void DoMyWindow1(int windowID)
	{
		GUILayout.Label(string.Format("Move the Player along path to red target."));
		GUILayout.Label(string.Format("Click a 'lit' tile to move."));
		GUILayout.Label(string.Format("<cntl>+click to rotate camera"));
		GUILayout.Label(string.Format("When Player is out of moves:"));
		GUILayout.Label(string.Format("  1) roll dice"));
		GUILayout.Label(string.Format("  2) reselect player"));
    }
	
	void DoMyWindow2(int windowID)
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
			quest.setWindow1 = GUILayout.Toggle(quest.setWindow1, "Quest Log");
			
			//Slot 2: Use Turns Checkbox
			GUILayout.Space(10f);
			game.useTurns = GUILayout.Toggle(game.useTurns, "USE TURNS");
				
			//if (game.useTurns)
			//{
			
			//Slot 3: Moves Left (currMoves) Display
				GUILayout.Space(10f);
				GUILayout.Label(string.Format("Moves Left: "+ game.selectedUnit.currMoves));
			//Slot 4: Max Moves (maxMoves) Display
				GUILayout.Label(string.Format("Max moves (Roll) this turn: " + game.selectedUnit.maxMoves));
			//Slot 5: Reset Turn...SOON TO BE ROLL DIE	
				if (game.allowInput)
				{
					GUILayout.Space(10f);
					if (GUILayout.Button("Reset Turn / Roll Die")){ 
					game.ChangeTurn(true);
					}
				}
			//}
		}
	}
}
	
	
	
	
	
	
	/*
	
	


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
*/