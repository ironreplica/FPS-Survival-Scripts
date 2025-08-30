using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSaveManager : MonoBehaviour, IDataPersistence
{
    [Header("All Buildables")]
    [SerializeField] public Dictionary<string, Item> allBuildables;
    [SerializeField] private ItemManager itemManager;
    

    public void LoadData(GameData data)
    {
        foreach(KeyValuePair<string, GameData.FoundationData> kvp in data.foundations)
        {
            GameObject foundation = Instantiate(itemManager.items[kvp.Value.BuildID].placedObjectPrefab);
            foundation.transform.parent = null;

            foundation.transform.position = kvp.Value.Position;

            foreach(KeyValuePair<int, int> kvp2 in kvp.Value.WallPositions)
            {
                if(kvp2.Value != -1)
                {
                    GameObject structure = Instantiate(itemManager.items[kvp2.Value].placedObjectPrefab);
                    structure.transform.position = foundation.transform.GetChild(kvp2.Key).position;
                    foundation.transform.GetChild(kvp2.Key).GetComponent<WallController>().isBuilt = true;

                    if(foundation.transform.GetChild(kvp2.Key).transform.name == "SnapPoint2")
                    {
                        structure.transform.Rotate(0, 90, 0);
                    }
                }
            }
        }
        
    }

    public void SaveData(ref GameData data)
    {
       
       BuildingState[] buildingStates = GameObject.FindObjectsOfType<BuildingState>();
       foreach(BuildingState state in buildingStates)
        {
            if (data.foundations.ContainsKey(state.SaveId))
            {
                // data.foundations[state.SaveId].WallPositions update wall positions
            }
            else
            {
                GameData.FoundationData foundationData = new GameData.FoundationData(state.transform.position);

                foundationData.WallPositions = state.GetAllWallControllers();

                foundationData.BuildID = state.gameObject.GetComponent<BuildInfo>().item.itemId;
                data.foundations[state.SaveId] = foundationData;
            }
            
        }
    }
}
