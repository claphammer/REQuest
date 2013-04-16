using UnityEngine;
using System.Collections;

public class Toolbar : MonoBehaviour {
	
	public int toolbarInt = 0;
    public string[] toolbarStrings = new string[] {"Toolbar1", "Toolbar2", "Toolbar3"};
	
    void OnGUI() {
		GUI.Box(new Rect(240,20,270,40),"");
        toolbarInt = GUI.Toolbar(new Rect(250, 25, 250, 30), toolbarInt, toolbarStrings);
		if(toolbarInt == 0){
			Debug.Log("0");
		}
		if(toolbarInt == 1){
			Debug.Log("1");
		}
		if(toolbarInt == 2){
			Debug.Log("2");
		}
    }

}
