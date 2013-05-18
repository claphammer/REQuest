///////////////////////////////////////////////////////////////////////////////////////////////
//
//		ProximityExample.js
//
//		Simple script that shows how to trigger a dialog using scripting (and object proximity)
//		(C) 2012 Melli Georgiou
//
///////////////////////////////////////////////////////////////////////////////////////////////

// NOTE: Remember that you need to have the DialogUI object in the scene, as well as the 2 variable's below setup in the Unity editor.

#pragma strict

var theDialog : GameObject;    			// In the unity Editor, you would then drag the Dialog prefab you created into this variable.
  var w = 400;
  var h = 300;
  private var rect = Rect((Screen.width-w)/2, (Screen.height-h)/2, w, h);


///////////////////////////////////////////////////////////////////////////////////////////////
//	ON GUI
///////////////////////////////////////////////////////////////////////////////////////////////

function OnGUI(){

var SocialTotal : float = DialogUI.GetTokenAsFloat ("Socializer");
var AchieverTotal : float = DialogUI.GetTokenAsFloat ("Achiever");
var KillerTotal : float = DialogUI.GetTokenAsFloat ("Killer");
var ExplorerTotal : float = DialogUI.GetTokenAsFloat ("Explorer");
//var WhoAmI : String = "Bartle Quotient"

SocialTotal = ((SocialTotal/30)*2);
AchieverTotal = ((AchieverTotal/30)*2);
KillerTotal = ((KillerTotal/30)*2);
ExplorerTotal = ((ExplorerTotal/30)*2);

  GUI.Label(rect, "Your Results are... Social: " + (SocialTotal).ToString() + ", Achiever: " + (AchieverTotal).ToString() + ", Killer: " + (KillerTotal).ToString() + ", Explorer: " + (ExplorerTotal).ToString() + " ... **Totals are out of 200%.  No single category will be over 100%**");
//  if (SocialTotal > AchieverTotal & KillerTotal & ExplorerTotal) 
        

//if(Vector3.Distance( player.position, transform.position ) > 2)
//	{
//	GUI.Label(theRect1, Vector3.Distance( player.position, transform.position ).ToString() + " To The Target.");
//	GUI.Label(theRect2, WhoAmI);
//	GUI.Label(theRect3, "Hearts/Social: " + SocialTotal);
//	GUI.Label(theRect4, "Diamonds/Achiever: " + AchieverTotal);
//	GUI.Label(theRect5, "Clubs/Killer: " + KillerTotal);
//	GUI.Label(theRect6, "Spades/Explorer: " + ExplorerTotal);
//	}
//else
//	{
//	GUI.Label(theRect1, Vector3.Distance( player.position, transform.position ).ToString());
//	GUI.Label(theRect2, WhoAmI + "...Is Within Range!");
//	GUI.Label(theRect3, "Hearts/Social: " + SocialTotal);
//	GUI.Label(theRect4, "Diamonds/Achiever: " + AchieverTotal);
//	GUI.Label(theRect5, "Clubs/Killer: " + KillerTotal);
//	GUI.Label(theRect6, "Spades/Explorer: " + ExplorerTotal);
//	}

}


