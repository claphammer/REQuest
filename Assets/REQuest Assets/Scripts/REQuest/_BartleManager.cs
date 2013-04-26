using UnityEngine;
using System.Collections.Generic;

public class _BartleManager : MonoBehaviour {
	
public List<GameObject> bQuestions = new List<GameObject>();	//create List of all Bartle Question dialog prefabs thru inspector
private int questionsAsked = 0; 								//variable to track how many questions have been asked
private int currQuestion;										//variable to track current question pulled from the the array
private int turn;
			
	public void QuestionPicker() 
	{
		currQuestion = (int)Random.Range(0, bQuestions.Count);
		print("Total Questions Left in List: " + bQuestions.Count);
		Instantiate(bQuestions[currQuestion]);
		questionsAsked = (questionsAsked + 1);
		bQuestions.RemoveAt(currQuestion);	
		print ("Questions Asked:" + questionsAsked);
	}
	

/*	void Update ()
	{
		turn = ((int)GameController.turnNumber);
		print("Turn Number is: " + turn);
		
		if (turn != 0 && (turn & 1) == 0)
		{
			print(turn + " is EVEN...continue to random Bartle Test Question");
			QuestionPicker();
		}
		else
		{
			print(turn + " is ODD.  Stop checker.");
		}
	}
*/
}
	
	

 
/*	void GetCard()
{
    int idx = Random.Range(0, deck.Count);
    Card theCard = deck[idx];
    deck.RemoveAt(idx);
    return theCard;
}
*/

