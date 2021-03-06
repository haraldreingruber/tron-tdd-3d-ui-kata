﻿using UnityEditor;
using UnityEngine;

namespace Editor
{
    // TODO (usability) - change Editor to be Tron editor to start Trail as well. - outdated
    // Now we have the button so we do not need it any more.
    [CustomEditor(typeof(RacingInteraction))]
    public class RacingInteractionEditor : UnityEditor.Editor
    {
        // idea taken from: https://docs.unity3d.com/Manual/editor-CustomEditors.html

        public override void OnInspectorGUI()
        {
            // TODO (Unity API) - use CreateInspectorGUI instead for new API of editor
            // See https://docs.unity3d.com/ScriptReference/Editor.CreateInspectorGUI.html
            base.OnInspectorGUI();

            if (GUILayout.Button("Start Racing"))
            {
                StartRaceButtonAction();
            }
        }

        private void StartRaceButtonAction()
        {
            var racingInteraction = target as RacingInteraction;
            if (racingInteraction == null)
            {
                Debug.Log("No racing interaction assigned");
                return;
            }

            racingInteraction.StartRace();
        }
    }
}