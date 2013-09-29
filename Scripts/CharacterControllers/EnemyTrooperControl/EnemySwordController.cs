using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 13/11/2012</para>
/// <para>Last modified: 13/11/2012 21:50</para> 
/// <para>Author: Marcos Zalacain </para>
/// EnemySwordController:   
///    -This script is to manage collisions between Sophie and EnemySword.  ......   V2.0 ==> and armour
///	   -It's conected with LifeManager.
///    -It plays sound and sends another message to throw player backwards. 
/// </summary>
public class EnemySwordController : MonoBehaviour {

	//public AudioClip swordHit;		
	
	void OnTriggerEnter(Collider col){
		
		if(col.gameObject.tag == "Player"){
			
			col.gameObject.SendMessage("LifeDown", SendMessageOptions.DontRequireReceiver);
			
			// Sword particle animation here, if any.
			
			//AudioSource.PlayClipAtPoint(swordHit, collider.transform.position);			
		}
	}
}
