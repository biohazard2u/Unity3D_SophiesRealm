using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 7/11/2012</para>
/// <para>Last modified: 22/02/2013 17:00</para> 
/// <para>Author: Marcos Zalacain </para>
/// HardwareButton:
/// 	Attached to a gameObject, is responsible for checking the back key on Android,
/// 	If this key is pressed up, it launches the corresponding prefab where double check for exiting application is carried out.
///     Updated for double click.
/// </summary>
public class HardwareButton : MonoBehaviour {

	public GameObject warningPrefab;
	private bool isCreated;
	private bool doubleClick = false;
	
	void FixedUpdate () {
		
		// The Android Back button is the escape key in Unity.
		// If we have clicked before, ie double click...
		if (Input.GetKeyUp(KeyCode.Escape) && isCreated && doubleClick) {
			if (Application.loadedLevelName == "WelcomeScene" ){
				Application.Quit();
			}else{
				Application.LoadLevel("WelcomeScene");	
			}
		}
		// If, on the other hand, it is the first time we click...		
		if (Input.GetKeyUp(KeyCode.Escape)) {
						
			// Launch ExitAlert Prefab.
			if(!isCreated){
				Instantiate(warningPrefab);
				isCreated = true;
				doubleClick = true;
			}
		}
	}
	
	public void isCreatedToFalse(){
		if (isCreated) {
			isCreated = false;
		}
	}
}
