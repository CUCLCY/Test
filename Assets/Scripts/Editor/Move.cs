using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Move))]
public class MoveEditor : Editor
{
    private SerializedProperty begin;
    private SerializedProperty end;
    private SerializedProperty time;
    private SerializedProperty pingpong;
    private SerializedProperty easeType;

    private void OnEnable()
    {
        begin = serializedObject.FindProperty("begin");
        end = serializedObject.FindProperty("end");
        time = serializedObject.FindProperty("time");
        pingpong = serializedObject.FindProperty("pingpong");
        easeType = serializedObject.FindProperty("easeType");
    }

    public override VisualElement CreateInspectorGUI()
    {
        var move = target as Move;
        var root = new VisualElement();

        var beginField = new PropertyField(begin);
        var endField = new PropertyField(end);
        var timeField = new PropertyField(time);
        var pingpongField = new PropertyField(pingpong);
        var easeField = new PropertyField(easeType);
        var moveBtn = new Button();
        moveBtn.text = "move";
        moveBtn.clicked += () =>
        {
            move.move();
        };
        root.Add(beginField);
        root.Add(endField);
        root.Add(timeField);
        root.Add(pingpongField);
        root.Add(easeField);
        root.Add(moveBtn);
        return root;
    }
}
