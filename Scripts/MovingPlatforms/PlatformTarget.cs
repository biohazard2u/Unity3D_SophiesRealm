using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 5/10/2012</para>
/// <para>Last modified: 5/10/2012 18:30</para> 
/// <para>Author: Marcos Zalacain </para>
/// PlatformTarget:   
///    -To draw a gizmo in the scene view, so it can be found...
/// </summary>
public class PlatformTarget : MonoBehaviour
{
	void OnDrawGizmos ()
	{
		Gizmos.DrawIcon (transform.position, "platformIcon.tif");
	}
	
	void Start() {
	}
	
}
