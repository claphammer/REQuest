using UnityEngine;
using System.Collections;

public class Trigger_BeginBartle : MonoBehaviour 
{
	
private static int trigBT = 0; //When changed to 1 sends message to inventory to activate object
private bool messageDelay = false; //true activates message popup coroutine. false means trigger is idle
public Transform player;
public GameObject dialogBTestStart;
	
	void Update(){
		 rigidbody.WakeUp();
	}
	
//Detects collision and starts coroutine. Can only be activated once.
	void OnTriggerEnter(Collider Player)
	{
		if(trigBT == 0)
		{
			trigBT = 1;
			Debug.Log("Triggered "+trigBT);	
			StartCoroutine(Wait(1.0F));
			Instantiate (dialogBTestStart);
		}
		else
		{
			trigBT = 1;
		}
    }
	//coroutine that creates a GUI message for the length of time specifiec in Wait(10.0f).  Resets to false after activation.
	IEnumerator Wait(float waitTime)
	{
        messageDelay = true;
		yield return new WaitForSeconds(waitTime);
		messageDelay = false;
    }

}

