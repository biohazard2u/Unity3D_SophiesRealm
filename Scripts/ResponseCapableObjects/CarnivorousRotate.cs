using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 2012</para>
/// <para>Last modified: 2012</para> 
/// <para>Author: Marcos Zalacain </para>
/// CarnivorousRotate:   
///    -This script is to control plant rotations.
/// </summary>
public class CarnivorousRotate : MonoBehaviour {
	
	private float plantPosition;
	void Start(){
		plantPosition = transform.position.x;
	}
	
	void OnTriggerStay(Collider col) {

		if(col.gameObject.tag == "Player"){
			
			if (plantPosition < col.transform.position.x) {
				animation.Play();
			}else{
				animation.Rewind();
			}				
		}
	} 
}
