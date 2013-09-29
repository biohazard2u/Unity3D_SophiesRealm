using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 19/10/2012</para>
/// <para>Last modified: 19/10/2012 11:30</para> 
/// <para>Author: Marcos Zalacain </para>
/// LeverController:   
///    -This script is to manage the Levers.
/// </summary>
public class LeverController : MonoBehaviour {

	public GameObject platformToTriger;
	public bool lockOn = true;				// lockOn to true means that lever is locked.
	public bool canChangeTargets = false;
	
	private bool animationPlayed = false;

	void OnTriggerEnter(Collider col){
		
		if (!lockOn) {
			if (animationPlayed == false) {
				if(col.gameObject.tag == "Player"){			
				
					// We send a message to the platform if it is a canChangeTarget platform.
					if (canChangeTargets) {
						platformToTriger.SendMessage("changeTargetPlus", 1);
					}
					
					platformToTriger.SendMessage("activatePlatform", SendMessageOptions.DontRequireReceiver);
					
					// We play the animation.
					animation.Play();
					animationPlayed = true;
					if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {
						audio.Play();
					}
				}
			}
		}
	}
	
	void unlockLever(){
		lockOn = false;
	}
}
