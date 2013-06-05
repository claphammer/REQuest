#pragma strict

function OnGUI () {
	
	GUI.Box(Rect(0,0,768,1024),"");
	
	GUI.Label(Rect(340,20,100, 20),"REQuest Demo Levels");
	
	if(GUI.Button(Rect(255,50,250,100),"Level 1"))
		Application.LoadLevel("REQuestDemo02");
		
	if(GUI.Button(Rect(255,200,250,100),"Level 2"))
		Application.LoadLevel("REQuestDemoNJ");

}