using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 07/03/2013</para>
/// <para>Last modified: 07/03/2013 15:10</para> 
/// <para>Author: Marcos Zalacain </para>
/// AdManager:   
///    -Singelton in charge of the adds.
/// </summary>
public class AdManager : MonoBehaviour {
	
	private static AdManager instance = null;
	
	public static AdManager Instance {
        get { return instance; }
    }
	
	void Awake() {
		
		if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
	}
	
	void Start () {
		
        AdvertisementHandler.Instantiate("a1513396948b49f",
			AdvertisementHandler.AdvSize.BANNER,
			AdvertisementHandler.AdvOrientation.HORIZONTAL,
			AdvertisementHandler.Position.TOP,
			AdvertisementHandler.Position.LEFT,
			false,
			AdvertisementHandler.AnimationInType.SLIDE_IN_LEFT,
			AdvertisementHandler.AnimationOutType.FADE_OUT,
			AdvertisementHandler.LevelOfDebug.LOW);
		
        AdvertisementHandler.EnableAds();	
		AdvertisementHandler.HideAds();
	}
	
	void OnLevelWasLoaded(int level) {
		
		if (level > 4) {
			AdvertisementHandler.ShowAds();
		}else{
			AdvertisementHandler.HideAds();
			
		}
	}
}
