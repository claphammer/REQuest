using UnityEngine;
using System.Collections;

public class Reporter : MonoBehaviour {
	
public Rect rect = new Rect(100, 100, 100, 100);
public float score;
	
	// Use this for initialization
	void Start () 
	{
	//	score = (float)DialogUI.GetTokenAsFloat("Achiever");
	}
	
	// Update is called once per frame
	void Update () 
	{
    //    score = (float)DialogUI.GetTokenAsFloat("Achiever");
	}
	
	void OnGUI ()
    {
        GUI.Label(rect, (score).ToString());
    }
	
}
