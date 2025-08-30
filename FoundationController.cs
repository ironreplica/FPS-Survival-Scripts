using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundationController : MonoBehaviour
{
    public List<IsColliding> snapPoints;
    private HandController handController;
    public GameObject foundationPrefab;
    public HotbarManager hotBarManager;
    public Material canBuild;
    public Material cantBuild;
    public KeyCode place = KeyCode.Mouse0;
    public bool canPlaceObj;

    void Start()
    {
        hotBarManager = GameObject.Find("ItemManager").GetComponent<HotbarManager>();
        handController = GameObject.Find("Player").GetComponent<HandController>();
        canPlaceObj = true;
        handController.canAttack = false;
        gameObject.GetComponent<MeshRenderer>().material = cantBuild;
        /*for(var i = 0; i < 4; i++)
        {
            snapPoints[i] = transform.GetChild(i).GetComponent<IsColliding>();
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        /*Snap1 = snapPoints[0].isColliding;
        Snap2 = snapPoints[1].isColliding;
        Snap3 = snapPoints[2].isColliding;
        Snap4 = snapPoints[3].isColliding;*/
        /* Make this general usage so i can place an object with n number of snap points */
        /*canPlace = snapPoints.All(snap => snap.isColliding() == true);*/
        if (canPlace() && canPlaceObj)
        {
            gameObject.GetComponent<MeshRenderer>().material = canBuild;
            if (Input.GetKeyDown(place) )
            {
                Place();
            }
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = cantBuild;
        }
    }
    private bool canPlace()
    {
        bool allTrue = true;
        foreach(IsColliding col in snapPoints)
        {
            if (col.isColliding)
            {
                allTrue = false;
                break;
            }
            continue;
        }
        return allTrue;
    }
    private void Place()
    {
        GameObject newFoundation = Instantiate(foundationPrefab);
        newFoundation.transform.parent = null;
        newFoundation.transform.position = new Vector3(GetComponent<PlaceBlueprint>().buildPos.x, GetComponent<PlaceBlueprint>().buildPos.y, GetComponent<PlaceBlueprint>().buildPos.z);
        /*newFoundation.transform.position.y += newFoundation.transform.localScale.y / 2;*/
        /*newFoundation.layer = null;*/
        /*newFoundation.layer = LayerMask.NameToLayer("Building");*/
        handController.GetComponent<FoundationBuildingBlueprints>().canBuild = false;
        hotBarManager.ResetHands();
        handController.GetComponent<FoundationBuildingBlueprints>().ReMapNavMesh();
        handController.canAttack = true;
        Destroy(gameObject);
    }
}
