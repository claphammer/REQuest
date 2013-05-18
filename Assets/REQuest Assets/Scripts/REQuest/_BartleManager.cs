using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class _BartleManager : MonoBehaviour
{
    public List<GameObject> bQuestions = new List<GameObject>();  //original List:  populate in inspector
    private List<GameObject> questions = new List<GameObject>();  //transfer List for runtime
    public bool questionAsked = false;
	private bool complete = false;
	private int currQuestion = 0;
	
		private float SocialTotal;
		private float AchieverTotal;
		private float KillerTotal;
		private float ExplorerTotal;
	
	
   
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
			print("Question has already been asked this turn");
			return;
		}
		else
		{
    		GameObject newQuestion = RandomPick();

        	if (newQuestion == null) 
			{
        		Debug.Log("Out of Questions");
         		BartleQuotient();
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
		//print("Total Questions Left in ORIGINAL List: " + bQuestions.Count); //Print how many questions remain in the ORIGINAL LIST
		//print("Total Questions Left in Copied List: " + questions.Count); //Print how many questions remain in the copied LIST

        return go;
    }
	
	public void BartleQuotient()
	{
		float scoreAch = (float)DialogUI.GetTokenAsFloat("Achiever");  //ignore MONO COMPILE ERROR
		float scoreExp = (float)DialogUI.GetTokenAsFloat("Explorer");  //ignore MONO COMPILE ERROR
		float scoreSoc = (float)DialogUI.GetTokenAsFloat("Socializer");  //ignore MONO COMPILE ERROR
		float scoreKil = (float)DialogUI.GetTokenAsFloat("Killer");  //ignore MONO COMPILE ERROR
		
		SocialTotal = ((scoreSoc/30)*2);
		AchieverTotal = ((scoreAch/30)*2);
		KillerTotal = ((scoreKil/30)*2);
		ExplorerTotal = ((scoreExp/30)*2);
		complete = true;
	}
		




	public void OnGUI()
	{
	  	float w = 400;
		float h = 300;
  		Rect rect = new  Rect((Screen.width-w)/2, (Screen.height-h)/2, w, h);
		
		if (complete == true)
		{
		  GUI.Label(rect, "Your Results are... Social: " + (SocialTotal).ToString() + ", Achiever: " + (AchieverTotal).ToString() + ", Killer: " + (KillerTotal).ToString() + ", Explorer: " + (ExplorerTotal).ToString() + " ... **Totals are out of 200%.  No single category will be over 100%**");
		}
	}

}