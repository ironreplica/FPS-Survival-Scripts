using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaplingManager : MonoBehaviour, IDataPersistence
{
    public void LoadData(GameData data)
    {
        throw new System.NotImplementedException();
    }

    public void SaveData(ref GameData data)
    {
        /*foreach (IPlantData plantData in GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IPlantData>())
        {
            GameObject sapling = (plantData as MonoBehaviour).gameObject;
            // Your code here
        }*/
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
