// ====================================================================================================================
// Created by Leslie Young
// http://www.plyoung.com/ or http://plyoung.wordpress.com/
// ====================================================================================================================

using UnityEngine;

public abstract class TMNController : MonoBehaviour 
{
	// ====================================================================================================================

	public Camera rayCam;	// the main game camera should be linked here
	public MapNav map;		// the MapNav used with this controller
	public int unitsLayer=21;	// on what layer is units

	// ====================================================================================================================

	private GameObject _selectedUnitGo = null;	// the currently selected unit
	private GameObject _hoverNodeGo = null;		// node that mouse is hovering over
	private LayerMask _rayMask = 0;				// used to determine what can be clicked on (Tiles and Units) Inited in Start()

	// ====================================================================================================================

	public virtual void Start()
	{
		if (map == null)
		{
			Debug.LogWarning("The 'map' property was not set, attempting to find a MapNav in the scene.");
			Object obj = GameObject.FindObjectOfType(typeof(MapNav));
			if (obj != null) map = obj as MapNav;

			// I'm not gonan do extra if() tests in the HandleInput.. tell coder now there is problem he should be sorting out asap
			if (map == null) Debug.LogError("Could not find a MapNav in the scene. You gonna get NullRef errors soon!");
		}

		_rayMask = (1<<map.tilesLayer);
	}
	
	// ====================================================================================================================

	public void UnitActivate()
	{
		OnNaviUnitClick(_selectedUnitGo);  //*** call the unit click method passing it the already selected unit...Not the raycast listener
	}
	
	//Call this every frame to handle input - For Tiles only now.
	protected void HandleInput()
	{
		Ray ray = rayCam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 500f, _rayMask))
		{
			// *** Ray hit a Tile
			if (hit.collider.gameObject.layer == map.tilesLayer)
			{
				if (Input.GetMouseButtonUp(0))  // mouse-click/touch detected
				{	
					OnTileNodeClick(hit.collider.gameObject);
				}
				else
				{	// else, mouse hovering over tile
					OnTileNodeHover(hit.collider.gameObject);
				}
			}
			else if (_hoverNodeGo != null)
			{
				OnTileNodeHover(null);
			}
		}
	}

	// ====================================================================================================================

	/// <summary>Handles tile clicks</summary>
	protected virtual void OnTileNodeClick(GameObject nodeGo)
	{
	}

	/// <summary>Handles mouse cursor hover over tile</summary>
	protected virtual void OnTileNodeHover(GameObject nodeGo)
	{
		_hoverNodeGo = nodeGo;
	}

	/// <summary>Handles unit clicks</summary>
	public virtual void OnNaviUnitClick(GameObject unitGo)
	{
		_selectedUnitGo = unitGo;
		//print("NaviUnitClick says i am: " + _selectedUnitGo);
	}
	// ====================================================================================================================
}
