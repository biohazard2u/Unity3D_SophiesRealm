using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof (CapsuleCollider))]

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 11/11/2012</para>
/// <para>Last modified: 01/04/2013 11:00</para> 
/// <para>Author: Marcos Zalacain </para>
/// EnemyTrooperController:   
///    -To control Enemy trooper
/// Improvements:
///    -Smooth some speeds up
/// Latest update:
///    -(other.gameObject.tag == "PlantKiller") logic has been added when collision with arrows.
/// </summary>
public class EnemyTrooperController : MonoBehaviour {
	
	// Target points.
	public Transform targetPointA;
	public Transform targetPointB;		
	// Enemy moving speeds.
	public float walkSpeed = 3.0f;
	public float runSpeed = 10.0f;
	public float rotationSpeed = 3.0f;
	public float restingTime = 3.0f;
	// Enemy animations speeds.
	public float walkAnimationSpeedModifier = 45f;
	public float runAnimationSpeedModifier = 1.5f;
	public float attackAnimationSpeedModifier = 1.5f;
	public float dieAnimationSpeedModifier = 2.0f;
	
	public AudioClip enemyDiesSndEffect;
	public AudioClip enemyattacksSndEffect;
	
	private Vector3 initialPosition, walkingTarget;
	private Transform currentTransform;
	
	// Boolean for has reached target.
	private bool hasReachedTarget = false;
	// Booleans for can dos.
	private bool canSeeSophie = false;
	private bool canSword = false;
	private bool mustDie = false;
	private bool canMove = true;
	
	private Vector3 sophiesPosition;
	
	private bool sophieIsDead = false;		// true when Sophie looses all lifes so Trooper stops attacking her.
	private bool canPlaySndEff = true;		// So attack sound effect isnt played multiple times over itself.

	void Awake(){
		currentTransform = transform;
	}
	
	void Start () {
	
		animation.Stop ();
		animation.wrapMode = WrapMode.Loop;
		
		int higherLayer = 1;
		
		AnimationState walk = animation["SoldierWalk"];
		walk.speed *= walkAnimationSpeedModifier * Time.deltaTime;
		
		AnimationState run = animation["SophieRun"];
		run.speed *= runAnimationSpeedModifier;
		
		AnimationState attack = animation["SoldierFight"];
		attack.speed *= attackAnimationSpeedModifier;
		
		AnimationState die = animation["SoldierDie"];
		die.layer = higherLayer;
		die.speed *= dieAnimationSpeedModifier;
		die.wrapMode = WrapMode.ClampForever;
		
		AnimationState dis = animation["soldierDisappear"];
		dis.layer = higherLayer;
	
		// Set initial position.
		initialPosition = targetPointA.position;
		// Set initial walking target.
		walkingTarget = targetPointB.position;
		
		sophieIsDead = false;
	}
	
	void FixedUpdate ()
	{
		// Make sure we are absolutely always in the 2D plane and touching ground.
		Vector3 newPos = currentTransform.position;
		newPos.z = initialPosition.z;
		newPos.y = initialPosition.y;				
		currentTransform.position = newPos;
		//currentTransform.rotation = Quaternion.Euler(0, currentTransform.rotation.eulerAngles.y, 0);
	}
	
	void Update () {
		
		// Raycast stuff.
		Vector3 fwd = currentTransform.TransformDirection(Vector3.forward);
		Vector3 higherPos = currentTransform.position + new Vector3(0, 1, 0);
		RaycastHit hit;
		Debug.DrawRay(higherPos, 5*fwd, Color.yellow);
		
		// Checking Raycast... Enemy sees Sophie
		if (Physics.Raycast (higherPos, fwd, out hit, 5)) {			
			if(hit.collider.gameObject.tag == "Player"){
				sophiesPosition = hit.collider.gameObject.transform.position;
				canSeeSophie = true;
				run(sophiesPosition);
			}else{
				canSeeSophie = false;
			}
		}
		
		// Re-set walking target if needed.
		if (Math.Round(currentTransform.position.x) == Math.Round(targetPointA.position.x)) {
			hasReachedTarget = true;
			walkingTarget = targetPointB.position;
		}else if(Math.Round(currentTransform.position.x) == Math.Round(targetPointB.position.x)){
			hasReachedTarget = true;
			walkingTarget = targetPointA.position;
		}else{
			hasReachedTarget = false;
			walk(walkingTarget);
		}
		
		// AI.
		if (mustDie) {
			die();
		}else{		
			if (hasReachedTarget) {
				StartCoroutine(restAndTurn(restingTime));
				canSword = false;
				canSeeSophie = false;
			}
		}
		//Debug.Log(canSeeSophie);
	}
	
	// Method call on Child to detect attack zone
	public void attackTrigger(Collider col, bool b) { 
		if (col.gameObject.tag == "Player") {
			if (b) {
				canSword = true;
			}else{
				canSword = false;
			}
		}
	}
	
	public void sophieIsSuperDead(){
		sophieIsDead = true;
	}
	
	// ENEMY ACTIONS
	// ------------------------------------------
	// Walk to target.
	void walk(Vector3 walkingTarget){
		if(canMove){	
			if (hasReachedTarget) {
				restAndTurn(restingTime);
			}else if (canSword) {
				//attack();
				StartCoroutine(attack(0.5f));
			}else if (canSeeSophie) {
				run(sophiesPosition);
			}else{
				// Do the walking.
				animation.CrossFade("SoldierWalk");
				hasReachedTarget = false;
				canSeeSophie = false;
				canSword = false;
				
				// Rotate and ...
				currentTransform.rotation = Quaternion.Slerp(
					currentTransform.rotation, 
					Quaternion.LookRotation(walkingTarget - currentTransform.position), 
					rotationSpeed * Time.deltaTime);
				// ... move.
				currentTransform.position += currentTransform.forward * walkSpeed * Time.deltaTime;
			}
		}
	}
	
	// Run to Sophie.
	void run(Vector3 sophiesPos){
		if(canMove){
			if (hasReachedTarget) {
				restAndTurn(restingTime);
			}else if (canSword) {
				//attack();
				StartCoroutine(attack(0.5f));
			}else if (!canSeeSophie) {
				walk(walkingTarget);
			}else{
				// Do the running.
				animation.CrossFade ("SophieRun");
				hasReachedTarget = false;
				canSword = false;
				
				/*		THIS HAS BEEN COMMENTATED SO SOPHIE DOES NOT TURN WHILE RUNNING.	
				currentTransform.rotation = Quaternion.Slerp(
					currentTransform.rotation, 
					Quaternion.LookRotation(sophiesPos - currentTransform.position), 
					rotationSpeed * Time.deltaTime);*/
				//currentTransform.rotation.y = 270.0f;
				
				//speed = Mathf.Lerp (speed, runSpeed, speedSmoothing);				
				currentTransform.position += currentTransform.forward * runSpeed * Time.deltaTime;
			}
		}
	}
	
	// Attack
	IEnumerator attack(float waitingTime){
		if (hasReachedTarget && canSword) {
			// Do the attacking;
			animation.CrossFade ("SoldierFight");
		}else if (hasReachedTarget && !canSword) {
			restAndTurn(restingTime);
		}else{
			canMove = false;
			animation.CrossFade ("SoldierFight");
			if (canPlaySndEff) {
				StartCoroutine(playSoundEffect(1.5f, true));
			}
			hasReachedTarget = false;
			canSeeSophie = false;
			yield return new WaitForSeconds(waitingTime);
			canSeeSophie = false;
			canMove = true;
			if (sophieIsDead) {
				canSword = false;
				canSeeSophie = false;
			}
		}
	}
		
	// Rest and turn
	IEnumerator restAndTurn(float waitingTime){
		
		// Do the resting and turning.		
		animation.CrossFade ("SophieIdle");
		yield return new WaitForSeconds(waitingTime);
		hasReachedTarget = false;
		walk(walkingTarget);
	}
	
	// Die
	void die(){
		animation.CrossFade ("SoldierDie");
		canSword = false;
		hasReachedTarget = false;
		canSeeSophie = false;
	}
	
	// Watch for arrow collisions.
	void OnTriggerEnter(Collider other) {
		// We check if 'other' is a tag as Arrow.
		if (other.gameObject.tag == "Arrow" || other.gameObject.tag == "PlantKiller") {	
	        Destroy(other.gameObject);
			animation.CrossFade("SoldierDie");
			canMove = false;
			canSeeSophie = false;
			canSword = false;
			collider.enabled = false;
			StartCoroutine(destroyThisTrooper(6f));
			StartCoroutine(playSoundEffect(0.1f, false));
			canPlaySndEff = false;
		}
    }
	
	// Destroy trooper
	IEnumerator destroyThisTrooper(float waitingTime){	
		yield return new WaitForSeconds(waitingTime);
		animation.Blend("soldierDisappear");
		Destroy(gameObject, 1f);
	}
	
	// This is to play sound effect.
	IEnumerator playSoundEffect(float waitTime, bool isAttack) {
		
		if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {
		
			if (!isAttack) {							// die sound.
				audio.PlayOneShot(enemyDiesSndEffect);
			}else{
				canPlaySndEff = false;
				audio.PlayOneShot(enemyattacksSndEffect);
				yield return new WaitForSeconds(waitTime);
				canPlaySndEff = true;
			}
		}
    }
}
