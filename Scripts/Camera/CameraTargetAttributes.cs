using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 10/8/2012</para>
/// <para>Last modified: 14/11/2012 15:10</para> 
/// <para>Author: Marcos Zalacain </para>
/// CameraTargetAttributes:   
///    -Contains the camera target attributes.
///    -This script goes on any GameObject in your scene that you will track with the camera.
///    -It'll help customize the camera tracking to your specific object to polish your game.
/// </summary>
public class CameraTargetAttributes : MonoBehaviour
{
	// See the GetGoalPosition () function in CameraScrolling.cs for an explanation of these variables.
	public float widthOffset = Globals.widthOffsetG;
	public float heightOffset = Globals.heightOffsetG;
	public float distanceModifier = 0.4f;
	public float velocityLookAhead = 0.15f;
	public Vector2 maxLookAhead = new Vector2(3.0f, 3.0f);
}
