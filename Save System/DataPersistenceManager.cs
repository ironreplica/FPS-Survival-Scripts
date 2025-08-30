using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    // Can get the instance publicly but cannot set it publicly
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more then one DPM in the scene.");
        }
        instance = this;
    }
    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }
    private void OnApplicationQuit()
    {
        
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void SaveGame()
    {
        // pass the data to other scripts so they can update the save
        // save the data to a file using datahandler
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            // https://www.youtube.com/watch?v=aUi9aijvpgs&t=72s left off at 9.31
            dataPersistenceObj.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
    }
    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        // Load any saved data froma file using data handler
        // if there is no save, create a new game save
        if(this.gameData == null)
        {
            Debug.Log("No game save found. Creating a new file...");
            NewGame();
        }
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        // push loaded data to all scripts that need to access the data
    }
    private List<IDataPersistence> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
