using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {
	public static int score=0;
	private Text myText;
	
	void Start(){
		myText=GetComponent<Text>();
		Reset();
	}
	// Use this for initialization
	public void Score(int points) {
		score += points;
		myText.text=score.ToString();
		
	}
	
	// Update is called once per frame
	public static void Reset () {
		score=0;
		//myText.text=score.ToString();
	}
	
	
}
