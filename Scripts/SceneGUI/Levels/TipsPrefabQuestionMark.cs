using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 14/03/2013</para>
/// <para>Last modified: 14/03/2013 20:40</para> 
/// <para>Author: Marcos Zalacain </para>
/// TipsPrefabQuestionMark:    
///    -This prefab is launched from QyestionMark gameObject.
/// </summary>
public class TipsPrefabQuestionMark : MonoBehaviour {
	
	[HideInInspector]
  	public GameObject myMaker;					// This reference the questionMark sign that has instanciated this prefab.
	[HideInInspector]
  	public int myMakersTipNumber;				// This is for the tipNumber of the sign that created this prefab.
	
	public Texture sign;
	public Texture sign2;
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
	
	private GameObject mainCameraObject;		// This reference our main camera.
	private CameraScrolling scriptComponent;	// This is the script in our main camera object.
	
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
		finalPosition = 2*unitW;
		
		windowRect = new Rect(currentPosition, heightPosition, 18*unitW, 14*unitH);
		
		// Hide any GUI, Controls and HUD gadgets, depending on which level we are on. 
		GameObject.Find("GUI/Controls").SendMessage("turnChildrenOff", SendMessageOptions.DontRequireReceiver);
		GameObject.Find("GUI/HUD").SendMessage("turnChildrenOff", SendMessageOptions.DontRequireReceiver);
		
		// Call CameraScrolling.cs to simulate animation.
		mainCameraObject = GameObject.FindGameObjectWithTag("MainCamera") as GameObject;
		scriptComponent =  mainCameraObject.GetComponent<CameraScrolling>();
		scriptComponent.zoomOutNow();
	}
	
	void Start(){
		SophieController.canControl = false;
		//SophieControllerKeyboard.canControl = false;		
	}
	
	void Update () {
		if (currentPosition > finalPosition) {
			currentPosition = currentPosition - unitW;
		}
		windowRect = new Rect(currentPosition, heightPosition, 17*unitW, 14*unitH);
	}
	
	void FixedUpdate () {
		
		// If we press Android Back button the device does not get blocked, we destroy the prefab instead.
		if (Input.GetKeyUp(KeyCode.Escape)) {
			Destroy(gameObject);
			goToGamePlayMode();
		}
	}
	
    void OnGUI() {
			
		windowRect = GUI.Window(0, windowRect, DoMyWindow, TextStrings.windowTips);
	}
	
	void DoMyWindow(int windowID) {
        
		if (myMakersTipNumber == 3) {
			GUI.Label(new Rect(unitW, 2*unitH, 4*unitW, 10*unitH), sign2);
		}else{
			GUI.Label(new Rect(unitW, 2*unitH, 4*unitW, 10*unitH), sign);
		}
		GUI.Label(new Rect(4*unitW, 3*unitH, 12*unitW, 1.5f*unitH), setTextStrings(1), questionStyle);		
       	GUI.Label(new Rect(4*unitW, 5*unitH, 12*unitW, 1.5f*unitH), setTextStrings(2), questionStyle);
		GUI.Label(new Rect(4*unitW, 7*unitH, 12*unitW, 1.5f*unitH), setTextStrings(3), questionStyle);
		
		if (GUI.Button(new Rect(7*unitW, 11*unitH, 7*unitW, 2*unitH), TextStrings.ContinueBttTips, answerStyle)){			
			//Destroy(gameObject);		// We destroy this prefab. - We're destroying this @ goToGamePlayMode().
			goToGamePlayMode();			// We go back to playMode.
			Destroy(myMaker);			// We destroy the sign that launched this prefab.
			//myMaker.SendMessage("isCreatedToFalse", SendMessageOptions.DontRequireReceiver);	// Method comented on TutorialTips.cs
		}
	}
	
	void goToGamePlayMode(){
		
		// RETURN TO DEFAULT CAMERA POSITION.
		CameraScrolling scriptComponent =  mainCameraObject.GetComponent<CameraScrolling>();
		scriptComponent.resetCametaValuesToDefault();
		
		SophieController.canControl = true;
		//SophieControllerKeyboard.canControl = true;
		Destroy(gameObject);
		GameObject.Find("GUI/Controls").SendMessage("turnChildrenOn", SendMessageOptions.DontRequireReceiver);
		GameObject.Find("GUI/HUD").SendMessage("turnChildrenOn", SendMessageOptions.DontRequireReceiver);
	}
	
	// Setting the strings for each language.
	string setTextStrings(int txtNumber){
		
		string st = "";
		
		if(Globals.choosenLanguage == Globals.English){
			
			if (myMakersTipNumber == 1) {
				switch (txtNumber) {
				case 1:
                    st = "Hmmm, this sign is to inform of a moving platform";
					break;
				case 2:
                    st = "It must be that platform above!";
					break;
				case 3:
                    st = "I bet I can get that platform down some how...";
					break;
				}
			}
			if (myMakersTipNumber == 2) {
				switch (txtNumber) {
				case 1:
                    st = "Hmmm, this sign is to inform of a moving platform";
					break;
				case 2:
                    st = "It must be this platform on the right!";
					break;
				case 3:
                    st = "I bet I can get it to go higher...";
					break;
				}
			}
			if (myMakersTipNumber == 3) {
				switch (txtNumber) {
				case 1:
                    st = "Gee whiz! This lever it’s different.";
					break;
				case 2:
                    st = "Blimey! It’s got a timer on.";
					break;
				case 3:
                    st = "I better dash for the platform, before the time runs out.";
					break;
				}
			}
		}else{						
			if (myMakersTipNumber == 1) {
				switch (txtNumber) {
					case 1:
	                    st = "Uhmm, esa señal me informa de una plataforma móvil";
						break;
					case 2:
	                    st = "Debe de ser esa plataforma de ahí arriba";
						break;
					case 3:
	                    st = "Apuesto a que la puedo bajar de alguna forma...";
						break;
				}
			}
			if (myMakersTipNumber == 2) {
				switch (txtNumber) {
					case 1:
	                    st = "Uhmm, esa señal me informa de una plataforma móvil";
						break;
					case 2:
	                    st = "Debe de ser esta plataforma de mi derecha";
						break;
					case 3:
	                    st = "Apuesto a que puedo hacerla subir más alto...";
						break;
				}
			}
			if (myMakersTipNumber == 3) {
				switch (txtNumber) {
					case 1:
	                    st = "Canastos, esta palanca no es igual.";
						break;
					case 2:
	                    st = "Mosquis, tiene un temporizador.";
						break;
					case 3:
	                    st = "Ya puedo salir pitando.";
						break;
				}
			}
		}
		return st;
	}
}
