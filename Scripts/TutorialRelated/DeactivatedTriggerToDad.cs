using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 29/11/2012</para>
/// <para>Last modified: 29/11/2012 18:00</para> 
/// <para>Author: Marcos Zalacain </para>
/// DeactivatedTriggerToDad:   
///    -To detect collisions on child and send them to parent script (TutorialMessageControl).
/// </summary>
public class DeactivatedTriggerToDad : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		
		// Pass it along to the parent:
		transform.parent.GetComponent<TutorialMessageControl>().deactivateTrigger(col, true);
    }
}
