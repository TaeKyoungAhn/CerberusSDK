using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Cerberus_Platform;


[CustomEditor(typeof(CameraController))]
public class CameraControllerEdit : Editor
{
    public CameraController cameraController;
    public Texture2D Logo;
    public GameObject cameraObject;

    SerializedProperty CameraMode_Prop;

    //GeneralCamera
    SerializedProperty TargetProp;

    SerializedProperty distanceProp;

    SerializedProperty maxDistProp;
    SerializedProperty minDistProp;

    SerializedProperty maxAngleProp;
    SerializedProperty minAngleProp;

    SerializedProperty smoothSensProp;

    SerializedProperty xMouseSensProp;
    SerializedProperty yMouseSensProp;


    //FlyCamera
    SerializedProperty speedProp;
    SerializedProperty shiftSpeedProp;
    SerializedProperty maxSpeedProp;
    SerializedProperty camSensProp;

    private void OnEnable()
    {
        if (AssetDatabase.Contains(target))
        {
            cameraController = null;
        }
        else
        {
            cameraController = (CameraController)target;
            Logo = (Texture2D)Resources.Load("Textures/Logo12");

            CameraMode_Prop = serializedObject.FindProperty("cameraMode");

            //GeneralCamera
            TargetProp = serializedObject.FindProperty("target");
            distanceProp = serializedObject.FindProperty("distance");

            minDistProp = serializedObject.FindProperty("distanceMin");
            maxDistProp = serializedObject.FindProperty("distanceMax");

            maxAngleProp = serializedObject.FindProperty("yMaxLimit");
            minAngleProp = serializedObject.FindProperty("yMinLimit");

            smoothSensProp = serializedObject.FindProperty("smoothTime");

            xMouseSensProp = serializedObject.FindProperty("xSpeed");
            yMouseSensProp = serializedObject.FindProperty("ySpeed");


            //FlyCamera
            speedProp = serializedObject.FindProperty("mainSpeed");
            shiftSpeedProp = serializedObject.FindProperty("shiftAdd");
            maxSpeedProp = serializedObject.FindProperty("maxShift");
            camSensProp = serializedObject.FindProperty("camSens");
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (cameraController == null)
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
        #region CameraMode
        EditorGUILayout.LabelField("Select Camera Mode", allEditStyle);
        EditorGUILayout.BeginVertical("Box");

        EditorGUILayout.PropertyField(CameraMode_Prop);

        if ((CameraMode)CameraMode_Prop.enumValueIndex == CameraMode.GeneralCamera)
        {
            EditorGUILayout.PropertyField(TargetProp);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(distanceProp);

            EditorGUILayout.PropertyField(minDistProp);
            EditorGUILayout.PropertyField(maxDistProp);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(maxAngleProp);
            EditorGUILayout.PropertyField(minAngleProp);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(smoothSensProp);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(xMouseSensProp);
            EditorGUILayout.PropertyField(yMouseSensProp);

        }
        else if ((CameraMode)CameraMode_Prop.enumValueIndex == CameraMode.FlyCamera)
        {
            EditorGUILayout.PropertyField(speedProp);
            EditorGUILayout.PropertyField(shiftSpeedProp);
            EditorGUILayout.PropertyField(maxSpeedProp);
            EditorGUILayout.PropertyField(camSensProp);

        }
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.EndVertical();
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////
        #region AddObject & Component
        GUILayout.BeginVertical("box");
        //필요 오브젝트 및 컴포넌트 삽입
        EditorGUILayout.LabelField("Add Objects & Components", allEditStyle);

        //자식 오브젝트 추가
        if (GUILayout.Button("Add Camera"))
        {
            cameraObject = Instantiate(Resources.Load("Prefabs/Camera")) as GameObject;
            cameraObject.GetComponent<Transform>().SetParent(cameraController.GetComponent<Transform>());

        }
        //rigidbody 추가
        if (GUILayout.Button("Add Rigidbody"))
        {
            if (cameraController.transform.gameObject.GetComponent<Rigidbody>() != null)
            {
                return;
            }
            cameraController.transform.gameObject.AddComponent<Rigidbody>();
            cameraController.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        GUILayout.EndVertical();
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

    public override bool HasPreviewGUI()
    {
        return true;
    }

}

