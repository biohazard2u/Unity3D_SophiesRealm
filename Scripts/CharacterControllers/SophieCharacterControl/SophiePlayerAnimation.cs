using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 7/10/2012</para>
/// <para>Last modified: 26/10/2012 18:00</para> 
/// <para>Author: Marcos Zalacain </para>
/// SophiePlayerAnimation:   
///    -This script is in charge of the Sophie's animation states.
/// </summary>
public class SophiePlayerAnimation : MonoBehaviour {

	// Adjusts the speed at which the walk animation is played back
	public float walkAnimationSpeedModifier = 2.5f;
	// Adjusts the speed at which the run animation is played back
	public float runAnimationSpeedModifier = 1.5f;
	// Adjusts the speed at which the jump animation is played back
	public float jumpAnimationSpeedModifier = 2.0f;
	
	public float shootAnimationSpeedModifier = 2.0f;
	public float dieAnimationSpeedModifier = 2.0f;
	
	// Adjusts after how long the falling animation will be 
	public float hangTimeUntilFallingAnimation = 0.8f;
	
	public float shootingSoundDelay = 0.8f;				// V2.0 so Sophie can shoot faster in last level.

	//private bool jumping = false;			// Do I need this???
	
	// Audio
	public AudioClip SophieJumpSoundEffect;
	public AudioClip SophieShootSoundEffect;
	public AudioClip SophieDiesSoundEffect;
	public AudioClip SophieHurtSoundEffect;
	public AudioClip SophieHurt2SoundEffect;
	private bool audioShoot = true;					// this is to time the audio shoot and avoid multiple times audios.
	
	private SophieController Sophie;		// GetComponent<SophieController> ();

	void Start ()
	{
		animation.Stop ();			//(In case user forgot to disable play automatically)
		
		// By default loop all animations
		animation.wrapMode = WrapMode.Loop;
		
		// Jump animation are in a higher layer:
		// Thus when a jump animation is playing it will automatically override all other animations until it is faded out.
		// This simplifies the animation script because we can just keep playing the walk / run / idle cycle without having to spcial case jumping animations.
		int jumpingLayer = 1;
		AnimationState jump = animation["SophieJump"];
		jump.layer = jumpingLayer;
		jump.speed *= jumpAnimationSpeedModifier;
		jump.wrapMode = WrapMode.Once;
				
		AnimationState run = animation["SophieRun"];
		run.speed *= runAnimationSpeedModifier;
		
		AnimationState walk = animation["SoldierWalk"];
		walk.speed *= walkAnimationSpeedModifier;
		
		AnimationState shoot = animation["SophieShoot"];
		shoot.speed *= shootAnimationSpeedModifier;
		shoot.layer = jumpingLayer;
		shoot.wrapMode = WrapMode.Once;
		
		AnimationState lifeDown = animation["SophieLifeDown"];
		lifeDown.layer = jumpingLayer + 1;
		lifeDown.wrapMode = WrapMode.Once;
		
		AnimationState die = animation["SophieDie"];
		die.layer = jumpingLayer;
		die.speed *= dieAnimationSpeedModifier;
		die.wrapMode = WrapMode.ClampForever;
	}
	
	void Update ()
	{
		Sophie = GetComponent<SophieController> ();
		//SophieControllerKeyboard Sophie = GetComponent<SophieControllerKeyboard> ();
		
		// We are not falling off the edge right now
		if (Sophie.GetHangTime () < hangTimeUntilFallingAnimation) {
			// Are we moving the character?
			if (Sophie.IsMoving ()) {
				// for touch screen:
				/**/
				if (Sophie.doubleTappingR || Sophie.doubleTappingL) {
					animation.CrossFade ("SophieRun");
				}else{
					animation.CrossFade ("SoldierWalk");
				}
				/**/
				// for Keyboard:
				/*
				if (Input.GetButton ("Fire2"))
					animation.CrossFade ("SophieRun");
				else
					animation.CrossFade ("SoldierWalk");
				*/
				
			}else{
				// Go back to idle when not moving
				animation.CrossFade ("SophieIdle", 0.5f);
			}
		// When falling off an edge, after hangTimeUntilFallingAnimation we will fade towards the ledgeFall animation
		} else {
			animation.CrossFade ("SophieJump");		//previous t-pose_4
		}
		
		// If jumping or walking/runing animations are on, we don't want Sophie Shooting. V2.0
		if (animation["SophieJump"].enabled || animation["SophieRun"].enabled  || animation["SoldierWalk"].enabled){
			Sophie.canDoShooting = false;
			// if audio has already started / is playing ==> we shall stop it! 
			if (!audioShoot) {
				audio.Stop();
			}
		}  
		// if shooting animation is playing, sophie can shoot arrows. V2.0
		if (animation["SophieShoot"].enabled) {
			Sophie.canDoShooting = true;
		}
	}

	void DidJump ()
	{
		animation.CrossFade ("SophieJump");
		playSoundEffect(SophieJumpSoundEffect);
	}
	
	void DidLand ()
	{
		animation.CrossFade ("SophieIdle");
	}
	
	void DidShoot ()
	{
		animation.CrossFade ("SophieShoot");
		if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {
			StartCoroutine(playShootingSoundEffect(shootingSoundDelay));
		}
	}
	
	void DidDie ()
	{
		animation.CrossFade ("SophieDie");
		playSoundEffect(SophieDiesSoundEffect);
	}
	
	void DidLifeDown()
	{
		animation.Blend("SophieLifeDown");
		if (Random.value <= 0.5) {
			playSoundEffect(SophieHurtSoundEffect);
		}else{
			playSoundEffect(SophieHurt2SoundEffect);
		}
	}
	
	// This is to play sound effect.
	void playSoundEffect(AudioClip aClip){
		if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {
			audio.PlayOneShot(aClip);
		}		
	}
	
	IEnumerator playShootingSoundEffect(float waitTime) {
        yield return new WaitForSeconds(waitTime);
		
		if (Sophie.canDoShooting) {			//V2.0
		
			if (audioShoot) {
				audio.PlayOneShot(SophieShootSoundEffect);
				audioShoot = false;
			}
			yield return new WaitForSeconds(waitTime);
			audioShoot = true;
		}
    }
}
