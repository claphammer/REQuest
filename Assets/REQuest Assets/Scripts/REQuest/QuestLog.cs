using UnityEngine;
using System.Collections;

public class QuestLog : MonoBehaviour {
	
	private Rect winRect4 = new Rect(410, 70, 200, 200);
	
	public bool setWindow1 = false;  //Activate window(true) or deactivate window(false)

	public GUISkin mySkin;
	
	//Controls contents in window
	void DoWindow4(int windowID) {
		/*if(Trigger.trig1 == 1){
			GUILayout.Label(string.Format("INCOMPLETE: Need to collect the dice and go to the village gate."));
		}*/
		if(Trigger.trig1 == 1 && VillageDoor.isOpen == true){
			GUILayout.Label(string.Format("COMPLETED: Need to collect the dice and go to the village gate."));
		}else{
			GUILayout.Label(string.Format("INCOMPLETE: Need to collect the dice and go to the village gate."));	
		}
	}
	
	//GUI to define the window parameters
    void OnGUI() {
		GUI.skin = mySkin;
        if (setWindow1){
            GUILayout.Window(4, winRect4, DoWindow4, "Quest Log");
		}
    }

}