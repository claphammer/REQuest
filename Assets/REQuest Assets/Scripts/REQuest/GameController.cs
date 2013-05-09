// ====================================================================================================================
// Test Game Controller Script
// RE-Quest  2013
// Will Clapham & Nate Joswa12y
// ====================================================================================================================

using UnityEngine;
using System.Collections.Generic;

public class GameController : TMNController
{
	// ====================================================================================================================
	#region inspector properties

	public CameraMove camMover;					// used to move camera around (like make it follow a transform)
	public SelectionIndicator selectionMarker;	// used to indicate which unit is active/selected
	public GameObject playerChar;				// SINGLE unit prefab	
	public TileNode playerSpawnPoint;			// the tile where the unit will start
	public bool hideSelectorOnMove = true;		// hide the selection marker when a unit moves?
	public bool hideMarkersOnMove = true;		// hide the node markers when a unit moves?
	
	#endregion
	// ====================================================================================================================
	#region vars
	
	public enum State : byte { Init=0, Running, DontRun }
	public State state = State.Init;
	private TileNode hoverNode = null;			// that that mouse is hovering over
	private TileNode prevNode = null;			// helper during movement

	public bool useTurns = false;				// allow "max moves" to be broken down into sub-moves without a reset
	public Unit selectedUnit = null;			// currently selected unit
	public bool allowInput { get; set; }
	
	public int turnNumber = 0;
	
	private _BartleManager bartle;
	private Dice dice;
	private Die die;
	

	//public int currPlayerTurn  { get; set; }	// which player's turn it is, only if useTurns = true;

	#endregion
	// ====================================================================================================================
	#region start/init
	
	 public override void Start()
	{
		base.Start();
		allowInput = false;
		state = State.Init;
		bartle = GetComponentInChildren<_BartleManager>();
		bartle.ResetQuestions();
		dice = GetComponent<Dice>();
		die = GetComponent<Die>();
		
		if(turnNumber == 0)
		{
			selectedUnit.maxMoves = 0;
			selectedUnit.currMoves = 0;
		}
	}
	
	private void SpawnUnit()
	{
		//set Player Starting node to node 478
		TileNode node = map[478];
		
		// spawn the unit
		Unit unit = (Unit)Unit.SpawnUnit(playerChar.gameObject, map, node);
		unit.Init(OnUnitEvent);
		unit.name = "iAmPlayer";
		
		// Select Unit (camera will auto-bind to the selected unit)
		this.OnNaviUnitClick(unit.gameObject);
		allowInput = true; // allow input again
	}

	#endregion
	// ====================================================================================================================
	#region update/input

	public void Update()
	{		
		if (state == State.Running)
		{
			// check if player clicked on tiles/units. 
			// You could choose not to call this in certain frames,
			// for example if your GUI handled the input this frame and you don't want the player 
			// clicking 'through' GUI elements onto the tiles or units
			if (allowInput) this.HandleInput();
		}
		else if (state == State.Init)
		{
			print(state);
			state = State.Running;
			print(state);
			SpawnUnit(); // Call SpawnUnit function
			allowInput = true;
		}
	}
	
	#endregion
	// ====================================================================================================================
	#region pub
	
	public int DieValue()
    {
      //  get
       // {
            return die.val;

        //}
    }
	
	
	public void ChangeTurn(bool changeTurn)  //cycle turn moves even for single player
	{	
		
		
		//Check if currMoves = 0. If not: roll die and update
		if(selectedUnit.currMoves != 0)
		{
			//do nothing
		}
		else
		{
			// Simple Randomizer for Roll Dice
			//selectedUnit.maxMoves = Random.Range(1,6);

			dice.UpdateRoll();
			
	



			
			
			
			

		
			// Reset call
			selectedUnit.Reset(); // Reset selected Unit's CurrMoves to match new MaxMoves value
			print("reset unit just called from gamecontroller change turn");
							
			//
			turnNumber++;
			bartle.questionAsked = false;
		}
	}

	
	#endregion
	// ====================================================================================================================
	#region input handlers - click tile

	protected override void OnTileNodeClick(GameObject go)
	{
		base.OnTileNodeClick(go);
		TileNode node = go.GetComponent<TileNode>();
		if (selectedUnit != null && node.IsVisible)
		{
			prevNode = selectedUnit.node; // needed if unit is gonna move
			if (selectedUnit.MoveTo(node, ref selectedUnit.currMoves))
			{
				// dont want the player clicking around while a unit is moving
				allowInput = false;

				// hide the node markers when unit is moving. Note that the unit is allready linked with
				// the destination node by now. So use the cached node ref
				if (hideMarkersOnMove) prevNode.ShowNeighbours(((Unit)selectedUnit).maxMoves, false);

				// hide the selector
				if (hideSelectorOnMove) selectionMarker.Hide();

				// camera should follow the unit that is moving
				//camMover.Follow(selectedUnit.transform);
				
				//insert Bartle Launch Sequence
				if(turnNumber != 0 && (turnNumber & 1) == 0)
				{
					print("I am launching Dialog Manager");
					bartle.QuestionPicker();
				}
				
			}
		}
	}

	protected override void OnTileNodeHover(GameObject go)
	{
		base.OnTileNodeHover(go);
		if (go == null)
		{	// go==null means TMNController wanna tell that mouse moved off but not onto another visible tile
			if (hoverNode != null)
			{
				hoverNode.OnHover(false);
				hoverNode = null;
			}
			return;
		}

		TileNode node = go.GetComponent<TileNode>();
		if (selectedUnit != null && node.IsVisible)
		{
			if (hoverNode != node)
			{
				if (hoverNode != null) hoverNode.OnHover(false);
				hoverNode = node;
				node.OnHover(true);
			}
		}
		else if (hoverNode != null)
		{
			hoverNode.OnHover(false);
			hoverNode = null;
		}
	}

	#endregion
	// ====================================================================================================================
	#region input handlers - click unit

	public override void OnNaviUnitClick(GameObject go)
	{
		base.OnNaviUnitClick(go);

		Unit unit = go.GetComponent<Unit>();
		
		// jump camera to the unit that was clicked on (is selected)
		camMover.Follow(go.transform);

		// -----------------------------------------------------------------------
		// not using turns sample?
	/*	if (useTurns)
		{
			// is active player's unit that was clicked on?
			//if (unit.playerSide == (currPlayerTurn + 1))
			//{
				selectedUnit = go.GetComponent<Unit>();
			Debug.Log("Selected Unit is: " + selectedUnit);
				// move selector to the clicked unit to indicate it's selection
				selectionMarker.Show(go.transform);

				// show the nodes that this unit can move to
				selectedUnit.node.ShowNeighbours(selectedUnit.currMoves, selectedUnit.tileLevel, true, true);

		}
		 */
		
		// -----------------------------------------------------------------------
		// using turns sample
	//	else
	//	{
			bool changeUnit = true;

			if (changeUnit)
			{
				selectedUnit = unit;

				// move selector to the clicked unit to indicate it's selection
				selectionMarker.Show(go.transform);

				// show the nodes that this unit can move to
				selectedUnit.node.ShowNeighbours(selectedUnit.currMoves, selectedUnit.tileLevel, true, true);
			}
	//	}

	}

	
	
	#endregion
	// ====================================================================================================================
	#region callbacks

	/// <summary>called when a unit completed something, like moving to a target node</summary>
	private void OnUnitEvent(NaviUnit unit, int eventCode)
	{
		// eventcode 1 = unit finished moving
		if (eventCode == 1)
		{
			//Unit u = (unit as Unit);

		//	if (!useTurns)
		//	{
		//		u.currMoves = u.maxMoves; // units constantly get rest to max after completing a move sequence
		//	}

			if (!hideMarkersOnMove && prevNode != null)
			{	// the markers where not hidden when the unit started moving,
				// then they should be now as they are invalid now
				prevNode.ShowNeighbours(((Unit)selectedUnit).maxMoves, false);
			}

			// do a fake click on the unit to "select" it again
			this.OnNaviUnitClick(unit.gameObject);
			allowInput = true; // allow input again
		}
	}

	}

	#endregion
	// ====================================================================================================================