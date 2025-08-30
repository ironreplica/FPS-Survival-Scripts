using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HealthController))]
public class HealthControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        HealthController healthController = (HealthController)target;

        if (string.IsNullOrEmpty(healthController.SaveId))
        {
            if(GUILayout.Button("Generate GUID"))
            {
                healthController.SaveId = System.Guid.NewGuid().ToString();
                EditorUtility.SetDirty(healthController);
            }
        }
    }
}
