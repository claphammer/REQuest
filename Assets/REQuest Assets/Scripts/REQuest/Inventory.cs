// ====================================================================================================================
// Test Inventory Script
// RE-Quest  2013
// Nate Josway
// ====================================================================================================================


using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
		
	public Texture aTexture;  //First texture - Shadow
	public Texture bTexture;  //Second texture - Color
	private Rect winRect3 = new Rect(220, 10, 200, 200);
	
	public bool setWindow1 = false;  //Activate window(true) or deactivate window(false)

	public GUISkin mySkin;
	
	//Controls contents in window
	void DoWindow3(int windowID) {
		if (Trigger.trig1 == 1){
		GUI.DrawTexture(new Rect(10, 20, 100, 100), bTexture, ScaleMode.ScaleToFit, true, 1.0F);
		//Debug.Log(Trigger.trig1);
		}else{
		GUI.DrawTexture(new Rect(10, 20, 100, 100), aTexture, ScaleMode.ScaleToFit, true, 1.0F);
		}
	}
	
	//GUI to define the window parameters
    void OnGUI() {
		GUI.skin = mySkin;
        if (setWindow1)
            GUI.Window(2, winRect3, DoWindow3, "Inventory");            
    }

}