using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 1/10/2012</para>
/// <para>Last modified: 1/10/2012 17:30</para> 
/// <para>Author: Marcos Zalacain </para>
/// StartingPoint:   
///    -This script must be attached to a game object to tell Unity where the player starts in the level.
///    -We use this Gizmo for failAnswerPositions also.
/// </summary>
public class StartingPoint : MonoBehaviour {

	void OnDrawGizmos ()
	{
		Gizmos.DrawIcon (transform.position, "Player Icon.tif");
	}
}
