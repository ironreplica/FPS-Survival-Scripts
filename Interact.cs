using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    RaycastHit hit;
    private LayerMask layer_mask;
    private Camera playerCam;
    public float interactRange = 10f;
    public GameObject interactText;
    public bool isOpen = false;
    public KeyCode keyCode = KeyCode.E;
    public GameObject horde;
    private bool hordeActive;
    public AudioSource hordeSound;
    void Start()
    {
        layer_mask = LayerMask.GetMask("Interactable");
        hordeActive = false;
        playerCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, interactRange)){
            if (hit.transform.CompareTag("Interactable"))
            {
                interactText.SetActive(true);
                if (Input.GetKeyDown(keyCode) && !isOpen)
                {
                    isOpen = true;
                    hit.transform.GetComponent<StorageBox>().OpenStorage();
                }
            }
            else
            {
                interactText.SetActive(false);
            }
        }
        else
        {
            interactText.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.Backspace) && !hordeActive)
        {
            horde.SetActive(true);
            hordeActive = true;
            hordeSound.Play();
        }
    }
}
