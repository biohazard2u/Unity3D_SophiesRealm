using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 8/11/2012</para>
/// <para>Last modified: 9/11/2012 13:00</para> 
/// <para>Author: Marcos Zalacain </para>
/// CollectableController: Is in charge of collisions with Sophie. It sends a message to CollectablesManager to change HUD,
/// 		play a sound and show particles. Also, destroys gameObject. 
/// Improvements:
/// 		
/// </summary>
public class CollectableController : MonoBehaviour {
	
	public float rotationSpeed = 60.0f;
	public GameObject collectableParticleSystem;
	public bool ShitRotation = false;
	
	void Start () {
		if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {
			collectableParticleSystem.audio.playOnAwake = true;
		}else{
			collectableParticleSystem.audio.playOnAwake = false;
		}
	}
	
	void Update () {
		
		if (ShitRotation) {
			transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
		}else{		
			transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
		}
	}
	
	void OnTriggerEnter(Collider col){
		
		if(col.gameObject.tag == "Player"){			
			
			// We send a message to CollectablesManager.
			col.gameObject.SendMessage("collectablesUp");
			
			// We destroy this object.
			Destroy(gameObject);
			
			// Show particles system.
			Vector3 addedHight = new Vector3(0,1,0);
			Instantiate( collectableParticleSystem, col.transform.position + addedHight, Quaternion.identity );
		}
	}
}
