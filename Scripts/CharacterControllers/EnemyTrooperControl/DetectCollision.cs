using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 13/11/2012</para>
/// <para>Last modified: 13/11/2012 19:00</para> 
/// <para>Author: Marcos Zalacain </para>
/// DetectCollision:   
///    -To detect collisions on child and send them to parent script.
/// </summary>
public class DetectCollision : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		
		// Pass it along to the parent:
		transform.parent.GetComponent<EnemyTrooperController>().attackTrigger(col, true);
    }
	
	void OnTriggerExit(Collider col) {
		
		// Pass it along to the parent:
		transform.parent.GetComponent<EnemyTrooperController>().attackTrigger(col, false);
    }
}
