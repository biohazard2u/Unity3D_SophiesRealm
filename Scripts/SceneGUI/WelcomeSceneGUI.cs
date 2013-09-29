using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 3/9/2012</para>
/// <para>Last modified: 22/01/2013 23:30</para> 
/// <para>Author: Marcos Zalacain </para>
/// WelcomeSceneGUI:   
///    -This script is to get/set global values, check playerPrefs and launch the GUI Buttons, background and audio.
/// </summary>
public class WelcomeSceneGUI : MonoBehaviour {

	public Texture background;
	public GUIStyle myTitleStyle;
	public GUIStyle myButtonStyle;	
	public Font titleFontSML;
	public Font titleFontSMH;
	public Font titleFontN7;
	public Font titleFontN10;
	public Font buttonFontSML;
	public Font buttonFontSMH;
	public Font buttonFontN7;
	public Font buttonFontN10;
	
	// Audio Controller reference here.
	private GameObject audioController;
	
	private float screenWidth;
	private float screenHeight;
	private GUIStyle titleStyle;
	private GUIStyle buttonStyle;
	private bool beenHere = false;	
	private float unitW, unitH;
	private bool buttonsShowed;		// Are we showing the buttons?
	
	private float currentPosition, finalPosition;
	
	private string startOrContinueString;
	
	void Awake () {
		
		// Size related.
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		unitW = screenWidth/20;
		unitH = screenHeight/20;
		
		// V2.0
		if (!PlayerPrefs.HasKey("widthOffset")) {
			Globals.widthOffsetG = 3.0f;
			Globals.heightOffsetG = 1.5f;
			PlayerPrefs.SetFloat("widthOffset", Globals.widthOffsetG);
			PlayerPrefs.SetFloat("heightOffset", Globals.heightOffsetG);
		}
		// V2.0
		
		// Checking if there are PlayerPrefs already. 
		if (!PlayerPrefs.HasKey("deviceTypeSettings")) {	// First time for the app to be launch on this device.
			Globals.initializeGlobals();
			suposedDeviceType();
			PlayerPrefs.SetInt("deviceTypeSettings", Globals.deviceType);
			PlayerPrefs.SetInt("musicSettings", Globals.choosenMusic);
			PlayerPrefs.SetInt("soundEffectsSettings", Globals.choosenSoundEffects);
			PlayerPrefs.SetInt("languageSettings", Globals.choosenLanguage);
			PlayerPrefs.SetFloat("buttonSizeSettings", Globals.choosenButtonSize);
			//PlayerPrefs.SetFloat("widthOffset", Globals.widthOffsetG);
			//PlayerPrefs.SetFloat("heightOffset", Globals.heightOffsetG);
			PlayerPrefs.SetInt("completedLevelAchieved", Globals.lastCompletedLevel);
		}else if(PlayerPrefs.HasKey("deviceTypeSettings") && beenHere == false){
			checkPlayerPrefs();
			beenHere = true;
		}else{
			// Nothing here. We'll handle all with the Globals static variables.
		}
		
		// We set the strings values here.
		TextStrings.SetStrings();		

		// We set the background and styles acording to deviceType here.
		if (Globals.deviceType == Globals.SmartPhoneL) {
			myTitleStyle.font = titleFontSML;
			myButtonStyle.font = buttonFontSML;
		}else if (Globals.deviceType == Globals.SmartPhoneH) {
			myTitleStyle.font = titleFontSMH;
			myButtonStyle.font = buttonFontSMH;	
		}else if (Globals.deviceType == Globals.N7) {
			myTitleStyle.font = titleFontN7;
			myButtonStyle.font = buttonFontN7;
		}else{
			myTitleStyle.font = titleFontN10;
			myButtonStyle.font = buttonFontN10;
		}
		this.guiTexture.pixelInset = new Rect(0, 0, screenWidth, screenHeight);
		this.guiTexture.texture = background;
		
		buttonsShowed = true;
	}
	
	void Start(){
		currentPosition = 15*unitW;
		finalPosition = 2*unitW;
		
		// checking if start game or continue game.
		if (Globals.lastCompletedLevel == 0) {
			startOrContinueString = TextStrings.startButton;
		}else{
			startOrContinueString = TextStrings.continueGameButton;
		}		
	}
	
	void Update () {
		if (currentPosition > finalPosition) {
			currentPosition = currentPosition - 500*Time.deltaTime;
		}
	}

	 void OnGUI() {
			
		GUI.Label(new Rect(currentPosition, unitH, 16*unitW, 3*unitH), TextStrings.gameTitle, myTitleStyle);
		
		if (buttonsShowed) {
			// Start Game.
	        if (GUI.Button(new Rect(unitW, 8*unitH, 8.5f*unitW, 3*unitH), startOrContinueString, myButtonStyle)){
				playSE();
	            Globals.levelToLaunch = Globals.lastCompletedLevel + 1;
				// DOES IT HAVE STORY SCENE?
				if (Globals.lastCompletedLevel % 9 == 0) {
					Application.LoadLevel("StoryScene");
				}else{
					Application.LoadLevel("Level" + Globals.levelToLaunch);
				}
			}
			// Levels Selection.
			if (GUI.Button(new Rect(unitW, 12*unitH, 8.5f*unitW, 3*unitH), TextStrings.levelsSelectionButton, myButtonStyle)){
				playSE();
				Application.LoadLevel("LevelsScene");
			}
			// Settings.
			if (GUI.Button(new Rect(unitW, 16*unitH, 8.5f*unitW, 3*unitH), TextStrings.settingsButton, myButtonStyle)){
				playSE();
				Application.LoadLevel("SettingsScene");
			}	
			// More.
			if (GUI.Button(new Rect(10.5f*unitW, 16*unitH, 3.75f*unitW, 3*unitH), TextStrings.moreButton, myButtonStyle)){
				playSE();
				Application.LoadLevel("MoreScene");
			}	
			// Exit.
			if (GUI.Button(new Rect(15.25f*unitW, 16*unitH, 3.75f*unitW, 3*unitH), TextStrings.exitButton, myButtonStyle)){
				Application.Quit();
			}
		}
     }
	
	// Check for PlayerPrefs. We'll call this function everytime we start the application.
	public void checkPlayerPrefs(){

		// Device type Settings.
		if (PlayerPrefs.HasKey("deviceTypeSettings")) {
			Globals.deviceType = PlayerPrefs.GetInt("deviceTypeSettings");
		}
		// Music Settings.
		if (PlayerPrefs.HasKey("musicSettings")) {
			Globals.choosenMusic = PlayerPrefs.GetInt("musicSettings");
		}
		// Sound Effects Settings.
		if (PlayerPrefs.HasKey("soundEffectsSettings")) {
			Globals.choosenSoundEffects = PlayerPrefs.GetInt("soundEffectsSettings");
		}
		// Language Settings.
		if (PlayerPrefs.HasKey("languageSettings")) {
			Globals.choosenLanguage = PlayerPrefs.GetInt("languageSettings");
		}
		// Button Size Settings.
		if (PlayerPrefs.HasKey("buttonSizeSettings")) {
			Globals.choosenButtonSize = PlayerPrefs.GetFloat("buttonSizeSettings");
		}
		// Completed Level Achieved.
		if (PlayerPrefs.HasKey("completedLevelAchieved")) {
			Globals.lastCompletedLevel = PlayerPrefs.GetInt("completedLevelAchieved");
		}
		// widthOffset Settings.
		if (PlayerPrefs.HasKey("widthOffset")) {
			Globals.widthOffsetG = PlayerPrefs.GetFloat("widthOffset");
		}
		// heightOffset Settings.
		if (PlayerPrefs.HasKey("heightOffset")) {
			Globals.heightOffsetG = PlayerPrefs.GetFloat("heightOffset");
		}
	}
	
	// Determine a probable device type through diagonal amount of pixels.
	void suposedDeviceType(){
		
		//float screenSize = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));	// Changed to width only.
		
		// SmartPhoneL
		if (screenWidth < 500.0f) {
			Globals.deviceType = Globals.SmartPhoneL;
		}
		// SmartPhoneH
		else if (screenWidth >  500.0f && screenWidth < 1000.0f) {
			Globals.deviceType = Globals.SmartPhoneH;
		}
		// N7
		else if (screenWidth > 1000.0f && screenWidth < 1300.0f) {
			Globals.deviceType = Globals.N7;
		}
		// N10
		else{
			Globals.deviceType = Globals.N10;
		}
	}
	
	// This is call when back/exit button to hide buttons.
	public void showButtons(bool trueOrFalse){
		if (!buttonsShowed && trueOrFalse == true) {
			buttonsShowed = true;
		}
		if (buttonsShowed && trueOrFalse == false) {
			buttonsShowed = false;
		}
	}
	
	void playSE(){
		audioController = GameObject.Find("BackgroundMusic");
		audioController.SendMessage("buttonsSoundEffect");
	}
}
