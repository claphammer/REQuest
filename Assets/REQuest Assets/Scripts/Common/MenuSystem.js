#pragma strict

function OnGUI () {
	
	GUI.Box(Rect(0,0,960,600),"");
	
	GUI.Label(Rect(Screen.width - 530,Screen.height - 550,100, 20),"REQuest Demo Levels");
	
	if(GUI.Button(Rect(Screen.width - 610,Screen.height - 500,250,100),"Level 1"))
		Application.LoadLevel("REQuestDemo02");
		
	if(GUI.Button(Rect(Screen.width - 610,Screen.height - 350,250,100),"Level 2"))
		Application.LoadLevel("REQuestDemoNJ");

}