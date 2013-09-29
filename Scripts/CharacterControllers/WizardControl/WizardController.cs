using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 14/11/2012</para>
/// <para>Last modified: 20/01/2013 14:40</para> 
/// <para>Author: Marcos Zalacain </para>
/// WizardController:   
///    -To control the wizard.
/// </summary>
public class WizardController : MonoBehaviour {
	
	// EndOfStage 
	public GameObject endStagePrefab;		// This is the end of stage GUI prefab.	
	public GameObject mainCameraObject;		// This reference is to launch the camera animation at the end of the stage.
	
	// Speed for all animations.
	public float animationSpeeds = 45f;
	
	// Audio sound effects.
	public AudioClip stageCleared1;
	public AudioClip stageCleared2;
	public AudioClip stageUncompleted;
	
	// passStage is true if Sophie has all collectables.
	private bool passStage = false;
		
	void Start(){

		// Animations states here:
		AnimationState happy = animation["MagicLightSpell"];
		happy.speed *= animationSpeeds * Time.deltaTime;
		happy.wrapMode = WrapMode.Once;
		
		AnimationState sadOne = animation["StaffBlock"];
		sadOne.speed *= animationSpeeds * Time.deltaTime;
		sadOne.wrapMode = WrapMode.Once;
		
		AnimationState sadTwo = animation["StaffEarthQuake"];
		sadTwo.speed *= animationSpeeds * Time.deltaTime;
		sadTwo.wrapMode = WrapMode.Once;
		
		AnimationState idle = animation["IdleWithoutStaff"];
		idle.speed *= animationSpeeds * Time.deltaTime;
		idle.wrapMode = WrapMode.Loop;
	}
	
	//	When player enters wizards trigger.
	void OnTriggerEnter(Collider col) {
		
		if(col.gameObject.tag == "Player"){	
			
			if (passStage) {
				// IF WE ARE ON FINAL STAGE/LEVEL, 
				if (Application.loadedLevelName == "Level81") {
					// we do NOT update Globals.lastCompletedLevel.
				}else{
					// Set new Globals value & playerPreffs if we've past a new level.
					string currLevelStr = Application.loadedLevelName.Remove(0,5);
					int currLevelInt = System.Convert.ToInt32(currLevelStr);
					if (currLevelInt == Globals.lastCompletedLevel + 1) {
						Globals.lastCompletedLevel = Globals.lastCompletedLevel + 1;
						PlayerPrefs.SetInt("completedLevelAchieved", Globals.lastCompletedLevel);
					} 
				}	
				
				// Animate wizard.
				animation.CrossFade ("MagicLightSpell");
				// Call CameraScrolling.cs to simulate animation.
				CameraScrolling scriptComponent =  mainCameraObject.GetComponent<CameraScrolling>();
				scriptComponent.StartCoroutine("animationEndOfStage", 10.0f);				
				// Launch EndOfStage prefab.
				Instantiate(endStagePrefab);
				// Play audio
				if (Random.value <= 0.5) {
					playSoundEffect(stageCleared1);
				}else{
					playSoundEffect(stageCleared2);
				}
				
			}else{
				StartCoroutine(sadAnimations(1f));
				playSoundEffect(stageUncompleted);
			}
		}
    }
	
	// Coroutine for all sad animations.
	IEnumerator sadAnimations(float waitingTime){
		animation.CrossFade ("StaffBlock");
		yield return new WaitForSeconds(waitingTime);
		animation.CrossFade ("StaffEarthQuake");
		yield return new WaitForSeconds(waitingTime);
		animation.CrossFade ("IdleWithoutStaff");
	}
	
	// Public method sent on CollectablesManager script when Sophie gets all collectables.
	public void allCollected(bool collectionDone){
		
		if (collectionDone) {
			passStage = true;
		}
	}
	
	// This is to play sound effect.
	void playSoundEffect(AudioClip aClip){
		if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {
			audio.PlayOneShot(aClip);
		}		
	}
}
