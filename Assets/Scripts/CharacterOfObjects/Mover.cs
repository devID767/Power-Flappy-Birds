using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
//using UnityEditor.SceneManagement;

//[CustomEditor(typeof(Mover))]
//[CanEditMultipleObjects]
//public class MoverEditor : Editor
//{
//    private Mover mover;

//    private void OnEnable()
//    {
//        mover = (Mover)target;
//    }

//    public override void OnInspectorGUI()
//    {
//        serializedObject.Update();

//        var SetGlobalSpeed = EditorGUILayout.Toggle("Set global speed", mover.SetGlobalSpeed);

//        if (!mover.SetGlobalSpeed)
//        {
//            var speed = EditorGUILayout.FloatField("Speed", mover.speed);
//            var increaseSpeed = EditorGUILayout.FloatField("Increase speed", mover.increaseSpeed);
//            var maxSpeed = EditorGUILayout.FloatField("Max speed", mover.maxSpeed);

//            foreach (var Target in targets)
//            {
//                mover = (Mover)Target;

//                mover.SetGlobalSpeed = SetGlobalSpeed;

//                mover.speed = speed;
//                mover.increaseSpeed = increaseSpeed;
//                mover.maxSpeed = maxSpeed;
//            }
//        }
//        else
//        {
//            var globalSpeed = (GlobalSpeed)EditorGUILayout.ObjectField("Global speed", mover.globalSpeed, typeof(GlobalSpeed), true);
//            foreach (var Target in targets)
//            {
//                mover = (Mover)Target;

//                mover.SetGlobalSpeed = SetGlobalSpeed;

//                mover.globalSpeed = globalSpeed;
//            }
//        }

//        if (GUI.changed)
//        {
//            EditorUtility.SetDirty(mover.gameObject);
//            EditorSceneManager.MarkSceneDirty(mover.gameObject.scene);
//        }

//        serializedObject.ApplyModifiedProperties();
//    }
//}

public class Mover : MonoBehaviour
{
    [SerializeField] private bool SetGlobalSpeed = false;
    [SerializeField] private GlobalSpeed globalSpeed;

    public float speed;
    [SerializeField] private float increaseSpeed;
    [SerializeField] private float maxSpeed;

    private void OnEnable()
    {
        Score.Changed += PointsCollected;
    }

    private void Start()
    {
        if (SetGlobalSpeed)
        {
            try
            {
                speed = globalSpeed.speed;
                increaseSpeed = globalSpeed.increaseSpeed;
                maxSpeed = globalSpeed.maxSpeed;
            }
            catch
            {
                speed = 0;
                increaseSpeed = 0;
                maxSpeed = 0;
            }
        }

        if (speed + Score.count * increaseSpeed < maxSpeed)
            speed += Score.count * increaseSpeed;
        else
            speed = maxSpeed;
    }
    private void PointsCollected(int score)
    {
        if(speed < maxSpeed)
        {
            speed += increaseSpeed;
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        Score.Changed -= PointsCollected;
    }
}
