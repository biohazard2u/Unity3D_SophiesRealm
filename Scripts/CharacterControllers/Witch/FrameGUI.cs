using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 10/04/2013</para>
/// <para>Last modified: 10/04/2013 19:30</para> 
/// <para>Author: Marcos Zalacain </para>
/// FrameGUI:   
///    -To manage Maevas Camera Frame and its animation. It's a GUI.
/// </summary>
public class FrameGUI : MonoBehaviour {

	private float screenWidth;
	private float screenHeight;
	private Rect pixelInsetRect;
	
	private float xGUI;
	private bool regAnimatonDirection;		// true when Initial animation; false when exiting animation.
	
	void Start () {
		
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		regAnimatonDirection = true;
		
		xGUI = 1.1f;
		
		// We use the same proportions than Maevas Camera.
		pixelInsetRect = new Rect(screenWidth * xGUI, screenHeight * 0.24f,
			screenWidth * 0.18f, screenHeight * 0.51f);
		this.guiTexture.pixelInset = pixelInsetRect;
	}
	
	void Update(){
		
		// Initial animation => right to left.
		if (regAnimatonDirection) {
			if (xGUI >= 0.83f) {
				xGUI -= 0.01f;
			}
		// Exiting animation => left to right.	
		}else{
			if (xGUI <= 1.1f) {
				xGUI += 0.01f;
			}
		}
		
		pixelInsetRect = new Rect(screenWidth * xGUI, screenHeight * 0.24f,
			screenWidth * 0.18f, screenHeight * 0.51f);
		this.guiTexture.pixelInset = pixelInsetRect;
	}
	
	// This is to change animations.
	void changeAnimationDirection(){
		regAnimatonDirection = false;
	}
}
