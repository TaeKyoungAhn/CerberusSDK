using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Cerberus_Platform_Singleton
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(CerberusPlatform))]
    public class CerberusPlatformEditor : Editor
    {
        public CerberusPlatform cerberusPlatform;
        public Texture2D Logo;


        private void OnEnable()
        {
            if (AssetDatabase.Contains(target))
            {
                cerberusPlatform = null;
            }
            else
            {
                cerberusPlatform = (CerberusPlatform)target;
                Logo = (Texture2D)Resources.Load("Textures/Logo12");

            }
        }


        public override void OnInspectorGUI()
        {
            if (cerberusPlatform == null)
            {
                return;
            }

            Color utoColor = new Color(1.0f, 0.64f, 0.0f);
            GUIStyle allEditStyle = new GUIStyle(GUI.skin.label);
            allEditStyle.normal.textColor = utoColor;
            allEditStyle.fontStyle = FontStyle.Bold;

            ////////////////////////////////////////////////////////////////////////////////////
            #region LogoImage
            GUILayout.BeginHorizontal("box");
            GUIStyle myStyle = new GUIStyle(GUI.skin.label);
            myStyle.alignment = TextAnchor.MiddleCenter;
            GUILayout.FlexibleSpace();
            GUILayout.Label(Logo, myStyle, GUILayout.Height(200), GUILayout.Width(500));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            #endregion
            ////////////////////////////////////////////////////////////////////////////////////


            ////////////////////////////////////////////////////////////////////////////////////
            #region version Info
            GUILayout.BeginHorizontal("box");
            GUIStyle versionStyle = new GUIStyle(GUI.skin.label);
            versionStyle.alignment = TextAnchor.MiddleRight;
            EditorGUILayout.LabelField(" Cerberus_Platform [Version 0.1]", versionStyle);
            GUILayout.EndHorizontal();
            #endregion
            ////////////////////////////////////////////////////////////////////////////////////
        }


    }
}