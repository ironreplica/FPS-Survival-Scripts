using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : MonoBehaviour, IDataPersistence
{
    public GameObject safePrefab;
    public ItemManager itemManager;

    public GameData furnacePrefab;
    public void LoadData(GameData data)
    {
        foreach(KeyValuePair<string, GameData.SafeData> kvp in data.containers)
        {
            GameObject loadingSafe = Instantiate(safePrefab);
            loadingSafe.transform.parent = null;
            loadingSafe.transform.position = kvp.Value.Position;

            foreach(KeyValuePair<int,int> kvp2 in kvp.Value.Slots)
            {
                
                if (kvp2.Value != -1)
                {
                    loadingSafe.GetComponent<StorageBox>().slots[kvp2.Key] = itemManager.items[kvp2.Value];

                }
                else
                {
                    loadingSafe.GetComponent<StorageBox>().slots[kvp2.Key] = null;
                }
            }
        }
        
    }

    public void SaveData(ref GameData data)
    {
        StorageBox[] containers = GameObject.FindObjectsOfType<StorageBox>();
        foreach(StorageBox container in containers)
        {
            if (data.containers.ContainsKey(container.SaveId))
            {
                int[] slotsToSave = new int[24];
                for (var i = 0; i < container.slots.Length; i++)
                {
                    if (container.slots[i] == null)
                    {
                        data.containers[container.SaveId].Slots[i] = -1;
                    }
                    else
                    {
                        data.containers[container.SaveId].Slots[i] = container.slots[i].itemId;
                    }
                }
                data.containers[container.SaveId].Position = container.transform.position;
            }
            else
            {
                GameData.SafeData safeData = new GameData.SafeData(container.transform.position);
                

                int[] slotsToSave = new int[24];
                for (var i = 0; i < container.slots.Length; i++)
                {
                    if (container.slots[i] == null)
                    {
                        safeData.Slots[i] = -1;
                    }
                    else
                    {
                        safeData.Slots[i] = container.slots[i].itemId;
                    }
                }
                data.containers[container.SaveId] = safeData;
            }
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
