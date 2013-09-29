using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 25/03/2013</para>
/// <para>Last modified: 25/03/2013 19:00</para> 
/// <para>Author: Marcos Zalacain </para>
/// DetectCollisionSuper:   
///    -To detect collisions on child and send them to parent script. For SuperTroopers
///    -SphereZone is the sphereZone/smartZone area trigger.
///    -Otherwise (no SphereZone) is the attacking zone area trigger.
/// </summary>
public class DetectCollisionSuper : MonoBehaviour {
	
	public bool sphereZone;

	void OnTriggerEnter(Collider col) {
		
		// Pass it along to the parent:
		if (!sphereZone) {
			transform.parent.GetComponent<EnemySuperTrooperController>().attackTrigger(col, true);
		}else{
			if (col.gameObject.tag == "Player") {
				transform.parent.GetComponent<EnemySuperTrooperController>().smartZone = true;
			}
		}
    }
	
	void OnTriggerExit(Collider col) {
		
		// Pass it along to the parent:
		if (!sphereZone) {
			transform.parent.GetComponent<EnemySuperTrooperController>().attackTrigger(col, false);
		}else{
			if (col.gameObject.tag == "Player") {
				transform.parent.GetComponent<EnemySuperTrooperController>().smartZone = false;
			}
		}
    }
}
