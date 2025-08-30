using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainUtils;
public class TreeCreator : MonoBehaviour
{
    public GameObject treePrefab;
    void Start()
    {
        TerrainData terrain;
        terrain = gameObject.GetComponent<Terrain>().terrainData;

        foreach (TreeInstance tree in terrain.treeInstances)
        {
            Vector3 worldTreePosition = Vector3.Scale(tree.position, terrain.size) + Terrain.activeTerrain.transform.position;
            Instantiate(treePrefab, worldTreePosition, Quaternion.identity);
        }

        List<TreeInstance> newTrees = new List<TreeInstance>(0);
        terrain.treeInstances = newTrees.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
