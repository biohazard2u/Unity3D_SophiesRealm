using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 19/10/2012</para>
/// <para>Last modified: 08/01/2013 18:50</para> 
/// <para>Author: Marcos Zalacain </para>
/// QuestionPrefab:   
///    -This script is to collect all questions and answers from each scroll and then show it. It goes as a QuestionGUI component.
/// </summary>
public class QuestionPrefab : MonoBehaviour {
	
	public GUIStyle questionStyle;
	public GUIStyle answerStyle;
	public Font questionFontSML;
	public Font questionFontSMH;
	public Font questionFontN7;
	public Font questionFontN10;
	public Font answerFontSML;
	public Font answerFontSMH;
	public Font answerFontN7;
	public Font answerFontN10;
	// Audio
	public AudioClip rightAnswerSoundEffect;
	public AudioClip wrongAnswerSoundEffect;
	
	// declare the reference variable:
	[HideInInspector]
  	public GameObject myCreator;				//Note: myCreator is the scroll that launched this QI.
	
	private float screenWidth;
	private float screenHeight;
	private float unitW, unitH;
	
	// Private copies of data from Question.cs attached to launcher obj.
	private int questionNumber;
	private Texture questionBackground;	
	private string theQuestionEngL1;
	private string theQuestionEngL2;
	private string theQuestionSpaL1;
	private string theQuestionSpaL2;
	private string[] theAnswersEng;
	private string[] theAnswersSpa;	
	private int rightAnswer;
	
	private string theQuestionL1;
	private string theQuestionL2;
	private string[] theAnswer;

	private float currentPosition;
	private float finalPosition;
	
	void Start () {
		
		// We set the background and styles acording to deviceType here.
		if (Globals.deviceType == Globals.SmartPhoneL) {
			questionStyle.font = questionFontSML;
			answerStyle.font = answerFontSML;
		}else if (Globals.deviceType == Globals.SmartPhoneH) {
			questionStyle.font = questionFontSMH;
			answerStyle.font = answerFontSMH;	
		}else if (Globals.deviceType == Globals.N7) {
			questionStyle.font = questionFontN7;
			answerStyle.font = answerFontN7;
		}else{
			questionStyle.font = questionFontN10;
			answerStyle.font = answerFontN10;
		}
		
		// Size related.
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		unitW = screenWidth/20;
		unitH = screenHeight/20;

		// Get QuestionLauncher.cs using the reference:
   		Question script = myCreator.GetComponent<Question>();
		// Then, read the strings.
		this.questionNumber = script.questionNumber;
		this.questionBackground = script.questionBackground;
   		this.theQuestionEngL1 = script.theQuestionEngL1;
		this.theQuestionEngL2 = script.theQuestionEngL2;
		this.theQuestionSpaL1 = script.theQuestionSpaL1;
		this.theQuestionSpaL2 = script.theQuestionSpaL2;
		this.theAnswersEng = script.theAnswersEng;
		this.theAnswersSpa = script.theAnswersSpa;
   		this.rightAnswer = script.rightAnswer;	
		
		if (Globals.choosenLanguage == Globals.English) {
			theQuestionL1 = this.theQuestionEngL1;
			theQuestionL2 = this.theQuestionEngL2;
			theAnswer = this.theAnswersEng;
		}else{
			theQuestionL1 = this.theQuestionSpaL1;
			theQuestionL2 = this.theQuestionSpaL2;
			theAnswer = this.theAnswersSpa;
		}
		
		guiTexture.texture = questionBackground;
		
		// Items positions.
		currentPosition = screenWidth;
		finalPosition = 4*unitW;
		
	}
	
	void Update () {
		if (currentPosition > finalPosition) {
			currentPosition = currentPosition - 0.5f*unitW;
		}
	}
	
    void OnGUI() {
			
		GUI.Label(new Rect(currentPosition, 4.5f*unitH, 12*unitW, 1*unitH), theQuestionL1, questionStyle);	
		GUI.Label(new Rect(currentPosition + 0.5f*unitW, 5.5f*unitH, 12*unitW, 1*unitH), theQuestionL2, questionStyle);
				
		if(GUI.Button(new Rect(currentPosition - 0.6f*unitW, 8*unitH, 14*unitW, 2*unitH), theAnswer[0], answerStyle)) {

			// Check answer.
			// If right answer, we'll send a Message to ScrollController to do the job.
			if (rightAnswer == 1) {
				playSoundEffect(rightAnswerSoundEffect);
				myCreator.SendMessage("rightAnswer", SendMessageOptions.DontRequireReceiver);
			}else{
				playSoundEffect(wrongAnswerSoundEffect);
				myCreator.SendMessage("wrongAnswer", SendMessageOptions.DontRequireReceiver);
			}
			// Anyhow, we'll go back to Game Mode.
			StartCoroutine(gameMode(0.8f));
		}
        
		if(GUI.Button(new Rect(currentPosition - 0.6f*unitW, 11*unitH, 14*unitW, 2*unitH),theAnswer[1], answerStyle)){
           
			// If right answer, we'll send a Message to ScrollController to do the job.
			if (rightAnswer == 2) {
				playSoundEffect(rightAnswerSoundEffect);
				myCreator.SendMessage("rightAnswer", SendMessageOptions.DontRequireReceiver);
			}else{
				playSoundEffect(wrongAnswerSoundEffect);
				myCreator.SendMessage("wrongAnswer", SendMessageOptions.DontRequireReceiver);
			}
			// Anyhow, we'll go back to Game Mode.
			StartCoroutine(gameMode(0.8f));
		}
        
		if(GUI.Button(new Rect(currentPosition - 0.6f*unitW, 14*unitH, 14*unitW, 2*unitH),theAnswer[2], answerStyle)){
			
			// If right answer, we'll send a Message to ScrollController to do the job.
			if (rightAnswer == 3) {
				playSoundEffect(rightAnswerSoundEffect);
				myCreator.SendMessage("rightAnswer", SendMessageOptions.DontRequireReceiver);
			}else{
				playSoundEffect(wrongAnswerSoundEffect);
				myCreator.SendMessage("wrongAnswer", SendMessageOptions.DontRequireReceiver);
			}
			// Anyhow, we'll go back to Game Mode.
			StartCoroutine(gameMode(0.8f));
		}        
    }
	
	 IEnumerator gameMode(float waitTime) {
        yield return new WaitForSeconds(waitTime);
		Destroy(gameObject);
		GameObject.Find("GUI/Controls").SendMessage("turnChildrenOn", SendMessageOptions.DontRequireReceiver);
		GameObject.Find("GUI/HUD").SendMessage("turnChildrenOn", SendMessageOptions.DontRequireReceiver);
    }
	
	// This is to play sound effect.
	void playSoundEffect(AudioClip aClip){
		if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {
			audio.PlayOneShot(aClip);
		}		
	}
}
