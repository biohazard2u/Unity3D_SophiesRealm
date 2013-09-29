using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 16/1/2013</para>
/// <para>Last modified: 23/01/2013 18:00</para> 
/// <para>Author: Marcos Zalacain </para>
/// CollisionSoundEffectEnemy:   
///    -Small script to hold a reference to an audioclip to play when the ENEMY hits me.
///    -This script is attached to game object making up our level. 
///    -The "FootEnemy" script (which is attached to the enemy soldier) looks for this script on whatever it touches. If it finds it, 
/// 	then it will play the sound when the foot comes in contact. 
/// </summary>
public class CollisionSoundEffectEnemy : MonoBehaviour {

	public AudioClip audioClip;
	public float volumeModifier = 1.0f;
}
