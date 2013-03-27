// ====================================================================================================================
//
// REQuest 2013
//
// ====================================================================================================================

using UnityEngine;
using System.Collections;

public class CollectionItem : MonoBehaviour 
//public class CollectionItem : Trigger 

	
{

	//public GameObject Dice; //collectable item
	public bool isVisible = true; 
	

	
	// Update is called once per frame
	void Update () 
	{
	if (Trigger.trig1 == 1)
		{
		this.renderer.enabled = false;
		}
	}

}
	