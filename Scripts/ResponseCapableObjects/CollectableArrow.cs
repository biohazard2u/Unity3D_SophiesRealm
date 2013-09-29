using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 01/04/2013</para>
/// <para>Last modified: 01/04/2013 11:00</para> 
/// <para>Author: Marcos Zalacain </para>
/// CollectableArrow:   
///    -This script is to control the big 3D arrow object that Sophie can collect to kill carnivorous plants.
/// </summary>
public class CollectableArrow : MonoBehaviour {

	public float rotationSpeed = 60.0f;
	public GameObject collectableParticleSystem;
	
	void Start () {
		if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {
			collectableParticleSystem.audio.playOnAwake = true;
		}else{
			collectableParticleSystem.audio.playOnAwake = false;
		}
	}
	
	void Update () {
		
		transform.Rotate(new Vector3(rotationSpeed, 0, 0) * Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider col){
		
		if(col.gameObject.tag == "Player"){			
			
			// We send a message to SophieController, as it is the script which instanciates the arrows.
			col.gameObject.SendMessage("killerPlantArrowsCollected");
			
			// We destroy this object.
			Destroy(gameObject);
			
			// Show particles system.
			Vector3 addedHight = new Vector3(0,1,0);
			Instantiate( collectableParticleSystem, col.transform.position + addedHight, Quaternion.identity );
		}
	}
}
