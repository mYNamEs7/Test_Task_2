using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    [HideInInspector] public int bullets;
    [HideInInspector] public float health;
    [HideInInspector] public List<Sprite> itemIcons;

    public Sprite[] startItemIcons;

    private void Awake()
    {
        Instance = this;

        LoadData();
    }

    private void LoadData()
    {
        GameData gameData = GetComponent<GameData>();
        FileDataHandler fileDataHandler = new(Application.persistentDataPath, "data.json");
        fileDataHandler.Load(ref gameData);
    }

    public void SaveData()
    {
        GameData gameData = GetComponent<GameData>();
        FileDataHandler fileDataHandler = new(Application.persistentDataPath, "data.json");
        fileDataHandler.Save(gameData);
    }
}
