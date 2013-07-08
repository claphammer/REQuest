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
	public float scoreAch;
	public float scoreExp;
	public float scoreSoc;
	public float scoreKil;
	
	private float questionsAsked = 0;
	private Inventory inv;
	private QuestLog quest;
	private GameController game;
	private Rect winRect1 = new Rect(10f, 10f, 200f, 80f);
	
	void Start()
	{
		game = GetComponent<GameController>();
		inv = GetComponent<Inventory>();
		quest = GetComponent<QuestLog>();
		scoreAch = (float)DialogUI.GetTokenAsFloat("Achiever");  //ignore MONO COMPILE ERROR
		scoreExp = (float)DialogUI.GetTokenAsFloat("Explorer");  //ignore MONO COMPILE ERROR
		scoreSoc = (float)DialogUI.GetTokenAsFloat("Socializer");  //ignore MONO COMPILE ERROR
		scoreKil = (float)DialogUI.GetTokenAsFloat("Killer");  //ignore MONO COMPILE ERROR
	}
	
	void OnGUI()
	{
		//MiniMapWindow
		if(isDrawnMM == true)
			if(Event.current.type == EventType.Repaint)
				//Graphics.DrawTexture(new Rect(Screen.width - sizeMM - offsetMM, offsetMM, sizeMM, sizeMM), miniMapTexture, miniMapMaterial); //Top Right
				Graphics.DrawTexture(new Rect(Screen.width - sizeMM - offsetMM, Screen.height - sizeMM -offsetMM, sizeMM, sizeMM), miniMapTexture, miniMapMaterial);
		
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
					
		if (questionsAsked == 30)
			{
				float SocialTotal = ((scoreSoc/30)*2);
				float AchieverTotal = ((scoreAch/30)*2);
				float KillerTotal = ((scoreKil/30)*2);
				float ExplorerTotal = ((scoreExp/30)*2);
				float w = 400;
				float h = 300;
  				Rect rect = new  Rect((Screen.width-w)/2, (Screen.height-h)/2, w, h);
		  		GUI.Label(rect, "Your Results are... Social: " + (SocialTotal).ToString() + ", Achiever: " + (AchieverTotal).ToString() + ", Killer: " + (KillerTotal).ToString() + ", Explorer: " + (ExplorerTotal).ToString() + " ... **Totals are out of 200%.  No single category will be over 100%**");
			}
		
		
		//Other Gui Window
		winRect1 = GUILayout.Window(1, winRect1, DoMyWindow1, "RE-Quest Test GUI");
		
		//Right button
		if(GUI.Button(new Rect (Screen.width - 450,Screen.height - 75,180,60), "Turn right"))
		{
			game.ManualPlayerRotateR();
		}
		//Left button
		if(GUI.Button(new Rect (Screen.width - 650,Screen.height - 75,180,60), "Turn left"))
		{
			game.ManualPlayerRotateL();
		}
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
			GUILayout.Label(string.Format("Turn # = " + game.turnNumber));
		
			//Slot 5: Moves Left (currMoves) Display
			//GUILayout.Space(10f);
			GUILayout.Label(string.Format("Moves Left This Turn: "+ game.selectedUnit.currMoves));
			
			GUILayout.Space(10f);
			if (GUILayout.Button("Return to Main Menu"))
            	Application.LoadLevel("REQuestDemo_Menu");
			
			//Slot 6: Bartle
			
			scoreAch = (float)DialogUI.GetTokenAsFloat("Achiever");
			scoreExp = (float)DialogUI.GetTokenAsFloat("Explorer");
			scoreSoc = (float)DialogUI.GetTokenAsFloat("Socializer");
			scoreKil = (float)DialogUI.GetTokenAsFloat("Killer");
			questionsAsked = (scoreAch + scoreExp + scoreSoc + scoreKil);
			GUILayout.Space(10f);
			GUILayout.Label("Questions Answered: " + questionsAsked + "/30");
			//GUILayout.Space(10f);
			GUILayout.Label("Ach:" + (scoreAch).ToString() + "  Exp:" + (scoreExp).ToString() + "  Soc:" + (scoreSoc).ToString() + "  Kil:" + (scoreKil).ToString()); 
			
		}
	}
	
}






