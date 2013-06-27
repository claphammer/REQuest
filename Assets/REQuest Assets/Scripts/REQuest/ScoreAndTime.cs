using UnityEngine;
using System.Collections;

public class ScoreAndTime : MonoBehaviour {

	public int currentTime = 90;
	public int totalScore = 0;
	
	
	void Start () {
		InvokeRepeating("Countdown",1,1);
	}
	
	void Countdown () {
		if(--currentTime == 0)
		{
			Debug.Log("Time is up");
			CancelInvoke("Countdown");
		}
	}
	
	void Score (){
		if(currentTime >= 90){
			totalScore = currentTime*4;
		}
		if(currentTime >= 60){
			totalScore = currentTime*3;
		}
		if(currentTime >= 30){
			totalScore = totalScore+(currentTime*2);
		}else{
			totalScore = totalScore+currentTime;
		}
	}
	
	void Update () {
		//Debug.Log(currentTime);
		Debug.Log(totalScore);
		Score();
	}
}
