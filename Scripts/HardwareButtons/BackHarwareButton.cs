using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 7/11/2012</para>
/// <para>Last modified: 05/01/2013 17:40</para> 
/// <para>Author: Marcos Zalacain </para>
/// BackHarwareButton:
/// 	Attached to object prefab, is responsible for double checking back button.
/// </summary>
public class BackHarwareButton : MonoBehaviour {
	
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
		
		// Hide any GUI, Controls and HUD gadgets, depending on which level we are on. 
		if (Application.loadedLevel >= 5 ) {
			GameObject.Find("GUI/Controls").SendMessage("turnChildrenOff", SendMessageOptions.DontRequireReceiver);
			GameObject.Find("GUI/HUD").SendMessage("turnChildrenOff", SendMessageOptions.DontRequireReceiver);
		}else{
			guiLauncher = GameObject.Find("GUILauncher");
			foreach (Transform child in guiLauncher.transform) {
          		child.SendMessage("showButtons", false);
       		}
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
        
		GUI.Label(new Rect(unitW, 2*unitH, 12*unitW, 1.5f*unitH), TextStrings.questionBackScene, questionStyle);
		
        if (GUI.Button(new Rect(2*unitW, 4*unitH, 12*unitW, 2*unitH), TextStrings.answerBackSceneYes, answerStyle)){
			Application.LoadLevel("WelcomeScene");
		}
		
		if (GUI.Button(new Rect(2*unitW, 7*unitH, 12*unitW, 2*unitH), TextStrings.answerBackSceneNo, answerStyle)){
			
			// We'll go back to Game Mode if we are on any LevelScene.
			if (Application.loadedLevel >= 5 ) {
				StartCoroutine(gameMode(0.8f));
			}else{
				Destroy(gameObject);
				GameObject.Find("GUI/HardwareButton").SendMessage("isCreatedToFalse", SendMessageOptions.DontRequireReceiver);
				foreach (Transform child in guiLauncher.transform) {
          			child.SendMessage("showButtons", true);
       			}
			}
		}
	}
	
	 IEnumerator gameMode(float waitTime) {
        yield return new WaitForSeconds(waitTime);
		Destroy(gameObject);
		GameObject.Find("GUI/HardwareButton").SendMessage("isCreatedToFalse", SendMessageOptions.DontRequireReceiver);
		GameObject.Find("GUI/Controls").SendMessage("turnChildrenOn", SendMessageOptions.DontRequireReceiver);
		GameObject.Find("GUI/HUD").SendMessage("turnChildrenOn", SendMessageOptions.DontRequireReceiver);
    }
}
