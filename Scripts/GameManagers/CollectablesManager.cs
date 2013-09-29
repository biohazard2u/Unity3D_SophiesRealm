using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 8/11/2012</para>
/// <para>Last modified: 9/11/2012 13:00</para> 
/// <para>Author: Marcos Zalacain </para>
/// CollectablesManager: Recives message from CollectableController when collision detected.
/// 					 Kees track of rewardsCollected/CollectablesCollected and changing HUD.
/// 					 Also, it plays sound and instanciates prefab OrbCollected. 
/// </summary>
public class CollectablesManager : MonoBehaviour {

	public static int collectablesNeeded = 5;			// Do we need this to be static???

	public GUIText collectablesText;	
	public GUITexture collectablesTexture;
	public Font textFontSML;
	public Font textFontSMH;
	public Font textFontN7;
	public Font textFontN10;
	/////public AudioClip collectablesPickedUpSound;
	/////public GameObject collectablesPickedUpParticle;
	
	private Font collectablesTextFont;
	private int collectablesCollected;	

	void Start () {
		
		collectablesCollected = 0;

		// We set the font acording to deviceType here.
		if (Globals.deviceType == Globals.SmartPhoneL) {
			collectablesTextFont = textFontSML;
		}else if (Globals.deviceType == Globals.SmartPhoneH) {
			collectablesTextFont = textFontSMH;	
		}else if (Globals.deviceType == Globals.N7) {
			collectablesTextFont = textFontN7;
		}else{
			collectablesTextFont = textFontN10;
		}
		
		collectablesText.font = collectablesTextFont;
		collectablesText.material.color = Color.black;
		collectablesText.text = collectablesCollected + "/" + collectablesNeeded;	
	}
		
	public void collectablesUp(){
		
		if (collectablesCollected < collectablesNeeded) {
			
			collectablesCollected++;
			collectablesText.text = collectablesCollected + "/" + collectablesNeeded;	
			
		}
		
		if (collectablesCollected == collectablesNeeded) {
			GameObject.Find("TheWizard").SendMessage("allCollected", true);
			StartCoroutine(allCollectedAnima(6f));
			
			// We also destroy the witch so she appears no more after collecting all mushrooms.
			GameObject witch = GameObject.Find("TheWitch");
			Destroy(witch);
		}
	}
	
	// Coroutine for all collectables collected animation.
	IEnumerator allCollectedAnima(float waitingTime){
		collectablesTexture.animation.Play();
		yield return new WaitForSeconds(waitingTime);
		collectablesTexture.animation.Play();
		yield return new WaitForSeconds(waitingTime);
		collectablesTexture.animation.Play();
	}
}
