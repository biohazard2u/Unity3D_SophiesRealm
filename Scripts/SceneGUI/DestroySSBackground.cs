using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 29/01/2013</para>
/// <para>Last modified: 29/01/2013 18:30</para> 
/// <para>Author: Marcos Zalacain </para>
/// DestroySSBackground:   
///    -This script is to destroy BackgroundSS object.
/// </summary>
public class DestroySSBackground : MonoBehaviour {

	void Start () {
		
		if (GameObject.Find("BackgroundSS")){
			Destroy(GameObject.Find("BackgroundSS"));
		}
	}
}
