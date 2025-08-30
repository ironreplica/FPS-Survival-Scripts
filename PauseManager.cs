using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private DataPersistenceManager dataPersistancceManager;
    private void Start()
    {
        dataPersistancceManager = transform.parent.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>();
    }
    public void SaveGame()
    {
        dataPersistancceManager.SaveGame();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ResumeGame()
    {

    }
    public void Options()
    {

    }

}

