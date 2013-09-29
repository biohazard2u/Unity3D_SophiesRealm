using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 5/10/2012</para>
/// <para>Last modified: 17/1/2013 01:30</para> 
/// <para>Author: Marcos Zalacain </para>
/// MovingPlatformEffects:   
///    -This script compares the previous position with the current position and if it's moving upwards the particle emitters turn on.
///    -It needs to be attched to the MovingPlatform object.
/// </summary>
public class MovingPlatformEffects : MonoBehaviour
{
	// SOUND HERE.
	public bool verticalPlatform;
	public AudioClip soundEffect;
	
	private bool goingUp, goingRight;
	
	//  We are going to use these later to calculate the current velocity.
	private Vector3 oldPosition;
	private Vector3 currentVelocity;

	void Start ()
	{
		// Grabs the initial position of the platform.
		oldPosition = transform.position;
	}

	void Update ()
	{
		if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {
			// Vertical Platform
			if (verticalPlatform) {						
				if (currentVelocity.y > 0) {
					goingUp = true;
				}
				if (currentVelocity.y < 0) {
					goingUp = false;
				}
			// Horizontal Platform	
			}else{
				if (currentVelocity.x > 0) {
					goingRight = true;
				}
				if (currentVelocity.x < 0) {
					goingRight = false;
				}
			}
		}
	}

	// LateUpdate() function instead of Update() is to stop the particle emitter from reacting one frame late.
	void LateUpdate ()
	{
		if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {
			
			currentVelocity = transform.position - oldPosition;
			oldPosition = transform.position;
			
			// Vertical Platform
			if (verticalPlatform) {	
				if (goingUp && currentVelocity.y < 0) {
					goingUp = false;
					audio.PlayOneShot(soundEffect);
				}
				if (!goingUp && currentVelocity.y > 0) {
					goingUp = true;
					audio.PlayOneShot(soundEffect);
				}
			// Horizontal Platform
			}else{
				if (goingRight && currentVelocity.x < 0) {
					goingRight = false;
					audio.PlayOneShot(soundEffect);
				}
				if (!goingRight && currentVelocity.x > 0) {
					goingRight = true;
					audio.PlayOneShot(soundEffect);
				}
			}
		}
	}
}
