using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 16/1/2013</para>
/// <para>Last modified: 23/01/2013 18:00</para> 
/// <para>Author: Marcos Zalacain </para>
/// FootEnemy:   
///	   -This script is attached to Enemy foot game object and plays foot steps sounds. 
/// </summary>
public class FootEnemy : MonoBehaviour {

	public float baseFootAudioVolume = 1.0f;
	public float soundEffectPitchRandomness = 0.05f;

	void OnTriggerEnter (Collider other)
	{
		if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {
			
			CollisionSoundEffectEnemy collisionSoundEffect = other.GetComponent<CollisionSoundEffectEnemy> ();
			
			if (collisionSoundEffect) {
				audio.clip = collisionSoundEffect.audioClip;
				audio.volume = collisionSoundEffect.volumeModifier * baseFootAudioVolume;
				audio.pitch = Random.Range (1.0f - soundEffectPitchRandomness, 1.0f + soundEffectPitchRandomness);
				audio.Play ();
			}
		}
	}

	void Reset ()
	{
		rigidbody.isKinematic = true;
		collider.isTrigger = true;
	}	
}
