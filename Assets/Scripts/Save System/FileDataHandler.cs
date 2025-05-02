using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class FileDataHandler
{
    private string dataDirPath = "";    // where to save the data
    private string dataFileName = "";   // name of save file

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string path = Path.Combine(dataDirPath, dataFileName);

        GameData loadedData = null;

        if (File.Exists(path))
        {
            try
            {
                // load the serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // deserialize the data
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);

            } catch (Exception e)
            {
                Debug.LogError($"Error trying to load from file {path}: \n{e}");
            }
        }

        return loadedData;
    }

    public void Save(GameData data)
    {
        string path = Path.Combine(dataDirPath, dataFileName);
        try
        {
            // create the dir the file will be written to if its not already created
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            // serialize the C# obj to json
            string dataToStore = JsonUtility.ToJson(data, true);

            //write the serialized data to the file
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error trying to save the file {path}: \n{e}");
        }
    }
}
