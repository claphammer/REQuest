using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class _BartleManager : MonoBehaviour
{
    public List<GameObject> bQuestions = new List<GameObject>();  //original List:  populate in inspector
    private List<GameObject> questions = new List<GameObject>();  //transfer List for runtime
    public bool questionAsked = false;
	private int currQuestion = 0;
   
    public void ResetQuestions()
    {
       	questionAsked = false;
        questions.Clear();
        questions.AddRange(bQuestions);
    }
 
	public void QuestionPicker()
    {
		if (questionAsked == true)
		{
			Debug.Log("Question has been asked this turn");
			return;
		}
		else
		{
    		GameObject newQuestion = RandomPick();

        	if (newQuestion == null) 
			{
        		Debug.Log("Out of Questions");
         		return;
       		}
		}
       	questionAsked = true;

    }
	
    GameObject RandomPick()
    {
        if(questions.Count == 0)
        {
        	return null;
        }
 
        currQuestion = Random.Range(0, questions.Count);
        GameObject go = GameObject.Instantiate(questions[currQuestion]) as GameObject;
        questions.RemoveAt(currQuestion);
		print("Current Question(list order#): " + currQuestion); //Print our random pick
		print("Total Questions Left in ORIGINAL List: " + bQuestions.Count); //Print how many questions remain in the ORIGINAL LIST
		print("Total Questions Left in Copied List: " + questions.Count); //Print how many questions remain in the copied LIST

        return go;
    }
}