using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 26/10/2012</para>
/// <para>Last modified: 26/10/2012 19:00</para> 
/// <para>Author: Marcos Zalacain </para>
/// SpikesController:   
///    -This script is to manage Sophie's Life. It's conected with SpikesController,...
///    -Kees track of live and it changes the HUD.
/// ToDO: 
///    -Script to start new game/match when we die.
///    -Audio.
///    -Intermitent alpha for Sophie or nasty particles go on when Shophie gets hurt.
/// ToConsider:
///    -Do we need the public variable life to be static???
/// </summary>
public class LifeManager : MonoBehaviour {

	public static int life;
	
	//public AudioClip lifeGained;
	//public AudioClip lifeLost;
	public Texture2D[] lifeHudTextures;
	public GUITexture lifeHudGUITexture;	
	public GameObject reStartLevelPrefab;
	
	private bool canTakeLife = true;		// this variable is to avoid to repeatly kill our player.
	private GameObject[] enemyTrooper;		// list with all gameObjects tag as Enemy.
	
	// Start.
	void Start () {
	
		life = 4;
		lifeHudGUITexture.texture = lifeHudTextures[life];
		enemyTrooper = GameObject.FindGameObjectsWithTag("Enemy");
	}				  		
	
	// We loose Life.
	public void LifeDown(){

		// We check here if we have just recently collided so we don't kill Sophie on consecutive Spikes.
		if(canTakeLife){
			life--;
			lifeHudGUITexture.texture = lifeHudTextures[life];
			if (life > 0) {
				StartCoroutine(DelayController(0.5f));
				gameObject.SendMessage("ThrowPlayerOff", SendMessageOptions.DontRequireReceiver);
			}else{
				StartCoroutine(DelayController(4.0f));
				gameObject.SendMessage("DidDie", SendMessageOptions.DontRequireReceiver);
				// Show Re-Start Level Message.
				StartCoroutine(showReStartLevelMessage(2.0f));
			}
		}
	}
	
	// We loose Life by Fire.
	public void LifeDownByFire(){
		if(canTakeLife){
			life--;
			lifeHudGUITexture.texture = lifeHudTextures[life];
			if (life > 0) {
				StartCoroutine(DelayController(0.0f));
			}else{
				StartCoroutine(DelayController(4.0f));
				gameObject.SendMessage("DidDie", SendMessageOptions.DontRequireReceiver);
				// Show Re-Start Level Message.
				StartCoroutine(showReStartLevelMessage(2.0f));
			}
		}
	}
	
	// We gain Life.
	public void LifeUp(){
		life++;
		lifeHudGUITexture.texture = lifeHudTextures[life];
	}
	
	// To Cancel controller for a few seconds.
	IEnumerator DelayController(float waitingTime){
		
		SophieController.canControl = false;
		//SophieControllerKeyboard.canControl = false;
		canTakeLife = false;
		if (life > 0) {
			gameObject.SendMessage("DidLifeDown", SendMessageOptions.DontRequireReceiver);	
			StartCoroutine(makeSophieDieable(2.0f));
		}		
		yield return new WaitForSeconds(waitingTime);
		if (life > 0) {
			SophieController.canControl = true;
			//SophieControllerKeyboard.canControl = true;
		}
		//canTakeLife = true;
	}
	
	// To make Sophie able to Die again after blinking animation.
	IEnumerator makeSophieDieable(float waitingTime){
		
		yield return new WaitForSeconds(waitingTime);
		canTakeLifeToTrue();
	}
	
	// To show the re-start level message.
	IEnumerator showReStartLevelMessage(float waitingTime){
		
		SophieController.canControl = false;
		//SophieControllerKeyboard.canControl = false;
		
		// To refrain enemy from attacking Sophie when she is dead.
		foreach (GameObject enemy in enemyTrooper) {
			if (enemy != null) {
				enemy.SendMessage("sophieIsSuperDead",SendMessageOptions.DontRequireReceiver);
			}
		}
		yield return new WaitForSeconds(waitingTime);
		Instantiate(reStartLevelPrefab);
	}
	
	// So player can get hurt again.
	void canTakeLifeToTrue(){				
		if (!canTakeLife) {
			canTakeLife = true;
		}
	}
}
