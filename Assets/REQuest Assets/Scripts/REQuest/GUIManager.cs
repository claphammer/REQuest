using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour 
{
	public RenderTexture dieRollTexture;
	public RenderTexture miniMapTexture;
	public Material dieRollMaterial;
	public Material miniMapMaterial;
	public int sizeDR;
	public int sizeMM;
	public float offsetDR;
	public float offsetMM;
	public bool isDrawnDR = true; //allow dieroll to be toggled from an external button
	public bool isDrawnMM = true; //allow minimap to be toggled from an external button
	
	private Inventory inv;
	private QuestLog quest;
	private GameController game;
	//private Dice dice;
	private Rect winRect1 = new Rect(10f, 10f, 200f, 80f);
	
	void Start()
	{
		game = GetComponent<GameController>();
		inv = GetComponent<Inventory>();
		quest = GetComponent<QuestLog>();
		//dice = GetComponent<Dice>();
	}

	void OnGUI()
	{
		//MiniMapWindow
		if(isDrawnMM == true)
			if(Event.current.type == EventType.Repaint)
				Graphics.DrawTexture(new Rect(Screen.width - sizeMM - offsetMM, offsetMM, sizeMM, sizeMM), miniMapTexture, miniMapMaterial);
		
		//DieRollWindow
		if(isDrawnDR == true)
			if(Event.current.type == EventType.Repaint)
				Graphics.DrawTexture(new Rect(offsetDR, Screen.height - sizeDR - offsetDR, sizeDR, sizeDR), dieRollTexture, dieRollMaterial);
		if (game.allowInput)
		{
			if (GUI.Button(new Rect (20,Screen.height - 200,180,20), "Roll Die"))
			{ 
				game.ChangeTurn(true);
			}
			Dice.Value(""); //instantiates the Value method to give back to the player		
		}
		
		
		//Other Gui Window
		winRect1 = GUILayout.Window(1, winRect1, DoMyWindow1, "RE-Quest Test GUI");
		
	}
	
	void DoMyWindow1(int windowID)
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
			GUILayout.Label(string.Format("Player= " + game.selectedUnit));

			//Slot 2: Toggle Inventory Window
			GUILayout.Space(10f);
			inv.setWindow1 = GUILayout.Toggle(inv.setWindow1, "Inventory toggle");

			//Slot 3: Toggle Quest Window
			GUILayout.Space(10f);
			quest.setWindow1 = GUILayout.Toggle(quest.setWindow1, "Quest Log");

			//Slot 4: Display Turn Number
			GUILayout.Space(10f);
			GUILayout.Label(string.Format("Turn# = " + game.turnNumber));
		
			//Slot 5: Moves Left (currMoves) Display
			GUILayout.Space(10f);
			GUILayout.Label(string.Format("Moves Left: "+ game.selectedUnit.currMoves));	
		}
	}
	
}






