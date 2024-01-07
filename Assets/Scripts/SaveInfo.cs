using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using JetBrains.Annotations;

public class SaveInfo : MonoBehaviour
{
    public MenuUIHandler MenuUIHandler;
    //can access this script from any other scripts
    public static SaveInfo Instance;

    //carries player name between scenes
    public string CurrentPlayer;
    
    //saves name and score to JSON
    public string HiScorePlayer;
    public int HiScore;
  
    private void Awake()
    {
        //Create instance on awake, destroys any extra instances, doesn't destroy instance between scenes
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        //Loads save file of HiScore and HiScorePlayer on awake (if it exists)
        LoadName();
    }

    [System.Serializable]
    class SaveData
    {
        public string HiScorePlayer;
        public int HiScore;
    }

    public void SaveName()
    {
        SaveData data = new SaveData();
 
        data.HiScorePlayer = HiScorePlayer;
        data.HiScore = HiScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "savefile.json"), json);
    }

    public void LoadName()
    {
        string path = Path.Combine(Application.persistentDataPath, "savefile.json");
        
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HiScorePlayer = data.HiScorePlayer;
            HiScore = data.HiScore;
        }
    }
    public void ClearName()
    {
        SaveData data = new SaveData();

        string NewName = "";
        int NewScore = 0;

        data.HiScorePlayer = NewName;
        data.HiScore = NewScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "savefile.json"), json);
    } 
}
