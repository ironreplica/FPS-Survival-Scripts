using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class FileDataHandler 
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }
    public GameData Load()
    {
        string fullPath = Path.Join(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // deserializing the data from json into the obj
                loadedData = JsonConvert.DeserializeObject<GameData>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("Error occured loading the data from file: " + "\n" + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }
    public void Save(GameData data)
    {
        string fullPath = Path.Join(dataDirPath, dataFileName);
        try
        {
            // creating a dir the file will be written to
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // serailizing to json
            string dataToStore = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            // writing the serialized data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Error occured saving data to file: " + fullPath + "\n" + e);
        }
    }
}
