using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 18/01/2013</para>
/// <para>Last modified: 05/01/2013 18:50</para> 
/// <para>Author: Marcos Zalacain </para>
/// EndOfStage:
/// 	It's attached to EndOfStage prefab and it's lauched when player reaches the end of stage, usually the wizard.
/// 	It creates the Next Level message window.
/// </summary>
public class EndOfStage : MonoBehaviour {

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
	// Audio variable goes here.
	
	private Rect windowRect;
	private float screenWidth;
	private float screenHeight;
	private float unitW, unitH;

	private float currentPosition;
	private float heightPosition;
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
				
		// Items positions.
		currentPosition = screenWidth + 10;
		heightPosition = 6*unitH ;
		finalPosition = 2.5f*unitW;
		
		windowRect = new Rect(currentPosition, heightPosition, 5*unitW, 5*unitH);
		
		// Hide any GUI, Controls and HUD gadgets, depending on which level we are on. 
		GameObject.Find("GUI/Controls").SendMessage("turnChildrenOff", SendMessageOptions.DontRequireReceiver);
		GameObject.Find("GUI/HUD").SendMessage("turnChildrenOff", SendMessageOptions.DontRequireReceiver);
	}
	
	void Update () {
		if (currentPosition > finalPosition) {
			currentPosition = currentPosition - 20.0f;
		}
		windowRect = new Rect(currentPosition, heightPosition, 16*unitW, 10.5f*unitH);
	}
	
    void OnGUI() {
			
		windowRect = GUI.Window(0, windowRect, DoMyWindow, TextStrings.windowEOS);
	}
	
	void DoMyWindow(int windowID) {	
		
		// IF we reach last Build level...
		if (Application.loadedLevelName == "Level81") {
			
			GUI.Label(new Rect(4*unitW, 2*unitH, 12*unitW, 1.5f*unitH), TextStrings.moreLevels1, questionStyle);
			GUI.Label(new Rect(5*unitW, 4*unitH, 12*unitW, 1.5f*unitH), TextStrings.moreLevels2, questionStyle);
			GUI.Label(new Rect(6*unitW, 6*unitH, 12*unitW, 1.5f*unitH), TextStrings.moreLevels3, questionStyle);
			
			if (GUI.Button(new Rect(7*unitW, 8*unitH, 4*unitW, 2*unitH), TextStrings.moreLevels4, answerStyle)){
				Application.LoadLevel("TheEnd");
				SophieController.canControl = true;
			}
			
			/*if (GUI.Button(new Rect(5.5f*unitW, 8*unitH, 9.5f*unitW, 2*unitH), TextStrings.giveUsaPush, answerStyle)){
				Application.OpenURL("https://play.google.com/store/apps/details?id=com.H2OGames.SophiesRealm&feature=search_result&write_review=true#?t=W251bGwsMSwyLDEsImNvbS5IMk9HYW1lcy5Tb3BoaWVzUmVhbG0iXQ..");
			}*/
		// Otherwise...	
		}else{
        
			GUI.Label(new Rect(unitW, 2*unitH, 12*unitW, 1.5f*unitH), TextStrings.questionEOS, questionStyle);
			
	        if (GUI.Button(new Rect(2*unitW, 4*unitH, 12*unitW, 2*unitH), TextStrings.answerEOSContinue, answerStyle)){
				
				// Launch Next Level 
				string currLevelStr = Application.loadedLevelName.Remove(0,5);
				int currLevelInt = System.Convert.ToInt32(currLevelStr);
				Globals.levelToLaunch = currLevelInt + 1;
				// Does it have a StoryScene?
				if (currLevelInt % 9 == 0) {
					Application.LoadLevel("StoryScene");
				}else{
					Application.LoadLevel("Level" + Globals.levelToLaunch);
				}
				SophieController.canControl = true;
				//SophieControllerKeyboard.canControl = true;
				
				// Do i need to:	// RETURN TO DEFAULT CAMERA POSITION.
									//CameraScrolling scriptComponent =  mainCameraObject.GetComponent<CameraScrolling>();
									//scriptComponent.resetCametaValuesToDefault();
			}
			
			if (GUI.Button(new Rect(2*unitW, 7*unitH, 12*unitW, 2*unitH), TextStrings.answerEOSExit, answerStyle)){
				Application.LoadLevel("LevelsScene");
				SophieController.canControl = true;
				//SophieControllerKeyboard.canControl = true;
			}
		}
	}
}
