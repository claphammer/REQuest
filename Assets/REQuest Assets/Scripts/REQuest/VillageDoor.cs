// ====================================================================================================================
// Created Nate Josway
// ====================================================================================================================

using UnityEngine;
using System.Collections;

public class VillageDoor : MonoBehaviour 
{
	public GameObject doorPart;	// the part that will be shown/hidden
	public TileNode tileNode;	// the tile this door is occupying
	public TileNode triggerNode;
	static public bool isOpen = false;	// state of the door
	public bool messageDelay = false; //true activates message popup coroutine. false means trigger is idle
	
	void Update()
	{
		// check if player satisfied conditions to open the door
		if (Trigger.trig1 == 1 && tileNode != null && doorPart != null && triggerNode.units.Count == 1){
			OpenDoor(true); // open the door
		}
		if (Trigger.trig1 != 1 && triggerNode.units.Count == 1){
			StartCoroutine(Wait(10.0F));
		}

	}
	
	void OnGUI () {
		if (messageDelay == true){
			GUI.Box(new Rect(Screen.width/2,Screen.height/2, 250, 25), "Your missing the key! Go back and find it!");
		}
	}
	
	IEnumerator Wait(float waitTime) {
        messageDelay = true;
		yield return new WaitForSeconds(waitTime);
		messageDelay = false;
    }
	
	private void OpenDoor(bool open)
	{
		isOpen = open;
		doorPart.collider.enabled = !open;
		doorPart.renderer.enabled = !open;

		// update the tilenode links
		tileNode.SetLinkStateWithAll(open);

	}
}
