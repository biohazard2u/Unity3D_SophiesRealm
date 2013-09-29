using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 25/03/2013</para>
/// <para>Last modified: 01/04/2013 19:00</para> 
/// <para>Author: Marcos Zalacain </para>
/// TipsPrefabSuperTrooper:   
///    -This is the super trooper tutorial. It has been extended for carnivorousPlantKillers tutorial.
/// </summary>
public class TipsPrefabSuperTrooper : MonoBehaviour {

	public GameObject mainCameraObject;		
	
	public Texture2D superTrooper;
	public Texture2D plantKillerArrow;
	public Texture2D maeva;
	
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
	
	private Texture2D imageTexture;
	private int currentLevelNumber;
		
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
		// Watch OUT: we dont need this since we dont turn them on @ TurnControlChildrenOnOFF.cs for level 1 nor 37.
		//GameObject.Find("GUI/Controls").SendMessage("turnChildrenOff", SendMessageOptions.DontRequireReceiver);
		//GameObject.Find("GUI/HUD").SendMessage("turnChildrenOff", SendMessageOptions.DontRequireReceiver);
		
		// Call CameraScrolling.cs to simulate animation.
		CameraScrolling scriptComponent =  mainCameraObject.GetComponent<CameraScrolling>();
		scriptComponent.StartCoroutine("animationTipsPrefab", 10.0f);
	}
	
	void Start(){
		SophieController.canControl = false;
		//SophieControllerKeyboard.canControl = false;
		
		if (Application.loadedLevelName == "Level37") {
			imageTexture = superTrooper;
			currentLevelNumber = 37;
		}
		if (Application.loadedLevelName == "Level46") {
			imageTexture = plantKillerArrow;
			currentLevelNumber = 46;
		}
		if (Application.loadedLevelName == "Level55") {
			imageTexture = maeva;
			currentLevelNumber = 55;
		}
		if (Application.loadedLevelName == "Level73") {
			imageTexture = superTrooper;
			currentLevelNumber = 73;
		}
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

		GUI.Label(new Rect(unitW, 2*unitH, 4*unitW, 10*unitH), imageTexture);
		
       	GUI.Label(new Rect(4*unitW, 3*unitH, 12*unitW, 1.5f*unitH), setTextStrings(1), questionStyle);
		GUI.Label(new Rect(4*unitW, 5*unitH, 12*unitW, 1.5f*unitH), setTextStrings(2), questionStyle);
		GUI.Label(new Rect(4*unitW, 7*unitH, 12*unitW, 1.5f*unitH), setTextStrings(3), questionStyle);
		
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
	
	// Setting the strings for each language.
	string setTextStrings(int txtNumber){
		
		string st = "";
		
		if(Globals.choosenLanguage == Globals.English){
			
			if (currentLevelNumber == 37) {
				switch (txtNumber) {
				case 1:
	                st = "Sophie, have you seen the spiky troopers?";
					break;
				case 2:
	                st = "These enemy troopers aren’t that easy.";
					break;
				case 3:
	                st = "Be aware of them!";
					break;
				}
			}
			if(currentLevelNumber == 46){
				switch (txtNumber) {
				case 1:
	                st = "Are you getting fed up with those carnivorous plants?";
					break;
				case 2:
	                st = "The Magic woodland is full of them.";
					break;
				case 3:
	                st = "Use weedkiller arrows to finish them up!";
					break;
				}
			}
			if(currentLevelNumber == 55){
				switch (txtNumber) {
				case 1:
	                st = "Watch out with Maeva, she is getting furious!";
					break;
				case 2:
	                st = "She may through you fireballs now.";
					break;
				case 3:
	                st = "Apply the zoom out view to avoid her fireballs easier.";
					break;
				}
			}
			if(currentLevelNumber == 73){
				switch (txtNumber) {
				case 1:
	                st = "Maeva’s troopers are way too many.";
					break;
				case 2:
	                st = "I have magic you up so you can shoot faster.";
					break;
				case 3:
	                st = "Try it, and get done with all those final soldiers!";
					break;
				}
			}
		}else{				
			
			if (currentLevelNumber == 37) {
				switch (txtNumber) {
					case 1:
	                    st = "Sofía, has visto a los enemigos con pinchos?";
						break;
					case 2:
	                    st = "Estos soldados no son tan fáciles.";
						break;
					case 3:
	                    st = "Ten cuidado con ellos!";
						break;
				}
			}
			if(currentLevelNumber == 46){
				switch (txtNumber) {
					case 1:
	                    st = "Te veo un poco harta de tanta planta carnívora.";
						break;
					case 2:
	                    st = "El bosque mágico está repleto de estas plantas.";
						break;
					case 3:
	                    st = "Usa las flechas herbicidas para acabar con ellas!";
						break;
				}
			}
			if(currentLevelNumber == 55){
				switch (txtNumber) {
				case 1:
	                st = "Cuidado con Maeva, cada vez está más furiosa!";
					break;
				case 2:
	                st = "Es posible que te lance bolas de fuego.";
					break;
				case 3:
	                st = "Usa la vista amplia para esquivarlas mejor.";
					break;
				}
			}
			if(currentLevelNumber == 73){
				switch (txtNumber) {
				case 1:
	                st = "Hay demasiados de esos soldados de Maeva. ";
					break;
				case 2:
	                st = "Te he agilizado tu disparo con magia.";
					break;
				case 3:
	                st = "Pruébalo, ahora dispararás más rápido.";
					break;
				}
			}
		}
		return st;
	}
}
