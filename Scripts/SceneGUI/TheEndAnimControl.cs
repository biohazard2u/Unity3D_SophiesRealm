using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 19/04/2013</para>
/// <para>Last modified: 19/04/2013 18:50</para> 
/// <para>Author: Marcos Zalacain </para>
/// TheEndAnimControl:
/// 	-To control the final animation at TheEnd Scene.
/// </summary>
public class TheEndAnimControl : MonoBehaviour {
	
	// GameObjects
	public GameObject maeva;
	//public GameObject elric;
	
	// Conversation
	public GUIStyle labelStyle;
	public Font labelFontSML;
	public Font labelFontSMH;
	public Font labelFontN7;
	public Font labelFontN10;	
	private int longitud1, longitud2 = 0;	
	private float screenWidth, screenHeight;
	private float unitW, unitH;
	private string text1 = "";
	private string text2 = "";
	private bool showMessage1 = false;
	private bool showMessage2 = false;
	
	private bool canLaunchCredits = false;
	
	// Camera animation
	private bool animStoped = false;	
	private AnimationState theEnd;
	
	void Start () {
	
		// Size related.
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		unitW = screenWidth/20;
		unitH = screenHeight/20;
		
		// We set the background and styles acording to deviceType here.
		if (Globals.deviceType == Globals.SmartPhoneL) {
			labelStyle.font = labelFontSML;
			labelStyle.fixedHeight = screenHeight/2;
		}else if (Globals.deviceType == Globals.SmartPhoneH) {
			labelStyle.font = labelFontSMH;
			labelStyle.fixedHeight = screenHeight/4;
		}else if (Globals.deviceType == Globals.N7) {
			labelStyle.font = labelFontN7;
			labelStyle.fixedHeight = screenHeight/3;
		}else{
			labelStyle.font = labelFontN10;
			labelStyle.fixedHeight = screenHeight/3;
		}
		
		// Set label strings.
		if(Globals.choosenLanguage == Globals.English){
			text1 = "Your mushrooms, she is all yours";
			text2 = "Good job! Now she dies.";
		}else{
			text1 = "Tus setas. Es toda tuya.";
			text2 = "Buen trabajo! Ahora debe morir.";
		}
		
		// camera animations, so we can pause it.
		theEnd = animation["TheEnd"];
		// Maevas Animation State.
		AnimationState maevaDies = maeva.animation["Dying"];
		maevaDies.layer = 1;
		maevaDies.wrapMode = WrapMode.Once;
	}
	
	void Update () {
	
		// Check for input touches.
		if ( ( Input.touchCount > 0 ) || Input.GetKeyDown ( KeyCode.Space ) ) {
			if (animStoped) {
				if (showMessage1) {
					showMessage1 = false;
					theEnd.speed = 1;
				}
				if (showMessage2) {
					showMessage2 = false;
					theEnd.speed = 1;
				}
			}
			// To launch credits.
			if (canLaunchCredits) {
				Application.LoadLevel("MoreScene");
			}
		}
		
		// Update messages length so they can animate.
		if (showMessage1) {
			if ( longitud1 < text1.Length ) {
					longitud1++;
			}
		}
		if (showMessage2) {
			if ( longitud2 < text2.Length ) {
					longitud2++;
			}
		}
	}
	
	void OnGUI(){
		
		// Show Messages when needed.
		if (showMessage1) {
			GUI.Label(new Rect(1.5f*unitW, 14.5f*unitH, 17*unitW, 2*unitH), text1.Substring ( 0 , longitud1 ), labelStyle );	
		}
		if (showMessage2) {
			GUI.Label(new Rect(1.5f*unitW, 14.5f*unitH, 17*unitW, 2*unitH), text2.Substring ( 0 , longitud2 ), labelStyle );	
		}
	}
		
	// Animation functions. To be call in the camera animation named 'TheEnd'.
	void StopAnimation(){
		theEnd.speed = 0;
		animStoped = true;
		showMessage1 = true;
	}
	
	void StopAnimation2(){
		theEnd.speed = 0;
		animStoped = true;
		showMessage2 = true;
	}
	
	void maevaDies(){
		maeva.animation.Stop();
		maeva.animation.Blend("Dying");
		playSoundEffect();
		// Launch Credits.
		StartCoroutine(launchCredits(4f));
	}
	
	IEnumerator launchCredits(float waitingTime){
		// player can launch credits by touching screen after 1f.
		yield return new WaitForSeconds(1f);
		canLaunchCredits = true;
		// if player doesnt touch, we launch the credits anyway.
		yield return new WaitForSeconds(waitingTime);
		Application.LoadLevel("MoreScene");
	}
	
	// This is to play sound effect.
	void playSoundEffect(){
		if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {
			this.audio.Play();
		}		
	}
}
