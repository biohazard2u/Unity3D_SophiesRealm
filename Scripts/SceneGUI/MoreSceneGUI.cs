using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 5/9/2012</para>
/// <para>Last modified: 05/01/2013 18:25</para> 
/// <para>Author: Marcos Zalacain </para>
/// SettingsSceneGUI:   
///    -This script is to launch the GUI Buttons, background, audio and Credits.
/// </summary>
public class MoreSceneGUI : MonoBehaviour {
	
	public Texture backgroundTexture;
	public Texture2D logoTexture;
	
	public GUIStyle titleStyle;
	public GUIStyle labelStyle;
	public GUIStyle backButtonStyle;
	public Font titleFontSML;
	public Font titleFontSMH;
	public Font titleFontN7;
	public Font titleFontN10;
	public Font labelFontSML;
	public Font labelFontSMH;
	public Font labelFontN7;
	public Font labelFontN10;
	public Font backButtonFontSML;
	public Font backButtonFontSMH;
	public Font backButtonFontN7;
	public Font backButtonFontN10;
	
	public GUIStyle gPlsStyle;
	public GUIStyle faceStyle;
	public GUIStyle twitStyle;
	public GUIStyle blogStyle;
	
	private GameObject audioController;
	
	private float screenWidth;
	private float screenHeight;
	private float unitW, unitH;
	
	private bool buttonsShowed;		// Are we showing the buttons?
	
	private float currentPosition;
	private float finalPosition;
	
	void Start () {
	
		// Size related.
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		unitW = screenWidth/20;
		unitH = screenHeight/20;
		
		// We set the background and styles acording to deviceType here.
		if (Globals.deviceType == Globals.SmartPhoneL) {
			titleStyle.font = titleFontSML;
			labelStyle.font = labelFontSML;
			backButtonStyle.font = backButtonFontSML;
		}else if (Globals.deviceType == Globals.SmartPhoneH) {
			titleStyle.font = titleFontSMH;
			labelStyle.font = labelFontSMH;
			backButtonStyle.font = backButtonFontSMH;	
		}else if (Globals.deviceType == Globals.N7) {
			titleStyle.font = titleFontN7;
			labelStyle.font = labelFontN7;
			backButtonStyle.font = backButtonFontN7;
		}else{
			titleStyle.font = titleFontN10;
			labelStyle.font = labelFontN10;
			backButtonStyle.font = backButtonFontN10;
		}
		
		this.guiTexture.pixelInset = new Rect(0, 0, screenWidth, screenHeight);
		this.guiTexture.texture = backgroundTexture;
		
		buttonsShowed = true;
		
		// Items positions.
		currentPosition = 15*unitH;
		finalPosition = -73*unitH;
		
		// Audio ref here.
		audioController = GameObject.Find("BackgroundMusic");
	}
	
	void Update () {
		if (currentPosition > finalPosition) {
			currentPosition = currentPosition - 0.04f*unitH;
		}
		if (currentPosition <= finalPosition) {
			currentPosition = 15*unitH;
		}
	}
	
	void OnGUI() {
			
		// Title.
		GUI.Label(new Rect(1*unitW, 2*unitH, 5*unitW, 2*unitH), TextStrings.creditsString, titleStyle);
		// Game General.
		GUI.Label(new Rect(4*unitW, currentPosition + 3.2f*unitH, 15*unitW, 2*unitH), TextStrings.Game, titleStyle);
		GUI.Label(new Rect(4*unitW, currentPosition + 5.3f*unitH, 15*unitW, 2*unitH), TextStrings.gameDesigner + "  Marcos Zalacaín", labelStyle);
		GUI.Label(new Rect(4*unitW, currentPosition + 6.3f*unitH, 15*unitW, 2*unitH), TextStrings.levelsDesigner + "  Marcos Zalacaín", labelStyle);
		GUI.Label(new Rect(4*unitW, currentPosition + 7.3f*unitH, 15*unitW, 2*unitH), TextStrings.scriptWriter + "  Marcos Zalacaín", labelStyle);
		GUI.Label(new Rect(4*unitW, currentPosition + 8.3f*unitH, 15*unitW, 2*unitH), TextStrings.gameAdviser + "  J.Antonio Cerqueiro", labelStyle);
		// Art.
		GUI.Label(new Rect(4*unitW, currentPosition + 11.2f*unitH, 15*unitW, 2*unitH), TextStrings.Art, titleStyle);
		GUI.Label(new Rect(4*unitW, currentPosition + 13.3f*unitH, 15*unitW, 2*unitH), TextStrings.gameArtist + "  J.Antonio Cerqueiro", labelStyle);
		GUI.Label(new Rect(4*unitW, currentPosition + 14.3f*unitH, 15*unitW, 2*unitH), TextStrings.graphicDesigner + "  J.Antonio Cerqueiro", labelStyle);
		GUI.Label(new Rect(4*unitW, currentPosition + 15.3f*unitH, 15*unitW, 2*unitH), TextStrings.ThreeDModeller + "  J.Antonio Cerqueiro", labelStyle);
		GUI.Label(new Rect(4*unitW, currentPosition + 16.3f*unitH, 15*unitW, 2*unitH), TextStrings.textureArtists + "  J.Antonio Cerqueiro", labelStyle);
		GUI.Label(new Rect(4*unitW, currentPosition + 17.3f*unitH, 15*unitW, 2*unitH), TextStrings.characterModellers + "  Unity3D AssetStore, Mixano", labelStyle);
		GUI.Label(new Rect(4*unitW, currentPosition + 18.3f*unitH, 15*unitW, 2*unitH), TextStrings.animators + "  J.Antonio Cerqueiro, Unity AssetStore, Mixano", labelStyle);
		GUI.Label(new Rect(4*unitW, currentPosition + 19.3f*unitH, 15*unitW, 2*unitH), TextStrings.guiDesigner + "  J.Antonio Cerqueiro, Marcos Zalacaín", labelStyle);
		GUI.Label(new Rect(4*unitW, currentPosition + 20.3f*unitH, 15*unitW, 2*unitH), TextStrings.storyImages + "  Marcos Zalacaín", labelStyle);
		GUI.Label(new Rect(4*unitW, currentPosition + 21.3f*unitH, 15*unitW, 2*unitH), TextStrings.mushroomsBy + "  Necturus", labelStyle);
		GUI.Label(new Rect(4*unitW, currentPosition + 22.3f*unitH, 15*unitW, 2*unitH), TextStrings.skyBoxes + "  A-Lab Software", labelStyle);
		// Audio.
		GUI.Label(new Rect(4*unitW, currentPosition + 25.2f*unitH, 15*unitW, 2*unitH), TextStrings.Audio, titleStyle);
		GUI.Label(new Rect(4*unitW, currentPosition + 27.3f*unitH, 15*unitW, 2*unitH), TextStrings.soundEffectsDesigner + "  J.Antonio Cerqueiro,", labelStyle);
		GUI.Label(new Rect(6*unitW, currentPosition + 28.3f*unitH, 15*unitW, 2*unitH), "http://www.soundjay.com/, Mike Koenig, Thore", labelStyle);
		GUI.Label(new Rect(4*unitW, currentPosition + 29.3f*unitH, 15*unitW, 2*unitH), TextStrings.musicComposer + "  J.Antonio Cerqueiro", labelStyle);
		// Programing.
		GUI.Label(new Rect(4*unitW, currentPosition + 32.2f*unitH, 15*unitW, 2*unitH), TextStrings.Programming, titleStyle);
		GUI.Label(new Rect(4*unitW, currentPosition + 34.3f*unitH, 15*unitW, 2*unitH), TextStrings.guiProgrammer + "  Marcos Zalacaín", labelStyle);
		GUI.Label(new Rect(4*unitW, currentPosition + 35.3f*unitH, 15*unitW, 2*unitH), TextStrings.aIProgrammer + "  Marcos Zalacaín", labelStyle);
		GUI.Label(new Rect(4*unitW, currentPosition + 36.3f*unitH, 15*unitW, 2*unitH), TextStrings.gameplayProgrammer + "  Marcos Zalacaín", labelStyle);
		// Other.
		GUI.Label(new Rect(4*unitW, currentPosition + 39.2f*unitH, 15*unitW, 2*unitH), TextStrings.other, titleStyle);
		GUI.Label(new Rect(4*unitW, currentPosition + 41.3f*unitH, 15*unitW, 2*unitH), TextStrings.Testing, labelStyle);		
		GUI.Label(new Rect(7*unitW, currentPosition + 42.3f*unitH, 15*unitW, 2*unitH), "Jorge Zalacaín" , labelStyle);
		GUI.Label(new Rect(7*unitW, currentPosition + 43.3f*unitH, 15*unitW, 2*unitH), "Isabel Langarita" , labelStyle);
		GUI.Label(new Rect(7*unitW, currentPosition + 44.3f*unitH, 15*unitW, 2*unitH), "Edgar Gascón" , labelStyle);
		GUI.Label(new Rect(7*unitW, currentPosition + 45.3f*unitH, 15*unitW, 2*unitH), "Francisco J. Martín" , labelStyle);
		GUI.Label(new Rect(7*unitW, currentPosition + 46.3f*unitH, 15*unitW, 2*unitH), "Sara García" , labelStyle);
		GUI.Label(new Rect(7*unitW, currentPosition + 47.3f*unitH, 15*unitW, 2*unitH), "Ramón Salcedo" , labelStyle);
		GUI.Label(new Rect(7*unitW, currentPosition + 48.3f*unitH, 15*unitW, 2*unitH), "Iván García - Trinit" , labelStyle);
		GUI.Label(new Rect(7*unitW, currentPosition + 49.3f*unitH, 15*unitW, 2*unitH), "Javier Carrillo" , labelStyle);
		GUI.Label(new Rect(7*unitW, currentPosition + 50.3f*unitH, 15*unitW, 2*unitH), "Jorge Tejada" , labelStyle);
		
		GUI.Label(new Rect(4*unitW, currentPosition + 53.2f*unitH, 15*unitW, 2*unitH), "English - script editor:  Stella Deleuze" , labelStyle);
		GUI.Label(new Rect(4*unitW, currentPosition + 56.1f*unitH, 15*unitW, 2*unitH), TextStrings.Marketing + "  Marcos Zalacaín", labelStyle);
		
		// Logo.	
		GUI.Label(new Rect(4*unitW, currentPosition + 59.0f*unitH, 15*unitW, 2*unitH), "Many thanks from H2O", labelStyle);
		GUI.Label(new Rect(6*unitW, currentPosition + 60.0f*unitH, 5*unitW, 5*unitH), logoTexture);
		
		// Space bar.
		GUI.Label(new Rect(4*unitW, currentPosition + 70*unitH, 15*unitW, 2*unitH), "     ........................................" , labelStyle);
		
		if (buttonsShowed) {
			
			// Social Buttons. Application.OpenURL("http://www.url.com");
			if (GUI.Button(new Rect(1*unitW, 6*unitH, 2*unitW, 2*unitH), "", gPlsStyle)){
				Application.OpenURL("https://plus.google.com/u/0/115186090114509660494/posts");
			}
			if (GUI.Button(new Rect(1*unitW, 9*unitH, 2*unitW, 2*unitH), "", faceStyle)){
				Application.OpenURL("http://www.facebook.com/pages/H2O-Games/512035432151369");
			}
			if (GUI.Button(new Rect(1*unitW, 12*unitH, 2*unitW, 2*unitH), "", twitStyle)){
				Application.OpenURL("https://twitter.com/h2ogamestudio");
			}
			if (GUI.Button(new Rect(1*unitW, 15*unitH, 2*unitW, 2*unitH), "", blogStyle)){
				Application.OpenURL("http://h2ogames.wordpress.com/");
			}
			
			// Back Button.
			if (GUI.Button(new Rect(16*unitW, 16*unitH, 3*unitW, 2*unitH), "", backButtonStyle)){
				Application.LoadLevel("WelcomeScene");
				audioController.SendMessage("backButtonsSoundEffect");
			}
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
}
