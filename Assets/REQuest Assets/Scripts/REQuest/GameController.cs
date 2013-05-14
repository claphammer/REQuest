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
	public bool hideSelectorOnMove = true;		// hide the selection marker when a unit moves?
	public bool hideMarkersOnMove = true;		// hide the node markers when a unit moves?
	
	#endregion
	// ====================================================================================================================
	#region vars
	
	public enum State : byte { Init=0, Running, DontRun }
	public State state = State.Init;
	private TileNode hoverNode = null;			// that that mouse is hovering over
	private TileNode prevNode = null;			// helper during movement

	public Unit selectedUnit = null;			// currently selected unit
	public bool allowInput { get; set; }
	public int turnNumber = 0;

	public _BartleManager bartle;
	private Dice dice;

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
		
		if(turnNumber == 0)
		{
			selectedUnit.maxMoves = 0;
			selectedUnit.currMoves = 0;
		}
		if (state == State.Init)
		{
			state = State.Running;
			SpawnUnit(); // Call SpawnUnit function
			allowInput = true;
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
		if (allowInput && state == State.Running)
		{
			this.HandleInput();  // check if player clicked on tiles
		}
	}
	
	#endregion
	// ====================================================================================================================
	
	public void ChangeTurn(bool changeTurn)  //cycle turn moves even for single player
	{	
		//Check if currMoves = 0. If not: roll die and update
		if(selectedUnit.currMoves != 0)
		{
								//do nothing
		}
		else
		{
			dice.UpdateRoll();  //call the dice roll class --> it handles updating the selectedUnit.maxMoves
		}
	}

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

				if (hideSelectorOnMove) selectionMarker.Hide();  // hide the selector

				// camera should follow the unit that is moving
				camMover.Follow(selectedUnit.transform);			//CHECK HERE for camera rotation insertion?
				
				//insert Bartle Launch Sequence if the turn# is EVEN and not 0
				if(turnNumber != 0 && (turnNumber & 1) == 0)
				{
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

	#endregion
	// ====================================================================================================================
	#region callbacks

	/// <summary>called when a unit completed something, like moving to a target node</summary>
	private void OnUnitEvent(NaviUnit unit, int eventCode)
	{
		if (eventCode == 1)
		{
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