using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldHealthObjectSaveSystem : MonoBehaviour, IDataPersistence
{

    public void LoadData(GameData data)
    {
        HealthController[] controllers = GameObject.FindObjectsOfType<HealthController>();
        if (data.worldHealthObjs != null)
        {
            foreach(KeyValuePair<string, bool> kvp in data.worldHealthObjs)
            {
                if(kvp.Key != null)
                {
                    if(kvp.Value == false)
                    {
                        // Keep it, do nothing
                    }
                    else if(kvp.Value == true)
                    {
                        // Delete it from the scene, but keep it in storage
                        foreach(HealthController controller in controllers)
                        {
                            if(controller.SaveId == kvp.Key)
                            {
                                Destroy(controller.gameObject);
                            }
                        }
                    }
                }
                else
                {
                    Debug.LogError("Empty health object string found when loading the data...");
                }
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        // You need a save and exit function, it does not have enough time to save all of this data.
        HealthController[] controllers = GameObject.FindObjectsOfType<HealthController>();
        if(data.worldHealthObjs != null)
        {
            foreach (HealthController controller in controllers)
            {
                // modify all values
                if (data.worldHealthObjs.ContainsKey(controller.SaveId))
                {
                    if (controller.IsBroken)
                    {
                        Debug.Log("is broken");
                        data.worldHealthObjs[controller.SaveId] = controller.IsBroken;
                    }
                }
                else if (!data.worldHealthObjs.ContainsKey(controller.SaveId))
                {
                    data.worldHealthObjs.Add(controller.SaveId, controller.IsBroken);
                }
            }
        }
        
    }


    void Start()
    {

        HealthController[] healthControllers = GameObject.FindObjectsOfType<HealthController>();
    }


    void Update()
    {
        
    }
}
