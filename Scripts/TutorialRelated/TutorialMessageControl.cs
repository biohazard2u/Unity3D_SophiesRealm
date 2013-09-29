using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 29/11/2012</para>
/// <para>Last modified: 29/11/2012 18:20</para> 
/// <para>Author: Marcos Zalacain </para>
/// TutorialMessageControl:   
///    -This script will remove this tutorial message when deactivation trigger is set on by player.
/// </summary>
public class TutorialMessageControl : MonoBehaviour {
	
	// Method call on Child to detect end of tutorial Message trigger.
	public void deactivateTrigger(Collider col, bool b){
		
		if (col.gameObject.tag == "Player") {
			if (b) {
				Destroy(gameObject);
			}
		}		
	}
}
