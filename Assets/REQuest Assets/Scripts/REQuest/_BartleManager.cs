using UnityEngine;
using System.Collections.Generic;

public class _BartleManager : MonoBehaviour {
	
	
private List<GameObject> bQuestions = new List<GameObject>();	//create List of all Bartle Question dialog prefabs thru inspector
public int questionsAsked = 0; 								//variable to track how many questions have been asked
public int currQuestion = 0;								//variable to track current question pulled from the the array
	
	
public int numberOfObjects;
private Vector3 nextPosition;
	
	
	// Use this for initialization
	void Start() 
	{
		foreach(GameObject g in Resources.LoadAll("/Assets/REQuest Assets/Prefabs/DialogPrefabs/BartleQuestions", typeof(GameObject)))
		{
   			Debug.Log("prefab found: " + g.name);
   			bQuestions.Add(g);
		}
	}



	
	
	
	public void QuestionPicker() 
	{
		currQuestion = (int)Random.Range(0, bQuestions.Count);
		print("Current Question(list order#): " + currQuestion);
		print("Total Questions Left in List: " + bQuestions.Count);
		Instantiate(bQuestions[currQuestion]);
		questionsAsked = (questionsAsked + 1);
		bQuestions.RemoveAt(currQuestion);	
		print("Questions Asked:" + questionsAsked);
	}
	

}




