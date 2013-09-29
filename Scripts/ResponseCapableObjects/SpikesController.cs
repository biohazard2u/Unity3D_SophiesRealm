using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 26/10/2012</para>
/// <para>Last modified: 26/10/2012 16:50</para> 
/// <para>Author: Marcos Zalacain </para>
/// SpikesController:   
///    -This script is to manage collisions between Sophie and: Spikes + Carnivorous plant.
///	   -It's conected with LifeManager.
///    -It plays sound and sends another message to throw player backwards. 
/// </summary>
public class SpikesController : MonoBehaviour {

	//public AudioClip spikesHit;		
	
	void OnTriggerEnter(Collider col){
		
		if(col.gameObject.tag == "Player"){
			
			col.gameObject.SendMessage("LifeDown", SendMessageOptions.DontRequireReceiver);
			
			// Spikes particle animation here, if any.
			
			//AudioSource.PlayClipAtPoint(spikesHit, collider.transform.position);			
		}
	}
}
