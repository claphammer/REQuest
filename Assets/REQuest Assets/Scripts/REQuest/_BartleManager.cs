using UnityEngine;
using System.Collections.Generic;

public class _BartleManager : MonoBehaviour {
	
public List<GameObject> bQuestions = new List<GameObject>();	//create List of all Bartle Question dialog prefabs thru inspector
private int QuestionsAsked = 0; 								//variable to track how many questions have been asked
private int currQuestion;										//variable to track current question pulled from the the array
	
	void QuestionPicker () 
	{
		currQuestion = (int)Random.Range(0, bQuestions.Count);
		print(bQuestions.Count);
		Instantiate(bQuestions[currQuestion]);
		QuestionsAsked = (QuestionsAsked + 1);
		bQuestions.RemoveAt(currQuestion);

	
	}
	

	void Update ()
	{
		// add if statement to check if a dialog is active...
		if (DialogUI.ended && Input.GetKeyDown(KeyCode.Space)) //ignore Mono error about DialogUI not existing
        {
			QuestionPicker();
        }

	}
	
	
	

 
/*	void GetCard()
{
    int idx = Random.Range(0, deck.Count);
    Card theCard = deck[idx];
    deck.RemoveAt(idx);
    return theCard;
}
*/
	
}
