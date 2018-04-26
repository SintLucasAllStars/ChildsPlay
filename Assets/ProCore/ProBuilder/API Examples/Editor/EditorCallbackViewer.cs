using System;
using System.Collections.Generic;
using System.Linq;
using ProBuilder2.Common;
using ProBuilder2.EditorCommon;
using ProBuilder2.Interface;
using UnityEditor;
using UnityEngine;
// pb_Editor and pb_EditorUtility
// pb_GUI_Utility
// EditLevel

// Sum()

internal class EditorCallbackViewer : EditorWindow
{
    private bool collapse = true;

    private readonly List<string> logs = new List<string>();
    private Vector2 scroll = Vector2.zero;

    private static Color logBackgroundColor
    {
        get { return EditorGUIUtility.isProSkin ? new Color(.15f, .15f, .15f, .5f) : new Color(.8f, .8f, .8f, 1f); }
    }

    private static Color disabledColor
    {
        get { return EditorGUIUtility.isProSkin ? new Color(.3f, .3f, .3f, .5f) : new Color(.8f, .8f, .8f, 1f); }
    }

    [MenuItem("Tools/" + pb_Constant.PRODUCT_NAME + "/API Examples/Log Callbacks Window")]
    private static void MenuInitEditorCallbackViewer()
    {
        GetWindow<EditorCallbackViewer>(true, "ProBuilder Callbacks", true).Show();
    }

    private void OnEnable()
    {
        // Delegate for Top/Geometry/Texture mode changes.
        pb_Editor.AddOnEditLevelChangedListener(OnEditLevelChanged);

        // Called when a new ProBuilder object is created.
        // note - this was added in ProBuilder 2.5.1
        pb_EditorUtility.AddOnObjectCreatedListener(OnProBuilderObjectCreated);

        // Called when the ProBuilder selection changes (can be object or element change).
        // Also called when the geometry is modified by ProBuilder.
        pb_Editor.OnSelectionUpdate += OnSelectionUpdate;

        // Called when vertices are about to be modified.
        pb_Editor.OnVertexMovementBegin += OnVertexMovementBegin;

        // Called when vertices have been moved by ProBuilder.
        pb_Editor.OnVertexMovementFinish += OnVertexMovementFinish;

        // Called when the Unity mesh is rebuilt from ProBuilder mesh data.
        pb_EditorUtility.AddOnMeshCompiledListener(OnMeshCompiled);
    }

    private void OnDisable()
    {
        pb_Editor.RemoveOnEditLevelChangedListener(OnEditLevelChanged);
        pb_EditorUtility.RemoveOnObjectCreatedListener(OnProBuilderObjectCreated);
        pb_EditorUtility.RemoveOnMeshCompiledListener(OnMeshCompiled);
        pb_Editor.OnSelectionUpdate -= OnSelectionUpdate;
        pb_Editor.OnVertexMovementBegin -= OnVertexMovementBegin;
        pb_Editor.OnVertexMovementFinish -= OnVertexMovementFinish;
    }

    private void OnProBuilderObjectCreated(pb_Object pb)
    {
        AddLog("Instantiated new ProBuilder Object: " + pb.name);
    }

    private void OnEditLevelChanged(int editLevel)
    {
        AddLog("Edit Level Changed: " + (EditLevel) editLevel);
    }

    private void OnSelectionUpdate(pb_Object[] selection)
    {
        AddLog("Selection Updated: " + string.Format("{0} objects and {1} vertices selected.",
                   selection != null ? selection.Length : 0,
                   selection != null ? selection.Sum(x => x.SelectedTriangleCount) : 0));
    }

    private void OnVertexMovementBegin(pb_Object[] selection)
    {
        AddLog("Began Moving Vertices");
    }

    private void OnVertexMovementFinish(pb_Object[] selection)
    {
        AddLog("Finished Moving Vertices");
    }

    private void OnMeshCompiled(pb_Object pb, Mesh mesh)
    {
        AddLog(string.Format("Mesh {0} rebuilt", pb.name));
    }

    private void AddLog(string summary)
    {
        logs.Add(summary);
        Repaint();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal(EditorStyles.toolbar);

        GUILayout.FlexibleSpace();

        GUI.backgroundColor = collapse ? disabledColor : Color.white;
        if (GUILayout.Button("Collapse", EditorStyles.toolbarButton))
            collapse = !collapse;
        GUI.backgroundColor = Color.white;

        if (GUILayout.Button("Clear", EditorStyles.toolbarButton))
            logs.Clear();

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Callback Log", EditorStyles.boldLabel);
        GUILayout.EndHorizontal();

        var r = GUILayoutUtility.GetLastRect();
        r.x = 0;
        r.y = r.y + r.height + 6;
        r.width = position.width;
        r.height = position.height;

        GUILayout.Space(4);

        pb_EditorGUIUtility.DrawSolidColor(r, logBackgroundColor);

        scroll = GUILayout.BeginScrollView(scroll);

        var len = logs.Count;
        var min = Math.Max(0, len - 1024);

        for (var i = len - 1; i >= min; i--)
        {
            if (collapse &&
                i > 0 &&
                i < len - 1 &&
                logs[i].Equals(logs[i - 1]) &&
                logs[i].Equals(logs[i + 1]))
                continue;

            GUILayout.Label(string.Format("{0,3}: {1}", i, logs[i]));
        }

        GUILayout.EndScrollView();
    }
}