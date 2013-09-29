using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 14/03/2013</para>
/// <para>Last modified: 14/03/2013 17:40</para> 
/// <para>Author: Marcos Zalacain </para>
/// TutotialTip:    
///    -If we touch the questionMark sign, this script launches the TipsPrefabQyestionMark.
/// </summary>
public class TutotialTip : MonoBehaviour {
	
	public GameObject TipQuestionMarkPrefab;
	public int tipNumer;
	private bool isCreated = false;
	
	void Update (){
		
		if(Input.touchCount > 0){
			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit))	{
				if (hit.transform == this.gameObject.transform) {	
					if(!isCreated){
						
						GameObject myChild = Instantiate(TipQuestionMarkPrefab) as GameObject;
						myChild.GetComponent<TipsPrefabQuestionMark>().myMaker = gameObject;
						myChild.GetComponent<TipsPrefabQuestionMark>().myMakersTipNumber = tipNumer;
						
						isCreated = true;
					}
				}
			}
		}
	}
		
	// MAY NOT NEED THIS..........
	/*public void isCreatedToFalse(){
		if (isCreated) {
			isCreated = false;
		}
	}	*/
	// MAY NOT NEED THIS..........
}
