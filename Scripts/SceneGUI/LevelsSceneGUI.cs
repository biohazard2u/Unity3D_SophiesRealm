using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 5/9/2012</para>
/// <para>Last modified: 02/03/2013 18:25</para> 
/// <para>Author: Marcos Zalacain </para>
/// SettingsSceneGUI:   
///    -This script is to launch the GUI Buttons, background, audio and controlling last level achieved.
/// </summary>
public class LevelsSceneGUI : MonoBehaviour {

	public Texture backgroundTexture;
	public Texture2D playableTexture, playableTexturePressed;
	public Texture2D lockedTexture, lockedTexturePressed;
	public Texture2D nextArrowTexture;		// To move to next levels page
	public Texture2D backArrowTexture;		// To move to previous levels page
	public Texture2D comingSoonTexture;		// For coming soon gameLevels/levelGroup.
	
	public GUIStyle titleStyle;
	public GUIStyle buttonStyle;
	public GUIStyle backButtonStyle;
	public GUIStyle voteUsStyle;
	public Font titleFontSML;
	public Font titleFontSMH;
	public Font titleFontN7;
	public Font titleFontN10;
	public Font buttonFontSML;
	public Font buttonFontSMH;
	public Font buttonFontN7;
	public Font buttonFontN10;
	public Font backButtonFontSML;
	public Font backButtonFontSMH;
	public Font backButtonFontN7;
	public Font backButtonFontN10;
	
	private GameObject audioController;
	
	private float screenWidth;
	private float screenHeight;
	private float unitW, unitH;
	private bool buttonsShowed;		// Are we showing the buttons?
	
	private int levelGroup;
	
	private float comingSoonX, comingSoonY;
	
	void Start () {
		
		// Size related.
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		unitW = screenWidth/20;
		unitH = screenHeight/20;
		
		// We set the background and styles acording to deviceType here.
		if (Globals.deviceType == Globals.SmartPhoneL) {
			titleStyle.font = titleFontSML;
			buttonStyle.font = buttonFontSML;
			backButtonStyle.font = backButtonFontSML;
			voteUsStyle.font = titleFontSML;
			comingSoonX = 3.2f*unitW;
			comingSoonY = 2.5f*unitH;
		}else if (Globals.deviceType == Globals.SmartPhoneH) {
			titleStyle.font = titleFontSMH;
			buttonStyle.font = buttonFontSMH;
			backButtonStyle.font = backButtonFontSMH;	
			voteUsStyle.font = titleFontSMH;
			comingSoonX = 4.1f*unitW;
			comingSoonY = 3.2f*unitH;
		}else if (Globals.deviceType == Globals.N7) {
			titleStyle.font = titleFontN7;
			buttonStyle.font = buttonFontN7;
			backButtonStyle.font = backButtonFontN7;
			voteUsStyle.font = titleFontSMH;
			comingSoonX = 3.8f*unitW;
			comingSoonY = 3.1f*unitH;
		}else{
			titleStyle.font = titleFontN10;
			buttonStyle.font = buttonFontN10;
			backButtonStyle.font = backButtonFontN10;
			voteUsStyle.font = titleFontSMH;
			comingSoonX = 6.4f*unitW;
			comingSoonY = 6.5f*unitH;
		}
		
		this.guiTexture.pixelInset = new Rect(0, 0, screenWidth, screenHeight);
		this.guiTexture.texture = backgroundTexture;
		
		buttonsShowed = true;
		
		// Audio ref here.
		audioController = GameObject.Find("BackgroundMusic");		// 4 button snd effec for now ONLY.
		
		// Music track should be launched here.
		levelGroup = (int)Globals.lastCompletedLevel/9 + 1;		
	}
	
	void Update () {}
	
	void OnGUI() {
		
		GUI.Label(new Rect(2*unitW, 2*unitH, 16*unitW, 3*unitH), setLevelGroupTitle(levelGroup), titleStyle);
		
		if (buttonsShowed) {
			
			// 1. Left arrow here if needed.
			if (levelGroup != 1) {
				if (GUI.Button(new Rect(1.5f*unitW, 9*unitH, 2*unitW, 2*unitH),  backArrowTexture)){
					levelGroup--;
					// Play sound.
				}
			}
			
			// 2. This is for levels Buttons.
			int btnNumber;
			if (levelGroup == 1) {
				btnNumber = 1;
			}else{
				btnNumber = (1 + levelGroup * 9) - 9;
			}
			//int btnNumber = 1;
			float spaceBtwnW = 0, spaceBtwnH = 0;
			for (int i = 1; i < 4; i++) {
				for (int j = 1; j < 4; j++) {
					
					if (Globals.lastCompletedLevel + 1 >= btnNumber) {
						buttonStyle.normal.background = playableTexture;
						buttonStyle.hover.background = playableTexturePressed;
					}else{
						buttonStyle.normal.background = lockedTexture;
						buttonStyle.hover.background = lockedTexturePressed;
					}
					
					if (GUI.Button(new Rect(j*3*unitW + spaceBtwnW + 1.5f*unitW, i*3*unitH + spaceBtwnH + 2*unitH, 3*unitW, 3*unitH), btnNumber.ToString(), buttonStyle)){
						if (Globals.lastCompletedLevel + 1 >= btnNumber) {
							audioController.SendMessage("buttonsSoundEffect");
							Globals.levelToLaunch = btnNumber;
							// DOES IT HAVE STORY SCENE?
							if ((btnNumber-1) % 9 == 0) {
								Application.LoadLevel("StoryScene");
							}else{
								Application.LoadLevel("Level" + btnNumber);
							}							
						}else{
							audioController.SendMessage("backButtonsSoundEffect");
						}
					}
					btnNumber ++;
					spaceBtwnW = unitW*j;
				}
				spaceBtwnH = unitH * i;
				spaceBtwnW = 0;
			}
			
			// 3. Right arrow here if needed.
			if (levelGroup != 9) {
				if (GUI.Button(new Rect(16.5f*unitW, 9*unitH, 2*unitW, 2*unitH),  nextArrowTexture)){
					levelGroup++;
					// Play sound.
				}
			}
			
			// 4. BackButton
			if (GUI.Button(new Rect(16*unitW, 16*unitH, 3*unitW, 2*unitH), "", backButtonStyle)){
				Application.LoadLevel("WelcomeScene");
				audioController.SendMessage("backButtonsSoundEffect");
			}
			
			// 5. Pro only stuff (no more coming soon).
			//if (levelGroup > 8) {
			//	GUI.Label(new Rect(comingSoonX, comingSoonY, 14*unitW, 15*unitH), comingSoonTexture);
			//	// Give Us a Push, vote for us on googlePlay!
			//	if (GUI.Button(new Rect(4*unitW, 17*unitH, 10*unitW, 2*unitH), TextStrings.giveUsaPush, voteUsStyle)){
			//		Application.OpenURL("https://play.google.com/store/apps/details?id=com.H2OGames.SophiesRealm&feature=search_result&write_review=true#?t=W251bGwsMSwyLDEsImNvbS5IMk9HYW1lcy5Tb3BoaWVzUmVhbG0iXQ..");
			//	}
			//}
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
	
	// Setting the string for each grouplevel.
	string setLevelGroupTitle(int lvlgrp){
		 
		string st = "";
		
		switch (lvlgrp) {
			case 1:
				st = TextStrings.levelGroupTitle1;
				break;
			case 2:
				st = TextStrings.levelGroupTitle2;
				break;
			case 3:
				st = TextStrings.levelGroupTitle3;
				break;
			case 4:
				st = TextStrings.levelGroupTitle4;
				break;
			case 5:
				st = TextStrings.levelGroupTitle5;
				break;
			case 6:
				st = TextStrings.levelGroupTitle6;
				break;
			case 7:
				st = TextStrings.levelGroupTitle7;
				break;
			case 8:
				st = TextStrings.levelGroupTitle8;
				break;
			case 9:
				st = TextStrings.levelGroupTitle9;
				break;		
		}
		
		return st;
	}
}
