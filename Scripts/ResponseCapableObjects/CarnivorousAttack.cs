using UnityEngine;
using System.Collections;

public class CarnivorousAttack : MonoBehaviour {
	
	//public float chillCarnivorousAttack = 2.5f;
	//public float ferociousCarnivorousAttack = 4.5f;
	//public float chillAttackTime = 3.0f;
	
	//private bool plantAttacking = false;
	private float timeToAttack = 6.0f;
		
	void Start (){
		animation.Stop ();	
		animation.wrapMode = WrapMode.Once;
	}
	
	void OnTriggerStay(Collider col) {

		if(col.gameObject.tag == "Player"){
			
			timeToAttack += Time.deltaTime;
			if (timeToAttack >= 6) {
				animation.Play ();
				timeToAttack = 0.0f;
				if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {
					StartCoroutine(playSEfect(0.8f));
				}
			}	
		}
	}
	
	IEnumerator playSEfect(float waitTime) {
        yield return new WaitForSeconds(waitTime);
		audio.Play();
		yield return new WaitForSeconds(waitTime);
		audio.Stop();
    }
}
