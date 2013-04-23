using UnityEngine;
using System.Collections;

public class MovementTrigger : MonoBehaviour {

	
public static int trig2 = 0; //When changed to 1 sends message to inventory to activate object

//public AudioClip clip;

	
	void Update(){
		 rigidbody.WakeUp();
	}
		
	//Detects collision and starts coroutine. Can only be activated once.
	void OnTriggerEnter(Collider Player) {
		if(trig2 == 0){
		trig2 = 1;
		Debug.Log("Movement Triggered "+trig2);
		}
	}
}
