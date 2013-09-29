using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 20/11/2012</para>
/// <para>Last modified: 22/01/2013 22:30</para> 
/// <para>Author: Marcos Zalacain </para>
/// AudioController:   
///    -This script follows the Singleton pattern.
///    -In charge of controlling background music and some buttons soundEffects.
/// </summary>
public class AudioController : MonoBehaviour  {

	private static AudioController instance = null;
	
	public static AudioController Instance {
        get { return instance; }
    }
	
	public AudioClip gamePlayAudio;
	public AudioClip menusAudio;
	public AudioClip buttonSndEffect;
	public AudioClip backButtonSndEffect;
	
	private int loadedLevel;		// to store former level/scene.
	
	void Awake() {
		
		if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
			checkAndPlayAudio();
        }
        DontDestroyOnLoad(this.gameObject);

	}
	
	// Checking levels here
	void OnLevelWasLoaded(int level) {
		
        if (level >= 5){
			audio.clip = gamePlayAudio;
			checkAndPlayAudio();
			loadedLevel = 5;			// even if am on level 6!
		}else{
			audio.clip = menusAudio;
			if (loadedLevel == 5) {
				checkAndPlayAudio();
				loadedLevel = Application.loadedLevel;
			}
		}
    }
	
	// Play audio if set as On	
	void checkAndPlayAudio(){
		if (Globals.choosenMusic == Globals.MusicOn) {	
			audio.Play();
		}
	}
	
	// Next two methods are for ButtonsEffects, so they can be fully played even when scene has ended.
	void buttonsSoundEffect(){	
		if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {	
			audio.PlayOneShot(buttonSndEffect);
		}
	}
	void backButtonsSoundEffect(){	
		if (Globals.choosenSoundEffects == Globals.SoundEffectsOn) {	
			audio.PlayOneShot(backButtonSndEffect);
		}
	}
}

