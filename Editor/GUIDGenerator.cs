using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class GUIDGenerator : Editor
{
    [MenuItem("Tools/Gemerate GUIDS for HealthControllers")]
    public static void GenerateGUIDs()
    {
        HealthController[] healthControllers = GameObject.FindObjectsOfType<HealthController>();
        int count = 0;

        foreach (HealthController controller in healthControllers)
        {
            if (controller.SaveId != null)
            {
                count++;
                // Accessing SaveId will generate a new GUID if it doesn't exist
                string guid = controller.SaveId;
                controller.SaveId = System.Guid.NewGuid().ToString();
                EditorUtility.SetDirty(controller);
            }
        }
        if (count > 0)
        {
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }
}
