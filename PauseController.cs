using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public KeyCode pauseBtn;
    public GameObject pauseMenu;
    public PlayerMovement playerMovement;
    private bool isActive;
    void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseBtn))
        {
            ToggleMenu();
        } 
    }
    private void ToggleMenu()
    {
        isActive = !isActive;
        playerMovement.CanMove = (isActive) ?  false : true; 
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
    }
}
