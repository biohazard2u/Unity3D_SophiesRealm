using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 12/11/2012</para>
/// <para>Last modified: 12/11/2012 22:30</para> 
/// <para>Author: Marcos Zalacain </para>
/// StartingPoint:   
///    -This script must be attached to a game object to tell Unity where the enemy target pooint is.
/// </summary>
public class EnemyTargetPoint : MonoBehaviour {

	void OnDrawGizmos ()
	{
		Gizmos.DrawIcon (transform.position, "EnemySoldierIcon.tif");
	}
}
