using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 15/03/2013</para>
/// <para>Last modified: 16/03/2013 17:00</para> 
/// <para>Author: Marcos Zalacain </para>
/// LeverTimerController:   
///    -This script is to manage the Levers with timers. 
/// </summary>
public class LeverTimerController : MonoBehaviour {
	
	public GameObject platformToTriger;
	public GameObject mainCam;				// We need a main camera reference to place the sandTimer.
	public Transform sandTimerPrefab;
	
	public float timeAmount = 10.0f;		
	public bool lockOn = false;				
	public bool canChangeTargets = false;	
	
	private bool animationPlayed = false;	
	private bool startTimer = false;
	private float timeRemaining;
	
	private Transform sandTimerClone;		// Clone of the prefab.
	
	void Start () {
		timeRemaining = timeAmount;
	}
	
	void Update () {
		
		if (startTimer) {
						
			if (timeRemaining > 0) {
				timeRemaining = timeRemaining - Time.deltaTime;
				
			}
			// TIME IS OVER
			else{		
				// We destroy the sandTimerClone.
				Destroy(sandTimerClone.gameObject);
				// We raise back the lever.
				leverBackUp();
				// We stop the platform.
				platformToTriger.SendMessage("deactivatePlatform", SendMessageOptions.DontRequireReceiver);
				// We allow the animationDown to play again.
				animationPlayed = false;				
				// We reset the time.
				timeRemaining = timeAmount;
				// We stop timing.
				startTimer = false;
			}
		}
	}		

	void OnTriggerEnter(Collider col){
		
		if (!lockOn) {
			if (animationPlayed == false) {
				if(col.gameObject.tag == "Player"){			
				
					// We start the timer.
					startTimer = true;
					
					// We instanciate the sandTimerPrefab and store it as sandTimerClone.
					Vector3 posTimerClone = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y - 1.6f, -2);
					sandTimerClone = Instantiate(sandTimerPrefab, posTimerClone, Quaternion.identity) as Transform;
					sandTimerClone.name = "timerClon";
					sandTimerClone.transform.parent = mainCam.transform;	// This makes it son of mainCam.
    				sandTimerClone.transform.localScale = Vector3.one;
					
					// We send a message to the platform if it is a canChangeTarget platform.
					if (canChangeTargets) {
						platformToTriger.SendMessage("changeTargetPlus", 1);
					}
					
					platformToTriger.SendMessage("activatePlatform", SendMessageOptions.DontRequireReceiver);
					
					// We play the animation.
					animation["LeverDown"].speed = 1;
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
	
	// To raise back the lever. animation.Rewind() works only while animation is being played, so I use this work around.
	void leverBackUp(){
		animation["LeverDown"].normalizedTime = 1f;
		animation["LeverDown"].speed = -1;
		animation.Play(); 
		if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {
			audio.Play();
		}
	}
}
