using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RacingInteraction))]
public class RacingInteractionEditor : Editor
{
    // idea taken from: https://docs.unity3d.com/Manual/editor-CustomEditors.html

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Start Racing"))
        {
            var racingInteraction = (target as RacingInteraction);
            if (racingInteraction == null)
            {
                Debug.Log("No racing interaction assigned");
                return;
            }

            racingInteraction.StartRace();
        }
    }
}