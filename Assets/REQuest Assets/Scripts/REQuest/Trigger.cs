using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {
	
public static int trig1 = 0; //When changed to 1 sends message to inventory to activate object
public bool messageDelay = false; //true activates message popup coroutine. false means trigger is idle

	
	void Update(){
		 rigidbody.WakeUp();
	}
		
	//Detects collision and starts coroutine. Can only be activated once.
	void OnTriggerEnter(Collider Player) {
		if(trig1 == 0){
		trig1 = 1;
		Debug.Log("Triggered "+trig1);
		StartCoroutine(Wait(10.0F));
		}else{
		trig1 = 1;
		}
    }
	//coroutine that creates a GUI message for the length of time specifiec in Wait(10.0f).  Resets to false after activation.
	IEnumerator Wait(float waitTime) {
        messageDelay = true;
		yield return new WaitForSeconds(waitTime);
		messageDelay = false;
    }
	//GUI message controls.  Alter GUI information from on GUI - valid until GUI styles are implemented
	void OnGUI () {
		if (messageDelay == true){
			GUI.Box(new Rect(Screen.width/2,Screen.height/2, 250, 25), "New Item Acquired");
		//GUI.Box(new Rect(Screen.width/2,Screen.height/2,250,100), "New Item Acquired", style);
		}
	}
}