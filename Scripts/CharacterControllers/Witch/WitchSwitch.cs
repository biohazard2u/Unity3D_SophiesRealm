using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 05/04/2013</para>
/// <para>Last modified: 05/04/2013 18:00</para> 
/// <para>Author: Marcos Zalacain </para>
/// WitchSwitch:   
///    -This script is to decide when is Witch Maeva appearing and in which position.
/// </summary>
public class WitchSwitch : MonoBehaviour {
	
	public GameObject maevasCamera;
	public GameObject maevasCameraFrame;
	public GameObject maevasPrefab;
	public GameObject smokePrefab;
	public Transform maevasPP;
	
	public float appearingChances;		// This must be between 0.0 [inclusive] and 1.0 [inclusive]. 
	public float timeToCheckForMaeva;	// every how long do we check if Maeva needs to be shown.
	
	private bool maevaIsOn;				
	private float maevasTimer;

	void Start () {
		maevaIsOn = false;
	}
	
	void Update () {
		maevasTimer += Time.deltaTime;
		
		if (maevasTimer >= timeToCheckForMaeva) {
			checkIfMaevaCanAppear();
			maevasTimer = 0.0f;
		}
	}
		
	public void checkIfMaevaCanAppear(){
		
		if (!maevaIsOn) {
			if (Random.value <= appearingChances) {	
				// We shall start Maevas show.
				StartCoroutine("startMaevasShow",1F);	
				maevaIsOn = true;
			}
		}	
	}
	
	// Maevas show.
	IEnumerator startMaevasShow(float waitingTime){
		
		// Instanciate Maeva's camera frame as child of TheWitch game object.
		GameObject maevaCamFr = Instantiate(maevasCameraFrame) as GameObject;
		maevaCamFr.transform.parent = transform;
		yield return new WaitForSeconds(0.1F);
		// Instanciate Maeva's camera as child of TheWitch game object.
		GameObject maevaCam = Instantiate(maevasCamera) as GameObject;
		maevaCam.transform.parent = transform;
		if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {
			maevaCam.audio.Play();
		}
		// Wait for MaevasCam Frame animation to conclude.
		yield return new WaitForSeconds(1F);
		// Instanciate smoke Prefab as child of TheWitch game object.
		GameObject maevaSmo = Instantiate(smokePrefab, maevasPP.position, Quaternion.identity) as GameObject;
		maevaSmo.transform.parent = transform;
		yield return new WaitForSeconds(waitingTime);
		// Instanciate Maeva as child of TheWitch game object.
		GameObject maeva = Instantiate(maevasPrefab, maevasPP.position, Quaternion.identity) as GameObject;
		maeva.transform.parent = transform;
		
		yield return new WaitForSeconds(6F);
		maevaCamFr.SendMessage("changeAnimationDirection");
		yield return new WaitForSeconds(1F);
		Destroy(maevaCamFr);
		Destroy(maevaCam);
		
		yield return new WaitForSeconds(2F);
		maevaIsOff();
	}
	
	public void maevaIsOff(){
		maevaIsOn = false;
	}
}
