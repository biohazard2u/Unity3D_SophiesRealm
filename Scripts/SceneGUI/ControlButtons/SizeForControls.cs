using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 2.0</para>
/// <para>Date created: 21/01/2013</para>
/// <para>Last modified: 06/01/2013 18:00</para> 
/// <para>Author: Marcos Zalacain </para>
/// SizeForControls:
/// 	It is to size control buttons according to device Pixels and advance settings.
/// </summary>
public class SizeForControls : MonoBehaviour {
	
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
		
		float spreadFactor = 0;			// so button moves when size is to big.
		
		if (Globals.choosenButtonSize > 1.5f) {	
			if (this.guiTexture.pixelInset.x == -6) {							// Fire Button
				spreadFactor = (Globals.choosenButtonSize * unitW) * -1;
			}else if (this.guiTexture.pixelInset.x == -3) {						// Jump Button
				spreadFactor = (Globals.choosenButtonSize * unitW) * -0.5f;
			}else if (this.guiTexture.pixelInset.x == 3.5) {					// Right Button
				spreadFactor = Globals.choosenButtonSize * unitW * 0.5f;
			}
		}
	
		if (Globals.choosenButtonSize == 1.0f) {
			pixelInsetRect = new Rect((this.guiTexture.pixelInset.x) * unitW, (this.guiTexture.pixelInset.y) * unitH,
				1.8f*unitW, 1.8f*unitW);
		}else{
			pixelInsetRect = new Rect(((this.guiTexture.pixelInset.x) * unitW) + spreadFactor, (this.guiTexture.pixelInset.y) * unitH,
				(1.8f*unitW) * Globals.choosenButtonSize, (1.8f*unitW) * Globals.choosenButtonSize);
		}
		this.guiTexture.pixelInset = pixelInsetRect;		
	}
}
