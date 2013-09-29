using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 8/11/2012</para>
/// <para>Last modified: 05/01/2013 18:55</para> 
/// <para>Author: Marcos Zalacain </para>
/// ReStartLevel:
/// 	It's attached to ReStarLevel prefab and it's lauched when player runs out of lifes.
/// 	It creates the Warning message window.
/// </summary>
public class ReStartLevel : MonoBehaviour {

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
			
		windowRect = GUI.Window(0, windowRect, DoMyWindow, TextStrings.windowRSL);
	}
	
	void DoMyWindow(int windowID) {
        
		GUI.Label(new Rect(unitW, 2*unitH, 12*unitW, 1.5f*unitH), TextStrings.questionRSL, questionStyle);
		
        if (GUI.Button(new Rect(2*unitW, 4*unitH, 12*unitW, 2*unitH), TextStrings.answerRSLYes, answerStyle)){
			Application.LoadLevel(Application.loadedLevel);
			SophieController.canControl = true;
			//SophieControllerKeyboard.canControl = true;
		}
		
		if (GUI.Button(new Rect(2*unitW, 7*unitH, 12*unitW, 2*unitH), TextStrings.answerRSLNo, answerStyle)){
			Application.LoadLevel("LevelsScene");
			SophieController.canControl = true;
			//SophieControllerKeyboard.canControl = true;
		}
	}
}
