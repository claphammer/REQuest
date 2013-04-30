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
	
	public bool test = false;
	public _BartleManager script;

	#endregion
	// ====================================================================================================================
	#region vars
	
	private enum State : byte { Init=0, Running, DontRun }
	private State state = State.Init;
	private TileNode hoverNode = null;			// that that mouse is hovering over
	private TileNode prevNode = null;			// helper during movement
	
	public bool useTurns = false;				// allow "max moves" to be broken down into sub-moves without a reset
	public Unit selectedUnit = null;			// currently selected unit
	public bool allowInput { get; set; }
	
	public static int turnNumber = 0;
	
	// * unused list stuff - can be refactored for inventory
	//private List<Unit>[] units = {
	//	new List<Unit>(),						// player 1's units
	//	new List<Unit>()						// player 2's units
	//};

	public int currPlayerTurn  { get; set; }	// which player's turn it is, only if useTurns = true;

	#endregion
	// ====================================================================================================================
	#region start/init
	
	 public override void Start()
	{
		base.Start();
		allowInput = false;
		state = State.Init;		
	
	}
	
	private void SpawnUnit()
	{
		//Find the inspector referenced spawn location and name it "node"
		TileNode node = playerSpawnPoint;
				Debug.Log(node);
		
		//set "node" specifically to node 478
		node = map[478];
				Debug.Log(node);
		
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
		//Find the spawn location
		TileNode node = playerSpawnPoint;
		
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
			Debug.Log("State should be INIT.  Survey Says: " + state + " and about to spawn the player at: " + node);
			state = State.Running;
			SpawnUnit(); // Call SpawnUnit function
			allowInput = true;
		}
		
		if(MovementTrigger.trig2 == 0 && turnNumber == 0){
			selectedUnit.maxMoves = 6;
			selectedUnit.currMoves = 6;
			turnNumber++;
		}
		/*if(MovementTrigger.trig2 == 1 && turnNumber == 1){
			selectedUnit.currMoves = 0;
			turnNumber++;
		}*/

}

	#endregion
	// ====================================================================================================================
	#region pub
	
	public void ChangeTurn(bool changeTurn)  //cycle turn moves even for single player
	{	
		if(turnNumber > 0){
			//Check if currMoves = 0. If not, do roll die and updare
			if(selectedUnit.currMoves != 0){
				//selectedUnit.currMoves = selectedUnit.currMoves;
			}else{
			// Roll Dice
			selectedUnit.maxMoves = Random.Range(1,6);
		
			// Reset call
			selectedUnit.Reset(); // Reset selected Unit's CurrMoves to match new MaxMoves value
				
			turnNumber++;
			}
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
					script.QuestionPicker();
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

	protected override void OnNaviUnitClick(GameObject go)
	{
		base.OnNaviUnitClick(go);

		Unit unit = go.GetComponent<Unit>();
		
		// jump camera to the unit that was clicked on (is selected)
		camMover.Follow(go.transform);

		// -----------------------------------------------------------------------
		// not using turns sample?
		if (useTurns)
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

		
		// -----------------------------------------------------------------------
		// using turns sample
		else
		{
			bool changeUnit = true;

			if (changeUnit)
			{
				selectedUnit = unit;

				// move selector to the clicked unit to indicate it's selection
				selectionMarker.Show(go.transform);

				// show the nodes that this unit can move to
				selectedUnit.node.ShowNeighbours(selectedUnit.currMoves, selectedUnit.tileLevel, true, true);
			}
		}

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
			Unit u = (unit as Unit);

			if (!useTurns)
			{
				u.currMoves = u.maxMoves; // units constantly get rest to max after completing a move sequence
			}

			if (!hideMarkersOnMove && prevNode != null)
			{	// the markers where not hidden when the unit started moving,
				// then they should be now as they are invalid now
				prevNode.ShowNeighbours(((Unit)selectedUnit).maxMoves, false);
			}

			// do a fake click on the unit to "select" it again
			//this.OnNaviUnitClick(unit.gameObject);
			allowInput = true; // allow input again
		}
	}

	}

	#endregion
	// ====================================================================================================================