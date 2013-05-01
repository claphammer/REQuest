// ====================================================================================================================
// Test Unit Script
// RE-Quest  2013
// Will Clapham & Nate Josway
// ====================================================================================================================

using UnityEngine;

public class Unit : NaviUnit
{
	// ====================================================================================================================
	#region inspector properties

	public int maxMoves =  2;			// how far this unit can move per turn

	#endregion
	// ====================================================================================================================
	#region vars

	//[HideInInspector]
	public int currMoves = 2; // how many moves this unit has left
			
	#endregion
// ====================================================================================================================
	#region pub

/// <summary>Should be called right after unit was spawned</summary>
	public override void Init(UnitEventDelegate callback)
	{
		base.Init(callback);
		//this.Reset();		
	}

/// <summary>Reset some values</summary>
	public void Reset()
	{
		currMoves = maxMoves;
	}

	#endregion
	// ====================================================================================================================
}
