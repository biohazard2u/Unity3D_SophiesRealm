using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 29/01/2013</para>
/// <para>Last modified: 29/01/2013 17:30</para> 
/// <para>Author: Marcos Zalacain </para>
/// SSBackground:   
///    -This script sets the texture to the backgroud according to level.
/// </summary>
public class SSBackground : MonoBehaviour {

	public Texture Levels1;
	public Texture Levels2;
	public Texture Levels3;
	public Texture Levels4;
	public Texture Levels5;
	public Texture Levels6;
	public Texture Levels7;
	public Texture Levels8;
	public Texture Levels9;
	//public Texture Levels10;	
	
	private float screenWidth;
	private float screenHeight;
	private float unitW, unitH;
	private Texture textureMaterialRightLevel;
	
	private float currentPosition;
	private float finalPosition;
	
	void Start () {
		
		// Size related.
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		unitW = screenWidth/20;
		unitH = screenHeight/20;
				
		// Items positions.
		currentPosition = 0.0f;
		finalPosition = -screenWidth;
		
		textureMaterialRightLevel = Levels1;				// setting 1 to initialize only.
		
		switch (Globals.levelToLaunch) {
			case 1:											// These are the levels/stages that hace a story.
				textureMaterialRightLevel = Levels1;
				break;
			case 10:
				textureMaterialRightLevel = Levels2;
				break;
			case 19:
				textureMaterialRightLevel = Levels3;
				break;
			case 28:
				textureMaterialRightLevel = Levels4;
				break;
			case 37:
				textureMaterialRightLevel = Levels5;
				break;
			case 46:
				textureMaterialRightLevel = Levels6;
				break;
			case 55:
				textureMaterialRightLevel = Levels7;
				break;
			case 64:
				textureMaterialRightLevel = Levels8;
				break;
			case 73:
				textureMaterialRightLevel = Levels9;
				break;
			//case 82:
			//	textureMaterialRightLevel = Levels10;
			//	break;		
		}
		
		this.guiTexture.pixelInset = new Rect(currentPosition, 0, 2*screenWidth, screenHeight);
		this.guiTexture.texture = textureMaterialRightLevel;
		
		// Keep background while nextScene fully loaded. We Will destroy this when GUI object is loaded.
		DontDestroyOnLoad(transform.gameObject);
	}
	
	void Update () {
		
		// If Nexus 10
		if (Screen.width >= 1300) {
			if (currentPosition > finalPosition + 2*unitW) {
				currentPosition = currentPosition - screenWidth/450;
			}
			this.guiTexture.pixelInset = new Rect(currentPosition + unitW, unitW, 2*(screenWidth - 2*unitW), screenHeight- 4*unitH);
		}
		// If Nexus 7
		else if(screenWidth < 1300 && screenWidth > 1000){
			if (currentPosition > finalPosition) {
				currentPosition = currentPosition - screenWidth/350;
			}
			this.guiTexture.pixelInset = new Rect(currentPosition, 0, 2*screenWidth, screenHeight);
		}
		// Any other device
		else{
			if (Time.timeSinceLevelLoad > 0.5f) {
				if (currentPosition > finalPosition) {
					currentPosition = currentPosition - screenWidth/400;
				}
			}
			this.guiTexture.pixelInset = new Rect(currentPosition, 0, 2*screenWidth, screenHeight);
		}
	}
}
