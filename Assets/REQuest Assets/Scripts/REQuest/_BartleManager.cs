using UnityEngine;
using System.Collections;

public class _BartleManager : MonoBehaviour {
	
	
public GameObject[] BQuestions = new GameObject[30];
	
	// Use this for initialization
	void Start () {
		int currQuestion = (int)Random.Range(0, BQuestions.Length);
		print(BQuestions.Length);
		Instantiate(BQuestions[currQuestion]);

	
	}
	
	// Update is called once per frame
	void Update () {
	//		float currQuestion = (float)Random.Range(0, BQuestions.Length);
	//	print(currQuestion);
	}
	
	
	
	
}
