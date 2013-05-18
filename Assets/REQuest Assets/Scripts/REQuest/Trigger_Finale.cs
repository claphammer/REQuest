using UnityEngine;
using System.Collections;

public class Trigger_Finale : MonoBehaviour {
	
public static int trig2 = 0; //When changed to 1 sends message to inventory to activate object
public bool messageDelay = true; //true activates message popup coroutine. false means trigger is idle
public ParticleSystem particles;

	
	void Update(){
		 rigidbody.WakeUp();
	}
		
	//Detects collision and starts coroutine. Can only be activated once.
	void OnTriggerEnter(Collider Player) {
		if(trig2 == 0)
		{
		trig2 = 1;
		Debug.Log("Trigger Finale "+trig2);
			
		audio.Play();	
		//particleEmitter.enabled = true;
			
			
		StartCoroutine(Wait(10.0F));
			
		//Time.timeScale = 0;
		}
		else
		{
		trig2 = 1;
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
		if (messageDelay == true)
		{
			float w = 400;
			float h = 300;
			Rect rect = new Rect((Screen.width-w)/2, (Screen.height-h)/2 - 50, w, h);
			GUI.Label(rect, "You Reached the Target! - A mini Game will soon live here.  ***  You can quit now or walk around and Finish the Bartle Test first");
		}
	}
}

