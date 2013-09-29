using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 4/9/2012</para>
/// <para>Last modified: 23/01/2013 00:10</para> 
/// <para>Author: Marcos Zalacain </para>
/// SettingsSceneGUI:   
///    -This script is to launch the GUI Buttons, background, audio and set PlayerPrefs.
/// </summary>
public class SettingsSceneGUI : MonoBehaviour {
	
	public Texture backgroundTexture;
	public Texture buttonSampleTexture;
	public GUIStyle titleStyle;
	public GUIStyle labelStyle;
	public GUIStyle buttonStyle;
	public GUIStyle backButtonStyle;
	public Texture2D currentOn, currentOff, currentOnH, currentOffH;
	public Font titleFontSML;
	public Font titleFontSMH;
	public Font titleFontN7;
	public Font titleFontN10;
	public Font labelFontSML;
	public Font labelFontSMH;
	public Font labelFontN7;
	public Font labelFontN10;
	public Font bttonFontSML;
	public Font bttonFontSMH;
	public Font bttonFontN7;
	public Font bttonFontN10;
	
	private float screenWidth;
	private float screenHeight;
	private float unitW, unitH;
	private bool buttonsShowed;		// Are we showing the buttons?
	private GameObject backgroudMusic;
	
	private float currentPosition;
	private float finalPosition;
	private bool extraSettings = false;
	private bool targetExtraSettings = true;
	
	// Sliders here.
	private float deviceSliderValue;
	private float deviceTypechoosen;
	private float btnSliderValue;
	
	void Start () {
		
		// Size related.
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		unitW = screenWidth/20;
		unitH = screenHeight/20;
		
		// We set font acording to deviceType here.
		if (Globals.deviceType == Globals.SmartPhoneL) {
			titleStyle.font = titleFontSML;
			labelStyle.font = labelFontSML;
			buttonStyle.font = bttonFontSML;
			deviceTypechoosen = 1;
			deviceSliderValue = 0.5f;
		}else if (Globals.deviceType == Globals.SmartPhoneH) {
			titleStyle.font = titleFontSMH;
			labelStyle.font = labelFontSMH;
			buttonStyle.font = bttonFontSMH;
			deviceTypechoosen = 2;
			deviceSliderValue = 1.5f;
		}else if (Globals.deviceType == Globals.N7) {
			titleStyle.font = titleFontN7;
			labelStyle.font = labelFontN7;
			buttonStyle.font = bttonFontN7;
			deviceTypechoosen = 3;
			deviceSliderValue = 2.5f;
		}else{
			titleStyle.font = titleFontN10;
			labelStyle.font = labelFontN10;
			buttonStyle.font = bttonFontN10;
			deviceTypechoosen = 4;
			deviceSliderValue = 3.5f;
		}
		this.guiTexture.pixelInset = new Rect(0, 0, screenWidth, screenHeight);
		this.guiTexture.texture = backgroundTexture;
		
		buttonsShowed = true;
		
		// Audio singleton.
		backgroudMusic = GameObject.Find("BackgroundMusic");
		
		// Items positions.
		currentPosition = 0*unitW;
		finalPosition = 0*unitW;
		
		btnSliderValue = Globals.choosenButtonSize;
	}
	
	void Update () {
		
		// Extra settings.
		if (extraSettings) {
			// If btn pressed
			if(!targetExtraSettings && currentPosition < finalPosition){
				currentPosition = currentPosition + unitW;
			}
			if (currentPosition >= 0 ) {
				targetExtraSettings = true;
				extraSettings = false;
			}
		// Reg settings.
		}else {
			// if btn pressed
			if (targetExtraSettings = true && currentPosition > finalPosition) {
				currentPosition = currentPosition - unitW;
			}
			if (Mathf.Floor(currentPosition) <= Mathf.Floor(-22*unitW)) {
				targetExtraSettings = false;
				extraSettings = true;
			}
		}
	}
	
	void OnGUI() {
		
		// Game Title.
		GUI.Label(new Rect(6*unitW, unitH, 14*unitW, 3*unitH), TextStrings.gameTitle, titleStyle);
			
		if (buttonsShowed) {
		
			// 1. Language Settings.
			GUI.Label(new Rect(currentPosition + unitW, 6.5f*unitH, 6*unitW, 3*unitH), TextStrings.languageString, labelStyle);
			if (Globals.choosenLanguage == Globals.English) {			// IF ENGLISH SELECTED
				buttonStyle.normal.background = currentOn;
				buttonStyle.hover.background = currentOnH;
		        if (GUI.Button(new Rect(currentPosition + 5*unitW, 6*unitH, 6*unitW, 2*unitH), TextStrings.englishOption, buttonStyle)){
					backgroudMusic.SendMessage("buttonsSoundEffect");			// Play sound.
					Globals.choosenLanguage = Globals.English;
					setPlayerPrefsSettings();
					TextStrings.SetStrings();
				}	
				buttonStyle.normal.background = currentOff;
				buttonStyle.hover.background = currentOffH;
				if (GUI.Button(new Rect(currentPosition + 12*unitW, 6*unitH, 6*unitW, 2*unitH), TextStrings.spanishOption, buttonStyle)){
					backgroudMusic.SendMessage("buttonsSoundEffect");
					Globals.choosenLanguage = Globals.Spanish;
					setPlayerPrefsSettings();
					TextStrings.SetStrings();
				}
			}else{														// IF SPANISH SELECTED!
				buttonStyle.normal.background = currentOff;
				buttonStyle.hover.background = currentOffH;
		        if (GUI.Button(new Rect(currentPosition + 5*unitW, 6*unitH, 6*unitW, 2*unitH), TextStrings.englishOption, buttonStyle)){
					backgroudMusic.SendMessage("buttonsSoundEffect");
					Globals.choosenLanguage = Globals.English;
					setPlayerPrefsSettings();
					TextStrings.SetStrings();
				}
				buttonStyle.normal.background = currentOn;
				buttonStyle.hover.background = currentOnH;
				if (GUI.Button(new Rect(currentPosition + 12*unitW, 6*unitH, 6*unitW, 2*unitH), TextStrings.spanishOption, buttonStyle)){
					backgroudMusic.SendMessage("buttonsSoundEffect");
					Globals.choosenLanguage = Globals.Spanish;
					setPlayerPrefsSettings();
					TextStrings.SetStrings();
				}				
			}
			
			// 2. Audio Settings.
			GUI.Label(new Rect(currentPosition + unitW, 9.5f*unitH, 6*unitW, 3*unitH), TextStrings.audioString, labelStyle);
			if (Globals.choosenMusic == Globals.MusicOn) {			// IF MUSIC IS ON
				buttonStyle.normal.background = currentOn;
				buttonStyle.hover.background = currentOnH;
				if (GUI.Button(new Rect(currentPosition + 5*unitW, 9*unitH, 6*unitW, 2*unitH), TextStrings.onOption, buttonStyle)){
					backgroudMusic.SendMessage("buttonsSoundEffect");
					Globals.choosenMusic = Globals.MusicOn;
					setPlayerPrefsSettings();
				}	
				buttonStyle.normal.background = currentOff;
				buttonStyle.hover.background = currentOffH;
				if (GUI.Button(new Rect(currentPosition + 12*unitW, 9*unitH, 6*unitW, 2*unitH), TextStrings.offOption, buttonStyle)){
					backgroudMusic.SendMessage("buttonsSoundEffect");
					Globals.choosenMusic = Globals.MusicOff;
					setPlayerPrefsSettings();
					// Turn Music OFF!
					backgroudMusic.audio.Stop();
				}
			}else{													// IF MUSIC IS OFF
				buttonStyle.normal.background = currentOff;
				buttonStyle.hover.background = currentOffH;
				if (GUI.Button(new Rect(currentPosition + 5*unitW, 9*unitH, 6*unitW, 2*unitH), TextStrings.onOption, buttonStyle)){
					backgroudMusic.SendMessage("buttonsSoundEffect");
					Globals.choosenMusic = Globals.MusicOn;
					setPlayerPrefsSettings();
					// Turn Music ON!
					backgroudMusic.audio.Play();
				}	
				buttonStyle.normal.background = currentOn;
				buttonStyle.hover.background = currentOnH;
				if (GUI.Button(new Rect(currentPosition + 12*unitW, 9*unitH, 6*unitW, 2*unitH), TextStrings.offOption, buttonStyle)){
					backgroudMusic.SendMessage("buttonsSoundEffect");
					Globals.choosenMusic = Globals.MusicOff;
					setPlayerPrefsSettings();
				}				
			}
		
			// 3. Sound Effects Settings.
			GUI.Label(new Rect(currentPosition + unitW, 12.5f*unitH, 6*unitW, 3*unitH), TextStrings.sndEffString, labelStyle);
			if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {			// IF SndE ON
				buttonStyle.normal.background = currentOn;
				buttonStyle.hover.background = currentOnH;
				if (GUI.Button(new Rect(currentPosition + 5*unitW, 12*unitH, 6*unitW, 2*unitH), TextStrings.onOption, buttonStyle)){
					backgroudMusic.SendMessage("buttonsSoundEffect");
					Globals.choosenSoundEffects = Globals.SoundEffectsOn;
					setPlayerPrefsSettings();
				}	
				buttonStyle.normal.background = currentOff;
				buttonStyle.hover.background = currentOffH;
				if (GUI.Button(new Rect(currentPosition + 12*unitW, 12*unitH, 6*unitW, 2*unitH), TextStrings.offOption, buttonStyle)){
					Globals.choosenSoundEffects = Globals.SoundEffectsOnOff;
					setPlayerPrefsSettings();
				}
			}else{																	// IF SndE OFF
				buttonStyle.normal.background = currentOff;
				buttonStyle.hover.background = currentOffH;
				if (GUI.Button(new Rect(currentPosition + 5*unitW, 12*unitH, 6*unitW, 2*unitH), TextStrings.onOption, buttonStyle)){
					backgroudMusic.SendMessage("buttonsSoundEffect");
					Globals.choosenSoundEffects = Globals.SoundEffectsOn;
					setPlayerPrefsSettings();
				}	
				buttonStyle.normal.background = currentOn;
				buttonStyle.hover.background = currentOnH;
				if (GUI.Button(new Rect(currentPosition + 12*unitW, 12*unitH, 6*unitW, 2*unitH), TextStrings.offOption, buttonStyle)){
					Globals.choosenSoundEffects = Globals.SoundEffectsOnOff;
					setPlayerPrefsSettings();
				}				
			}			
			
			// Extra Settings....................................................		
			// 4. Device Settings. Is a Tablet!
			GUI.Label(new Rect(currentPosition + 23*unitW, 4.5f*unitH, 6*unitW, 3*unitH), TextStrings.deviceString, labelStyle);
			deviceSliderValue = GUI.HorizontalSlider(new Rect(currentPosition + 27*unitW, 5.0f*unitH, 9*unitW, 2*unitH), deviceSliderValue, 0.0f, 4.0f);
			
			if (deviceSliderValue < 1) {
				GUI.Label(new Rect(currentPosition + 37*unitW, 4.5f*unitH, 3*unitW, 2*unitH), "Small", labelStyle);
				Globals.deviceType = Globals.SmartPhoneL;
				deviceTypechoosen = 1;
				titleStyle.font = titleFontSML;
				labelStyle.font = labelFontSML;
				buttonStyle.font = bttonFontSML;
				//setPlayerPrefsSettings();	
			}else if (deviceSliderValue >= 1 && deviceSliderValue < 2) {
				GUI.Label(new Rect(currentPosition + 37*unitW, 4.5f*unitH, 3*unitW, 2*unitH), "Large", labelStyle);
				Globals.deviceType = Globals.SmartPhoneH;
				deviceTypechoosen = 2;
				titleStyle.font = titleFontSMH;
				labelStyle.font = labelFontSMH;
				buttonStyle.font = bttonFontSMH;
				//setPlayerPrefsSettings();
			}else if (deviceSliderValue >= 2 && deviceSliderValue < 3) {
				GUI.Label(new Rect(currentPosition + 37*unitW, 4.5f*unitH, 3*unitW, 2*unitH), "Nexus7", labelStyle);
				Globals.deviceType = Globals.N7;
				deviceTypechoosen = 3;
				titleStyle.font = titleFontN7;
				labelStyle.font = labelFontN7;
				buttonStyle.font = bttonFontN7;
				//setPlayerPrefsSettings();
			}else {
				GUI.Label(new Rect(currentPosition + 37*unitW, 4.5f*unitH, 3*unitW, 2*unitH), "Nexus10", labelStyle);
				Globals.deviceType = Globals.N10;
				deviceTypechoosen = 4;
				titleStyle.font = titleFontN10;
				labelStyle.font = labelFontN10;
				buttonStyle.font = bttonFontN10;
				//setPlayerPrefsSettings();
			}
	
			// 5. GamePlay buttons size. 
			GUI.Label(new Rect(currentPosition + 23*unitW, 7.5f*unitH, 6*unitW, 3*unitH), TextStrings.buttonSizeString, labelStyle);
			btnSliderValue = GUI.HorizontalSlider(new Rect(currentPosition + 27*unitW, 8.0f*unitH, 9*unitW, 2*unitH), btnSliderValue, 0.5f, 3);
			GUI.DrawTexture(new Rect(currentPosition + 37*unitW, 6.5f*unitH, btnSliderValue*(1.8f*unitW), btnSliderValue*(1.8f*unitW)), buttonSampleTexture);
			GUI.Label(new Rect(currentPosition + 31*unitW, 6.7f*unitH, 2*unitW, 2*unitH), btnSliderValue.ToString("0.0#"), labelStyle);
			
			// 6. Sophie vertical position.
			GUI.Label(new Rect(currentPosition + 23*unitW, 10.5f*unitH, 10*unitW, 3*unitH), TextStrings.SVposition, labelStyle);
			if (Globals.heightOffsetG == 2.0f) {			// IF heightOffsetG = low!
				buttonStyle.normal.background = currentOn;
				buttonStyle.hover.background = currentOnH;
				if (GUI.Button(new Rect(currentPosition + 31*unitW, 10*unitH, 4*unitW, 2*unitH), TextStrings.SVpositionL, buttonStyle)){
					backgroudMusic.SendMessage("buttonsSoundEffect");
				}
				buttonStyle.normal.background = currentOff;
				buttonStyle.hover.background = currentOffH;
				if (GUI.Button(new Rect(currentPosition + 36*unitW, 10*unitH, 4*unitW, 2*unitH), TextStrings.SVpositionH, buttonStyle)){
					backgroudMusic.SendMessage("buttonsSoundEffect");
					Globals.heightOffsetG = 1.5f;
					setPlayerPrefsSettings();
				}
			}else{											// IF heightOffsetG = High!
				buttonStyle.normal.background = currentOff;
				buttonStyle.hover.background = currentOffH;
				if (GUI.Button(new Rect(currentPosition + 31*unitW, 10*unitH, 4*unitW, 2*unitH), TextStrings.SVpositionL, buttonStyle)){
					backgroudMusic.SendMessage("buttonsSoundEffect");
					Globals.heightOffsetG = 2.0f;
					setPlayerPrefsSettings();
				}
				buttonStyle.normal.background = currentOn;
				buttonStyle.hover.background = currentOnH;
				if (GUI.Button(new Rect(currentPosition + 36*unitW, 10*unitH, 4*unitW, 2*unitH), TextStrings.SVpositionH, buttonStyle)){
					backgroudMusic.SendMessage("buttonsSoundEffect");
				}
			}
			
			// 7. Sophie horizontal position. 
			GUI.Label(new Rect(currentPosition + 23*unitW, 13.5f*unitH, 10*unitW, 3*unitH), TextStrings.SHposition, labelStyle);
			if (Globals.widthOffsetG == 4.0f) {			// IF widthOffsetG = Left!
				buttonStyle.normal.background = currentOn;
				buttonStyle.hover.background = currentOnH;
				if (GUI.Button(new Rect(currentPosition + 31*unitW, 13*unitH, 4*unitW, 2*unitH), TextStrings.SHpositionL, buttonStyle)){
					backgroudMusic.SendMessage("buttonsSoundEffect");
				}
				buttonStyle.normal.background = currentOff;
				buttonStyle.hover.background = currentOffH;
				if (GUI.Button(new Rect(currentPosition + 36*unitW, 13*unitH, 4*unitW, 2*unitH), TextStrings.SHpositionR, buttonStyle)){
					backgroudMusic.SendMessage("buttonsSoundEffect");
					Globals.widthOffsetG = 3.0f;
					setPlayerPrefsSettings();
				}
			}else{											// IF widthOffsetG = Right!
				buttonStyle.normal.background = currentOff;
				buttonStyle.hover.background = currentOffH;
				if (GUI.Button(new Rect(currentPosition + 31*unitW, 13*unitH, 4*unitW, 2*unitH), TextStrings.SHpositionL, buttonStyle)){
					backgroudMusic.SendMessage("buttonsSoundEffect");
					Globals.widthOffsetG = 4.0f;
					setPlayerPrefsSettings();
				}
				buttonStyle.normal.background = currentOn;
				buttonStyle.hover.background = currentOnH;
				if (GUI.Button(new Rect(currentPosition + 36*unitW, 13*unitH, 4*unitW, 2*unitH), TextStrings.SHpositionR, buttonStyle)){
					backgroudMusic.SendMessage("buttonsSoundEffect");
				}
			}			
			
			// Extra settings Button.
			buttonStyle.normal.background = currentOff;
			buttonStyle.hover.background = currentOffH;
			if (extraSettings) {
				if (GUI.Button(new Rect(2*unitW, 16*unitH, 5*unitW, 2*unitH), TextStrings.regularSettString, buttonStyle)){
					backgroudMusic.SendMessage("buttonsSoundEffect");
					finalPosition = unitW;
				}	
			}else{
				if (GUI.Button(new Rect(2*unitW, 16*unitH, 5*unitW, 2*unitH), TextStrings.extraSettString, buttonStyle)){
					backgroudMusic.SendMessage("buttonsSoundEffect");
					finalPosition = -22*unitW;
				}
			}
			
			// Back Button.
			if (GUI.Button(new Rect(16*unitW, 16*unitH, 3*unitW, 2*unitH), "", backButtonStyle)){
				Application.LoadLevel("WelcomeScene");
				backgroudMusic.SendMessage("backButtonsSoundEffect");
				Globals.deviceType = (int)deviceTypechoosen;
				Globals.choosenButtonSize = btnSliderValue;
				setPlayerPrefsSettings();
			}
		}
    }
	
	// Set PlayerPrefs. We'll call this function only on the SettingsScene, when clicking save or back.
	void setPlayerPrefsSettings(){
		PlayerPrefs.SetInt("deviceTypeSettings", Globals.deviceType);
		PlayerPrefs.SetInt("musicSettings", Globals.choosenMusic);
		PlayerPrefs.SetInt("soundEffectsSettings", Globals.choosenSoundEffects);
		PlayerPrefs.SetInt("languageSettings", Globals.choosenLanguage);
		PlayerPrefs.SetFloat("buttonSizeSettings", Globals.choosenButtonSize);
		
		PlayerPrefs.SetFloat("widthOffset", Globals.widthOffsetG);
		PlayerPrefs.SetFloat("heightOffset", Globals.heightOffsetG);
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
}
