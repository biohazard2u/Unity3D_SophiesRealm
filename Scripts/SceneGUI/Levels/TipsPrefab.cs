using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 20/01/2013</para>
/// <para>Last modified: 05/01/2013 18:55</para> 
/// <para>Author: Marcos Zalacain </para>
/// TipsPrefab:
/// 	It's attached to TipsPrefab prefab and it's lauched at the begining of the first level.
/// 	It creates the Tips message window.
/// </summary>
public class TipsPrefab : MonoBehaviour {
	
	public GameObject mainCameraObject;		// This reference is to launch the camera animation at the end of the stage.
	
	public Texture tip1Image;
	public Texture tip2Image;
	public Texture tip3Image;
	
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
		
	void Awake () {
		
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
		heightPosition = 4*unitH ;
		finalPosition = 2.5f*unitW;
		
		windowRect = new Rect(currentPosition, heightPosition, 5*unitW, 5*unitH);
		
		// Hide any GUI, Controls and HUD gadgets, depending on which level we are on. 
		// Watch OUT: we dont need this since we dont turn them on @ TurnControlChildrenOnOFF.cs for level 1.
		//GameObject.Find("GUI/Controls").SendMessage("turnChildrenOff", SendMessageOptions.DontRequireReceiver);
		//GameObject.Find("GUI/HUD").SendMessage("turnChildrenOff", SendMessageOptions.DontRequireReceiver);
		
		// Call CameraScrolling.cs to simulate animation.
		CameraScrolling scriptComponent =  mainCameraObject.GetComponent<CameraScrolling>();
		scriptComponent.StartCoroutine("animationTipsPrefab", 10.0f);
	}
	
	void Start(){
		SophieController.canControl = false;
		//SophieControllerKeyboard.canControl = false;
	}
	
	void Update () {
		if (currentPosition > finalPosition) {
			currentPosition = currentPosition - 20.0f;
		}
		windowRect = new Rect(currentPosition, heightPosition, 16*unitW, 14*unitH);
	}
	
	void FixedUpdate () {
		
		// Android Back button is the escape key in Unity. 
		// We check this so device does not get blocked when esc is pressed while TipsPrefab is on.
		if (Input.GetKeyUp(KeyCode.Escape)) {
			goToGamePlayMode();
		}
	}
	
    void OnGUI() {
			
		windowRect = GUI.Window(0, windowRect, DoMyWindow, TextStrings.windowTips);
	}
	
	void DoMyWindow(int windowID) {
        
		GUI.Label(new Rect(unitW, 2*unitH, 12*unitW, 1.5f*unitH), TextStrings.questionTips, questionStyle);
		
       	GUI.Label(new Rect(2*unitW, 4*unitH, 12*unitW, 1.5f*unitH), TextStrings.answerTips1, questionStyle);
		GUI.Label(new Rect(11*unitW, 3.5f*unitH, 2*unitW, 1.8f*unitH), tip1Image);
		GUI.Label(new Rect(2*unitW, 6*unitH, 12*unitW, 1.5f*unitH), TextStrings.answerTips2, questionStyle);
		GUI.Label(new Rect(12*unitW, 5.5f*unitH, 2*unitW, 1.8f*unitH), tip2Image);
		GUI.Label(new Rect(2*unitW, 8*unitH, 12*unitW, 1.5f*unitH), TextStrings.answerTips3, questionStyle);
		GUI.Label(new Rect(12*unitW, 7.8f*unitH, 2*unitW, 1.8f*unitH), tip3Image);
		
		if (GUI.Button(new Rect(7*unitW, 11*unitH, 7*unitW, 2*unitH), TextStrings.ContinueBttTips, answerStyle)){
			
			goToGamePlayMode();
		}
	}
	
	void goToGamePlayMode(){
		
		// RETURN TO DEFAULT CAMERA POSITION.
		CameraScrolling scriptComponent =  mainCameraObject.GetComponent<CameraScrolling>();
		scriptComponent.resetCametaValuesToDefault();
		
		SophieController.canControl = true;
		//SophieControllerKeyboard.canControl = true;
		Destroy(gameObject);
		GameObject.Find("GUI/HardwareButton").SendMessage("isCreatedToFalse", SendMessageOptions.DontRequireReceiver);
		GameObject.Find("GUI/Controls").SendMessage("turnChildrenOn", SendMessageOptions.DontRequireReceiver);
		GameObject.Find("GUI/HUD").SendMessage("turnChildrenOn", SendMessageOptions.DontRequireReceiver);
	}
}
