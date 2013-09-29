using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 5/10/2012</para>
/// <para>Last modified: 5/10/2012 18:30</para> 
/// <para>Author: Marcos Zalacain </para>
/// PlatformMoverPlus:   
///    -This script is to manage different targets. It needs to be attched to the MovingPlatform object.
/// </summary>
public class PlatformMoverPlus : MonoBehaviour {
	
	public GameObject[] targets;
	public bool moveIsOn;
	public float speed = 0.1f;
	
	private GameObject currentTarget1;
	private GameObject currentTarget2;
	private int targetPlus = 0;

	void FixedUpdate ()
	{
		if (moveIsOn) {
			
			selectTargets();
			
			// Moving between currentTarget1 & currentTarget2.
			float weight = Mathf.Cos (Time.time * speed * 2 * Mathf.PI) * 0.5f + 0.5f;
			transform.position = currentTarget1.transform.position * weight + currentTarget2.transform.position * (1 - weight);
		}
	}
	
	void selectTargets(){
		
		int numTargets = targets.Length;
		currentTarget1 = targets[0];
		currentTarget2 = targets[targetPlus +1];
		
	}
	
	void activatePlatform(){
		if (moveIsOn == false) {
			moveIsOn = true;
		}
	}
	
	void deactivatePlatform(){
		if (moveIsOn == true) {
			moveIsOn = false;
		}
	}
	
	void changeTargetPlus(int newTargetPlus){
		if (newTargetPlus == 1) {
			targetPlus++;
			speed = speed/2;
		}else{
			targetPlus = newTargetPlus;
		}
	}
}
