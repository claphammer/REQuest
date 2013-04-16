using UnityEngine;
using System.Collections;

public class QuestLog : MonoBehaviour {
	
	private Rect winRect4 = new Rect(410, 70, 200, 200);
	
	public bool setWindow1 = false;  //Activate window(true) or deactivate window(false)

	public GUISkin mySkin;
	
	//Controls contents in window
	void DoWindow4(int windowID) {
		GUILayout.Label(string.Format("Max moves (Roll) this turn: "));
	}
	
	//GUI to define the window parameters
    void OnGUI() {
		GUI.skin = mySkin;
        if (setWindow1){
            GUILayout.Window(4, winRect4, DoWindow4, "Quest Log");
		}
    }

}