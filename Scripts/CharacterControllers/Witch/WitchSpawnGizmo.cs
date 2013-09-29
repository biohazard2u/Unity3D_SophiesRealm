using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 10/04/2013</para>
/// <para>Last modified: 10/04/2013 13:30</para> 
/// <para>Author: Marcos Zalacain </para>
/// WitchSpawnGizmo:   
///    -We use this Gizmo to show Maeva's spawn position.
/// </summary>
public class WitchSpawnGizmo : MonoBehaviour {

	void OnDrawGizmos ()
	{
		Gizmos.DrawIcon (transform.position, "MaevaPPIcon.tif");
	}
}
