using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 5/10/2012</para>
/// <para>Last modified: 5/10/2012 18:30</para> 
/// <para>Author: Marcos Zalacain </para>
/// PlatformMover:   
///    -This script is to move the MovingPlatforms. It needs to be attched to the MovingPlatform object.
/// </summary>
public class PlatformMover : MonoBehaviour
{
	public GameObject targetA;
	public GameObject targetB;
	public float speed = 0.1f;

	void FixedUpdate ()
	{
		float weight = Mathf.Cos (Time.time * speed * 2 * Mathf.PI) * 0.5f + 0.5f;
		transform.position = targetA.transform.position * weight + targetB.transform.position * (1 - weight);
	}	
}
