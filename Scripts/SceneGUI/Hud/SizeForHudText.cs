using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 05/01/2013</para>
/// <para>Last modified: 05/01/2013 18:00</para> 
/// <para>Author: Marcos Zalacain </para>
/// SizeForHudItems:
/// 	It is to position HUD text related to screen pixels/device Type.
/// </summary>
public class SizeForHudText : MonoBehaviour {

	private float screenWidth;
	private float screenHeight;
	private float unitW, unitH;
	private Vector2 pixelOffsetRect;
	
	void Start () {
		
		// Size related.
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		unitW = screenWidth/20;
		unitH = screenHeight/20;

		pixelOffsetRect = new Vector2((this.guiText.pixelOffset.x) * unitW, (this.guiText.pixelOffset.y) * unitH);
		this.guiText.pixelOffset = pixelOffsetRect;
	}
}
