using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class UserData
{
    public List<string> inven;
    public string name;
    public int gold;
    public int dia;
    public List<string> dog_list;
    public List<string> food_list;
    public int favor;
    public string Date;

    public static UserData FromJson(string json)
    {
        return JsonUtility.FromJson<UserData>(json);
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}

public class YourScriptName : MonoBehaviour
{
    private UserData userData;
    private string savePath; // JSON FILE PATH

    private void Awake()
    {
        savePath = Application.dataPath + "/DataFile/UserData.json";
    }

    private void Start()
    {
        // USER_DATA_SETTING(DEFAULT)
        userData = new UserData();
        userData.inven = new List<string>() { "item1", "item2" };
        userData.name = "DEFAULT";
        userData.gold = 100;
        userData.dia = 10;
        userData.dog_list = new List<string>() { "dog1", "dog2" };
        userData.food_list = new List<string>() { "food1", "food2" };
        userData.favor = 5;
        userData.Date = DateTime.Now.ToString();

        // SAVE_USER_DATA
        SaveUserData(userData);

        // LOAD_USER_DATA
        UserData loadedData = LoadUserData();
        if (loadedData != null)
        {
            Debug.Log("Loaded name: " + loadedData.name);
            Debug.Log("Loaded gold: " + loadedData.gold);
            //ADDITIONAL CODE PROGRAMMER SHOULD INPUT
        }
        else
        {
            Debug.Log("Failed to load user data.");
        }
    }

    private void SaveUserData(UserData data)
    {
        string json = data.ToJson();
        File.WriteAllText(savePath, json);
    }

    private UserData LoadUserData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return UserData.FromJson(json);
        }
        return null;
    }
}
