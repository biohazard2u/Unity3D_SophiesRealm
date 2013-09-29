using UnityEngine;
using System.Collections;

public class AdvertisementHandler : MonoBehaviour
{
    public enum AdvSize
    {
        BANNER,
        IAB_MRECT,
        IAB_BANNER,
        IAB_LEADERBOARD,
        DEVICE_WILL_DECIDE
    };

    public enum AdvOrientation
    {
        VERTICAL,
        HORIZONTAL 
    };

    public enum Position
    {
        NO_GRAVITY = 0,
        CENTER_HORIZONTAL = 1,
        LEFT = 3,
        RIGHT = 5,
        FILL_HORIZONTAL = 7,
        CENTER_VERTICAL = 16,
        CENTER = 17,
        TOP = 48,
        BOTTOM = 80,
        FILL_VERTICAL = 112
    };

    public enum AnimationInType
    {
        SLIDE_IN_LEFT, 
        FADE_IN,
    };

    public enum AnimationOutType
    {
        SLIDE_OUT_RIGHT,
        FADE_OUT,
    };

    public enum Activity
    {
        INSTANTIATE,
        DISABLE,
        ENABLE,
        HIDE,
        SHOW,
        REPOSITION
    }
    public enum LevelOfDebug
    {
        NONE,
        LOW,
        HIGH
    }

    static AndroidJavaClass admobPluginClass;
    static AndroidJavaClass unityPlayer;
    static AndroidJavaObject currActivity;

    public static void Instantiate(string pubID, AdvSize advSize, AdvOrientation advOrient, Position position_1, Position position_2, bool isTesting, AnimationInType animIn, AnimationOutType animOut, LevelOfDebug levelOfDebug)
    {
        //Debug.Log("Instantiate Called");
        admobPluginClass = new AndroidJavaClass("com.microeyes.admob.AdmobActivity");
        unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        currActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        admobPluginClass.CallStatic("AdvHandler", (int)Activity.INSTANTIATE, currActivity, pubID, (int)advSize, (int)advOrient, (int)position_1, (int)position_2, isTesting, (int)animIn, (int)animOut, (int)levelOfDebug);
        //Debug.Log("Instantiate FINISHED");
    }

    public static void EnableAds()
    {
        //Debug.Log("ENABLED Called");
        admobPluginClass.CallStatic("AdvHandler", (int)Activity.ENABLE, currActivity, "", -1, -1, -1, -1, false, -1, -1, -1);
        //Debug.Log("ENABLED FINISHED");        
    }

    

    public static void DisableAds()
    {
        //Debug.Log("DISABLED Called");
        admobPluginClass.CallStatic("AdvHandler", (int)Activity.DISABLE, currActivity, "", -1, -1, -1, -1, false, -1, -1, -1);
        //Debug.Log("DISABLED FINISHED");
    }

    public static void HideAds()
    {
        //Debug.Log("HIDE ADV Called");
        admobPluginClass.CallStatic("AdvHandler", (int)Activity.HIDE, currActivity, "", -1, -1, -1, -1, false, -1, -1, -1);
        //Debug.Log("HIDE ADV FINISHED");
    }

    public static void ShowAds()
    {
        //Debug.Log("SHOW ADV Called");
        admobPluginClass.CallStatic("AdvHandler", (int)Activity.SHOW, currActivity, "", -1, -1, -1, -1, false, -1, -1, -1);
        //Debug.Log("SHOW ADV FINISHED");
    }

    public static void RepositionAds(Position position_1, Position position_2)
    {
        //Debug.Log("REPOSITION Called");
        admobPluginClass.CallStatic("AdvHandler", (int)Activity.REPOSITION, currActivity, "", -1, -1, (int)position_1, (int)position_2, false, -1, -1, -1);
        //Debug.Log("REPOSITION FINISHED");
    }    
}
