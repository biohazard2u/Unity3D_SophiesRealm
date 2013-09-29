using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 7/11/2012</para>
/// <para>Last modified: 05/01/2013 18:50</para> 
/// <para>Author: Marcos Zalacain </para>
/// ExitHardwareButton:
/// 	Attached to object prefab, is responsible for double checking exiting application.
/// </summary>
public class ExitHardwareButton : MonoBehaviour {
	
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
	
	private GameObject guiLauncher;			// This is the found object daddy of SceneGUI scripts.
	
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
		
		// Hide GUI buttons. 
		guiLauncher = GameObject.Find("GUILauncher");
		foreach (Transform child in guiLauncher.transform) {
          	child.SendMessage("showButtons", false);
       	}		
	}
	
	void Update () {
		if (currentPosition > finalPosition) {
			currentPosition = currentPosition - 20.0f;
		}
		windowRect = new Rect(currentPosition, heightPosition, 16*unitW, 10.5f*unitH);
	}
	
    void OnGUI() {
			
		windowRect = GUI.Window(0, windowRect, DoMyWindow, TextStrings.windowExitOrBack);
	}
	
	void DoMyWindow(int windowID) {
        
		GUI.Label(new Rect(unitW, 2*unitH, 12*unitW, 1.5f*unitH), TextStrings.questionExitApp, questionStyle);
		
        if (GUI.Button(new Rect(2*unitW, 4*unitH, 12*unitW, 2*unitH), TextStrings.answerExitAppYes, answerStyle)){
			Application.Quit();
		}
		
		if (GUI.Button(new Rect(2*unitW, 7*unitH, 12*unitW, 2*unitH), TextStrings.answerExitAppNo, answerStyle)){
			Destroy(gameObject);
			GameObject.Find("GUI/HardwareButton").SendMessage("isCreatedToFalse", SendMessageOptions.DontRequireReceiver);
			foreach (Transform child in guiLauncher.transform) {
          		child.SendMessage("showButtons", true);
       		}
		}
	}
}
