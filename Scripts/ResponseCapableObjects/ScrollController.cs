using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 19/10/2012</para>
/// <para>Last modified: 19/10/2012 16:30</para> 
/// <para>Author: Marcos Zalacain </para>
/// ScrollController:   
///    -This script is to manage the Scrolls.
/// </summary>
public class ScrollController : MonoBehaviour {
	
	public GameObject lockAssociated;
	public GameObject leverAssociated;
	public GameObject QIPrefab;
	public float rotationSpeed = 60.0f;
	public Transform failAnswerPosition;
	
	void Update () {
		transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player"){
			// We'll disable the scroll collider so we don't hit it again while answering the QI.
			collider.enabled = false;
			// We'll launch the question interface.
			launchQI();
		}
	}
	
	void launchQI(){
				
		// Hide Controls and HUD gadgets.
		GameObject.Find("GUI/Controls").SendMessage("turnChildrenOff", SendMessageOptions.DontRequireReceiver);
		GameObject.Find("GUI/HUD").SendMessage("turnChildrenOff", SendMessageOptions.DontRequireReceiver);
		
		// Launch QI here and get a reference to the object instantiated:
		GameObject myCreature = Instantiate(QIPrefab, new Vector3(0.5f,0.5f,0), Quaternion.identity) as GameObject;
		// and store the creator GameObject reference in myCreator:
		myCreature.GetComponent<QuestionPrefab>().myCreator = gameObject;
		
		// Sound here.
		if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {
			audio.Play();
		}
	}
	
	// If answer right
	void rightAnswer(){
		// We'll destroy the scroll.
		Destroy(gameObject);
		// We'll destroy lock associated.
		Destroy(lockAssociated);
		// We'll unlock lever associated.
		leverAssociated.SendMessage("unlockLever", SendMessageOptions.DontRequireReceiver);
	}
	
	// If wrong answer
	void wrongAnswer(){
		// We shall move Sophie to previous spawn point.
		GameObject.Find("Characters/Sophie").transform.position = failAnswerPosition.position;
		// We must enable the scroll colider again.But we do so with a Coroutine to avoid problems.
		StartCoroutine(delayCollider(1.0f));
	}
	
	// This is a Coroutine to allow some time for the player to move back to fail answer position.
	IEnumerator delayCollider(float waitTime) {
        yield return new WaitForSeconds(waitTime);
		collider.enabled = true;
	}
}
