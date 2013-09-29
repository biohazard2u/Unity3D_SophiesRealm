using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 16/1/2013</para>
/// <para>Last modified: 16/1/2013 18:00</para> 
/// <para>Author: Marcos Zalacain </para>
/// CollisionSoundEffect:   
///    -Small script to hold a reference to an audioclip to play when the player hits me.
///    -This script is attached to game object making up our level. 
///    -The "Foot" script (which is attached to the player) looks for this script on whatever it touches. If it finds it, 
/// 	then it will play the sound when the foot comes in contact. 
/// </summary>
public class CollisionSoundEffect : MonoBehaviour
{
	public AudioClip audioClip;
	public float volumeModifier = 1.0f;
}
