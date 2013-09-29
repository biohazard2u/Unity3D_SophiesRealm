using UnityEngine;
using System.Collections;

public class AdvertisementManager : MonoBehaviour {

    void OnGUI()
    {
        // Make a background box
        GUI.Box(new Rect((Screen.width / 2) - 120, (Screen.height / 2)-140, 220, 220), "Loader Menu");
        
        // Make the Enable Button
        if (GUI.Button(new Rect((Screen.width / 2) - 110, (Screen.height / 2)-110, 200, 40), "Enable")){
            AdvertisementHandler.EnableAds();
        }

        // Make the Disable Button
        if (GUI.Button(new Rect((Screen.width / 2) - 110, (Screen.height / 2)-65, 200, 40), "Disable")){           
            AdvertisementHandler.DisableAds();
        }

        // Make the Hide Button
        if (GUI.Button(new Rect((Screen.width / 2) - 110, (Screen.height / 2)-20, 200, 40), "Hide")){
            AdvertisementHandler.HideAds();
        }

        // Make the Show button.
        if (GUI.Button(new Rect((Screen.width / 2) - 110, (Screen.height / 2)+25, 200, 40), "Show")){
            AdvertisementHandler.ShowAds();
        }

    }

	// Use this for initialization
	void Start () {
        Debug.Log("Unity Calling Start");
        AdvertisementHandler.Instantiate("a1513396948b49f", AdvertisementHandler.AdvSize.BANNER, AdvertisementHandler.AdvOrientation.HORIZONTAL, AdvertisementHandler.Position.BOTTOM, AdvertisementHandler.Position.CENTER_HORIZONTAL, false, AdvertisementHandler.AnimationInType.SLIDE_IN_LEFT, AdvertisementHandler.AnimationOutType.FADE_OUT, AdvertisementHandler.LevelOfDebug.LOW);
        AdvertisementHandler.EnableAds();
	}
}
