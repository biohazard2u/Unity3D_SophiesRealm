using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 04/01/2013</para>
/// <para>Last modified: 06/01/2013 18:00</para> 
/// <para>Author: Marcos Zalacain </para>
/// SizeForHudItems:
/// 	It is to size HUD items related to screen pixels/device Type.
/// </summary>
public class SizeForHudItems : MonoBehaviour {

	private float screenWidth;
	private float screenHeight;
	private float unitW, unitH;
	private Rect pixelInsetRect;
	
	void Start () {
		
		// Size related.
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		unitW = screenWidth/20;
		unitH = screenHeight/20;

		pixelInsetRect = new Rect((this.guiTexture.pixelInset.x) * unitW, (this.guiTexture.pixelInset.y) * unitH,
			2.5f*unitW, 2.5f*unitW);
		this.guiTexture.pixelInset = pixelInsetRect;		
	}
}
