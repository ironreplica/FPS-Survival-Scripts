using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void NewGame()
    {
        // delete old save file then load into game
        string savePath = Path.Combine(Application.persistentDataPath, "save.data");
        File.Delete(savePath);
        SceneManager.LoadScene(1);
    }
    public void LoadGame()
    {
        // load into game
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
