using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBlueprint : MonoBehaviour
{
    public GameObject bluePrint;
    public Camera playerCam;
    private RaycastHit hit;
    public Vector3 buildPos;
    [SerializeField] private float buildRange = 50f;
    
    private int layer_mask;
    private int layer_mask2;
    void Start()
    {
        layer_mask = LayerMask.GetMask("Ground");
        layer_mask2 = LayerMask.GetMask("Building");
        playerCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        bluePrint = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * buildRange, Color.blue);
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, buildRange, layer_mask ))
        {
               
                buildPos = new Vector3(hit.point.x, hit.point.y + gameObject.transform.localScale.y / 2, hit.point.z);
                bluePrint.transform.position = buildPos;
        }
    }
}
