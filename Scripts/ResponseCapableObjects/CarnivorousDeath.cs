using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 01/04/2013</para>
/// <para>Last modified: 01/04/2013 16:00</para> 
/// <para>Author: Marcos Zalacain </para>
/// CarnivorousDeath:   
///    -This script is to control the new Carnivorous Plants, these ones can now Die if collision with arrows tagged PlantKiller.
/// </summary>
public class CarnivorousDeath : MonoBehaviour {
	
	public GameObject dyingParticlesSystem;
	//public AudioClip dyingPlantSound;
		
	void OnTriggerStay(Collider col) {

		if(col.gameObject.tag == "PlantKiller"){
			
			// We'll disable all trigger colliders and so on...
			this.transform.parent.collider.enabled = false;
			Destroy(this.transform.parent.GetComponent<CarnivorousRotate>());
			// Release the particle system
			Instantiate(dyingParticlesSystem, this.transform.position, Quaternion.identity);
			// Stop any previous animations and start the plant dying animation.
			this.transform.parent.animation.Stop();
			this.transform.parent.animation.Play("CarnivorousDeath");
			// Destroy the whole object Plant after .45 sec.
			StartCoroutine(destroyPlant(0.45f));
		}
	}
	
	IEnumerator destroyPlant(float waitingTime){
		yield return new WaitForSeconds(waitingTime);
		Destroy(this.transform.parent.gameObject);
	}
}
