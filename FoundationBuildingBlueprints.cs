using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class FoundationBuildingBlueprints : MonoBehaviour
{
    public bool canBuild;
    public Item CurItem;
    public NavMeshSurface navMeshSurface;
    public GameObject bluePrint;
    public GameObject buildPrefab;
    public Camera playerCam;
    public string curBuild;
    public RaycastHit hit;
    public Material bluePrintMaterial;
    public Material woodMaterial;
    public Material invisibleMat;
    public KeyCode buildBtn = KeyCode.Mouse0;
    private GameObject lastObj;
    private HandController handController;
    public HotbarManager hotbarManager;
    [SerializeField] private float buildRange = 50f;

    private int layer_mask;
    void Start()
    {
        // At start of game, bake navmesh
        navMeshSurface.BuildNavMesh();
        layer_mask = LayerMask.GetMask("Building");
        playerCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        /*hotbarManager = GetComponent<HotbarManager>();*/
        bluePrint = gameObject;
        canBuild = false;
        handController = GetComponent<HandController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canBuild)
        {
            
            Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * buildRange, Color.blue);
            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, buildRange))
            {
                if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Building"))
                {
                    if (hit.transform.gameObject != lastObj && lastObj != null && !lastObj.GetComponent<WallController>().isBuilt)
                    {
                        lastObj.GetComponent<MeshRenderer>().material = invisibleMat;
                        hotbarManager.isSnapping = false;
                    }
                    if (hit.transform.name == "SnapPoint" || hit.transform.name == "SnapPoint2" && curBuild == "Wall")
                    {
                        if (hit.transform.gameObject.GetComponent<WallController>())
                        {
                            if (!hit.transform.gameObject.GetComponent<WallController>().isBuilt)
                            {

                                lastObj = hit.transform.gameObject;
                                hit.transform.gameObject.GetComponent<MeshRenderer>().material = bluePrintMaterial;
                                if (Input.GetKeyDown(KeyCode.Mouse0))
                                {
                                    hit.transform.gameObject.GetComponent<MeshRenderer>().material = invisibleMat;
                                    /*hit.transform.GetComponent<WallController>().built();
                                    hit.transform.GetComponent<BoxCollider>().isTrigger = false;*/
                                    /*hotbarManager.ResetHands();*/

                                    
                                    hit.transform.GetComponent<WallController>().built(CurItem);
                                    GameObject newBuild = Instantiate(buildPrefab);
                                    newBuild.transform.position = hit.transform.gameObject.transform.position;
                                    if (hit.transform.name == "SnapPoint2")
                                    {
                                        newBuild.transform.Rotate(0, 90, 0);
                                    }
                                    hotbarManager.ResetHands();
                                    canBuild = false;
                                    ReMapNavMesh();
                                }

                            }
                        }
                    }
                    if (hit.transform.name == "FloorSnapPoint" && curBuild == "Floor")
                    {
                        if (!hit.transform.gameObject.GetComponent<WallController>().isBuilt)
                        {
                            hotbarManager.isSnapping = true;
                            lastObj = hit.transform.gameObject;
                            hit.transform.gameObject.GetComponent<MeshRenderer>().material = bluePrintMaterial;
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                hit.transform.gameObject.GetComponent<MeshRenderer>().material = invisibleMat;
                                /*hit.transform.GetComponent<WallController>().built();
                                hit.transform.GetComponent<BoxCollider>().isTrigger = false;*/
                                /*hotbarManager.ResetHands();*/


                                GameObject newBuild = Instantiate(buildPrefab);
                                newBuild.transform.position = hit.transform.gameObject.transform.position;
                                hotbarManager.ResetHands();
                                canBuild = false;
                                hit.transform.GetComponent<WallController>().built(CurItem);
                                ReMapNavMesh();
                            }

                        }
                        else
                        {
                            hotbarManager.isSnapping = false;
                        }

                    }
                    if (hit.transform.name == "RoofSnapPoint" && curBuild == "Floor")
                    {
                        if (!hit.transform.gameObject.GetComponent<WallController>().isBuilt && CanBuildRoof(hit))
                        {
                            hotbarManager.isSnapping = true;
                            lastObj = hit.transform.gameObject;
                            Debug.Log(hit.transform.parent.transform.Find("SnapPoint").GetType());
                            hit.transform.gameObject.GetComponent<MeshRenderer>().material = bluePrintMaterial;
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                hit.transform.gameObject.GetComponent<MeshRenderer>().material = invisibleMat;
                                /*hit.transform.GetComponent<WallController>().built();
                                hit.transform.GetComponent<BoxCollider>().isTrigger = false;*/
                                /*hotbarManager.ResetHands();*/


                                GameObject newBuild = Instantiate(buildPrefab);
                                newBuild.transform.position = hit.transform.gameObject.transform.position;
                                hotbarManager.ResetHands();
                                canBuild = false;
                                /*hit.transform.GetComponent<WallController>().item = CurItem;*/
                                hit.transform.GetComponent<WallController>().built(CurItem);
                                ReMapNavMesh();
                            }

                        }
                        else
                        {
                            hotbarManager.isSnapping = false;
                        }

                    }
                }
                else
                {
                    hotbarManager.isSnapping = false;

                }
                
            }
        }
    }
    public void ReMapNavMesh()
    {
        navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
    }
    public bool CanBuildRoof(RaycastHit rayhit)
    {
        int totalWallsBuilt = 0;
        Transform parent = rayhit.transform.parent;
        /*Debug.Log(parent.childCount);*/
        for(var i = 0; i < parent.childCount; i++)
        {
            if(parent.GetChild(i).name == "SnapPoint" && parent.GetChild(i).GetComponent<WallController>().isBuilt)
            {
                totalWallsBuilt++;
            }
        }
        /*Debug.Log(totalWallsBuilt);*/
        if (totalWallsBuilt >= 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

