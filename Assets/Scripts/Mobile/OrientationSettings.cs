using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cerberus_Platform_Mobile
{
    public class OrientationSettings : MonoBehaviour
    {
#if UNITY_STANDALONE


#elif UNITY_ANDROID
        /// <summary>
        /// This function makes it possible to call when building a fixed screen.
        /// You can choose from a drop-down format in the editor, and you can call it from anywhere.
        /// </summary>
        /// <param name="orName"></param>
        public void OrientationChanged(string orName)
        {
            if (orName == "Portrait")
            {
                Screen.orientation = ScreenOrientation.Portrait;
            }
            else if (orName == "LandscapeLeft")
            {
                Screen.orientation = ScreenOrientation.LandscapeLeft;
            }
            else if (orName == "LandscapeRight")
            {
                Screen.orientation = ScreenOrientation.LandscapeRight;
            }
            else
            {
                Debug.Log("Chacking your Orientations Name");
            }
        }
#endif

    }
}

