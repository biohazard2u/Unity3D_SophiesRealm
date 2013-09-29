using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 29/9/2012</para>
/// <para>Last modified: 01/04/2013 18:30</para> 
/// <para>Author: Marcos Zalacain </para>
/// TurnControlChildrenOnOFF:   
///    -This script is to turn the attached object children on and Off.
/// </summary>
public class TurnControlChildrenOnOFF : MonoBehaviour {

	void Start () {
		//turnChildrenOn();
		
		string currLevelStr = Application.loadedLevelName.Remove(0,5);
		int currLevelInt = System.Convert.ToInt32(currLevelStr);
		
		// If we are not on first stage nor on level37, 46, 55, show gamePlay GUI as usual.
		if (currLevelInt == 1) {
			// Do nothing!
		}else if (currLevelInt == 37) {
			// Do nothing!
		}else if (currLevelInt == 46) {
			// Do nothing!
		}else if (currLevelInt == 55) {
			// Do nothing!
		}else if (currLevelInt == 73) {
			// Do nothing!
		}else{
			turnChildrenOn();
		}
	}
	
	void turnChildrenOn () {
		foreach (Transform child in transform) {
            child.gameObject.SetActiveRecursively(true);
		}
	}
	
	void turnChildrenOff () {
		foreach (Transform child in transform) {
            child.gameObject.SetActiveRecursively(false);
		}	
	}	
}
