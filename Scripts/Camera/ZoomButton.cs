using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 12/03/2013</para>
/// <para>Last modified: 13/03/2013 17:40</para> 
/// <para>Author: Marcos Zalacain </para>
/// ZoomButton:    
///    -ZoomOut button controller.
/// </summary>
public class ZoomButton : MonoBehaviour {
	
	public GameObject mainCameraObject;	
	public Texture2D textureNormal;
	public Texture2D texturePressed;
	
	private float screenWidth;
	private float screenHeight;
	private float unitW, unitH;
	private Rect pixelInsetRect;
	
	private CameraScrolling scriptComponent;
	
	void Start () {
		
		// Size related.
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		unitW = screenWidth/20;
		unitH = screenHeight/20;

		pixelInsetRect = new Rect((this.guiTexture.pixelInset.x) * unitW, (this.guiTexture.pixelInset.y) * unitH,
			2.5f*unitW, 2.5f*unitW);
		this.guiTexture.pixelInset = pixelInsetRect;	
		
		scriptComponent =  mainCameraObject.GetComponent<CameraScrolling>();
	}
	
	// 1st aproach:
	private int countedTouches;
	void Update (){
		countedTouches = Input.touchCount;
		for (int i = 0; i < countedTouches; i++) {
			Touch touch = Input.GetTouch(i);
			if (this.guiTexture.HitTest( touch.position )) {
				
				if (touch.phase == TouchPhase.Began) {
					scriptComponent.zoomOutNow();
					this.guiTexture.texture = texturePressed;
				}
				if (touch.phase == TouchPhase.Ended) {
					scriptComponent.zoomNormalNow();
					this.guiTexture.texture = textureNormal;
				}
			}
		}
	}

	/*
	// 2nd aproach
	public GUIStyle zoomButtonStyle;
	void OnGUI() {
		if (GUI.Button(new Rect(16*unitW, 16*unitH, 3*unitW, 2*unitH), "", zoomButtonStyle)){
			Debug.Log("Do the zoom out! - 2nd aproach");
		}
	}
	*/
}
