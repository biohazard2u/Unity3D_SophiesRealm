using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 8/11/2012</para>
/// <para>Last modified: 8/11/2012 19:00</para> 
/// <para>Author: Marcos Zalacain </para>
/// PizzaSliceController:   
///    -This script is to manage the 3D Pizza Slices. It goes attached to the PizzaSlice object.
/// </summary>
public class PizzaSliceController : MonoBehaviour {
	
	public float rotationSpeed = 80.0f;
	public GameObject pizzaParticleSystem;
	
	void Start () {
		if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {
			pizzaParticleSystem.audio.playOnAwake = true;
		}else{
			pizzaParticleSystem.audio.playOnAwake = false;
		}
	}
	
	void Update () {
		transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider col){
		
		// Check if we don't have all 4 lifes first.
		if(LifeManager.life < 4){
			
			if(col.gameObject.tag == "Player"){
				
				Destroy(gameObject);
				
				// Show particles system.
				Vector3 addedHight = new Vector3(0,1,0);
				Instantiate( pizzaParticleSystem, col.transform.position + addedHight, Quaternion.identity );
				
				// Send message to LifeManager for Life Up
				col.gameObject.SendMessage("LifeUp", SendMessageOptions.DontRequireReceiver);
			}
		}	
	}
}
