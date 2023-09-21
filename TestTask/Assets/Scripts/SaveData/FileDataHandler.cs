using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class FileDataHandler : MonoBehaviour
{
    private readonly string dataDirPath;
    private readonly string dataFileName;

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public void Load(ref GameData gameData)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        gameData.bullets = 20;
        gameData.health = 100;
        gameData.itemIcons = gameData.startItemIcons.ToList();

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                using FileStream stream = new(fullPath, FileMode.Open);
                {
                    using StreamReader reader = new(stream);
                    dataToLoad = reader.ReadToEnd();
                }

                JsonUtility.FromJsonOverwrite(dataToLoad, gameData);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }

    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            using FileStream stream = new(fullPath, FileMode.Create);
            using StreamWriter writer = new(stream);
            writer.Write(dataToStore);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
