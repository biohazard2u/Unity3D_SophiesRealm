using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 6/11/2012</para>
/// <para>Last modified: 6/11/2012 18:50</para> 
/// <para>Author: Marcos Zalacain </para>
/// FireController:   
///    -This script is to manage the fire: turning emiter On and Off + handle collider. Sound is still missing.
/// </summary>
public class FireController : MonoBehaviour {
	
	public float timeToLastFire = 6.0f;
	public float timeDelay = 0.0f;
	
	private float timeBurning = 0.0f;
	
	void Start(){
		timeBurning =+ timeDelay;
	}
	
	void Update () {
		timeBurning += Time.deltaTime;
		if (timeBurning >= timeToLastFire) {
			timeBurning = 0.0f;
			 foreach (Transform child in transform)  {
				if (child.particleEmitter.emit == true) {
					child.particleEmitter.emit = false;
					collider.enabled = false;
					if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {
						audio.Stop();
					}
				}else{
					child.particleEmitter.emit = true;
					collider.enabled = true;
					if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {
						audio.Play();
					}
				}
			}
		}
	}
	
	void OnTriggerEnter(Collider col){
		
		if(col.gameObject.tag == "Player"){
			
			col.gameObject.SendMessage("LifeDownByFire", SendMessageOptions.DontRequireReceiver);			
			//AudioSource.PlayClipAtPoint(spikesHit, collider.transform.position);			
		}
	}
}
