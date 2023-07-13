using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class JSON_Manager : MonoBehaviour
{
    public string jsonFilePath = "DataFile/foodinfo.json"; // JSON Data Path

    void Start()
    {
        // JSON Load
        LoadJSONFromFile(jsonFilePath);
    }

    // Load JSON From File
    void LoadJSONFromFile(string filePath)
    {
        string jsonText = null;

        string fullPath = Path.Combine(Application.dataPath, filePath);

        if (File.Exists(fullPath))
        {
            jsonText = File.ReadAllText(fullPath);

            // JSON to Object
            MyDataWrapper dataWrapper = JsonUtility.FromJson<MyDataWrapper>(jsonText);
            List<MyDataObject> dataObjects = dataWrapper.data;

            // Data logging
            foreach (MyDataObject dataObject in dataObjects)
            {
                Debug.Log("Num: " + dataObject.num);
                Debug.Log("Class: " + dataObject.myClass);
                Debug.Log("Sort: " + dataObject.sort);
                Debug.Log("Name: " + dataObject.name);
                Debug.Log("Item Text: " + dataObject.itemText);
                Debug.Log("Conv: " + dataObject.conv);
                Debug.Log("1st Stat Type: " + dataObject.firstStatType);
                Debug.Log("1st Stat Value: " + dataObject.firstStatValue);
                Debug.Log("2nd Stat Type: " + dataObject.secondStatType);
                Debug.Log("2nd Stat Value: " + dataObject.secondStatValue);
                Debug.Log("Probability: " + dataObject.probability);
            }
        }
        else
        {
            Debug.Log("Error loading JSON file: File not found");
        }
    }
}

[System.Serializable]
public class MyDataWrapper
{
    public List<MyDataObject> data;
}

[System.Serializable]
public class MyDataObject
{
    public int num;
    public string myClass;
    public string sort;
    public string name;
    public string itemText;
    public string conv;
    public int firstStatType;
    public int firstStatValue;
    public int secondStatType;
    public int secondStatValue;
    public float probability;
}
