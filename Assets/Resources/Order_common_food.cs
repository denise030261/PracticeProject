using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class LevelData
{
    public string level;
    public float probability;
    public string jsonFilePath;
}

public class Order_common_food : MonoBehaviour
{
    public TextAsset levelData;
    public LevelData[] levels;
    public Button selectionButton;
    public Image selectedImage; // Reference to the UI Image component

    private Dictionary<string, int> inventory; // Dictionary to store inventory data

    private void Start()
    {
        ParseLevelData();

        selectionButton.onClick.AddListener(StartSelection);

        // Initialize inventory dictionary
        inventory = new Dictionary<string, int>();
    }

    private void ParseLevelData()
    {
        if (levelData != null)
        {
            ProbabilityData data = JsonUtility.FromJson<ProbabilityData>(levelData.text);
            levels = data.levels;

            for (int i = 0; i < levels.Length; i++)
            {
                string jsonFilePath = "JsonFiles/" + levels[i].level;
                levels[i].jsonFilePath = jsonFilePath;
            }
        }
        else
        {
            Debug.LogError("Level data not assigned.");
        }
    }

    private void StartSelection()
    {
        string selectedLevel = SelectLevel();

        LevelData selectedLevelData = GetLevelData(selectedLevel);

        if (selectedLevelData != null)
        {
            string jsonFilePath = selectedLevelData.jsonFilePath;
            TextAsset selectedJsonFile = Resources.Load<TextAsset>(jsonFilePath);

            if (selectedJsonFile != null)
            {
                LoadJson(selectedJsonFile);
                SaveInventoryToJson();
            }
            else
            {
                Debug.LogError("JSON file not found for selected level: " + selectedLevel);
            }
        }
        else
        {
            Debug.LogError("Invalid level selected.");
        }
    }

    private LevelData GetLevelData(string level)
    {
        foreach (LevelData levelData in levels)
        {
            if (levelData.level == level)
            {
                return levelData;
            }
        }

        return null;
    }

    private void LoadJson(TextAsset jsonFile)
    {
        if (jsonFile != null)
        {
            string jsonText = jsonFile.text;
            ItemDataList itemList = JsonUtility.FromJson<ItemDataList>(jsonText);

            if (itemList != null && itemList.items.Length > 0)
            {
                ItemData selectedItem = itemList.items[Random.Range(0, itemList.items.Length)];
                Debug.Log("Selected Item: " + selectedItem.name);

                string imageName = selectedItem.imageName;
                Sprite selectedSprite = Resources.Load<Sprite>(imageName);

                if (selectedSprite != null)
                {
                    selectedImage.sprite = selectedSprite;

                    // Add the obtained item to the inventory or increase its quantity if it already exists
                    if (inventory.ContainsKey(selectedItem.name))
                    {
                        inventory[selectedItem.name]++;
                    }
                    else
                    {
                        inventory[selectedItem.name] = 1;
                    }
                }
                else
                {
                    Debug.LogError("Image file not found: " + imageName);
                }
            }
            else
            {
                Debug.LogError("No items found in the JSON file.");
            }
        }
        else
        {
            Debug.LogError("JSON file not assigned.");
        }
    }

    private string SelectLevel()
    {
        float randomValue = Random.Range(0f, 100f);
        float sum = 0f;

        foreach (LevelData level in levels)
        {
            sum += level.probability;
            if (randomValue <= sum)
            {
                return level.level;
            }
        }

        return levels[0].level;
    }

    private void SaveInventoryToJson()
    {
        string jsonFilePath = Application.persistentDataPath + "/inventory.json";
        string jsonData = JsonUtility.ToJson(inventory);

        // Write the inventory data to the JSON file
        File.WriteAllText(jsonFilePath, jsonData);
    }
}

[System.Serializable]
public class ProbabilityData
{
    public LevelData[] levels;
}

[System.Serializable]
public class ItemDataList
{
    public ItemData[] items;
}

[System.Serializable]
public class ItemData
{
    public string myClass;
    public string sort;
    public string name;
    public string itemText;
    public string conv;
    public int firstStatType;
    public int firstStatValue;
    public int secondStatType;
    public int secondStatValue;
    public string imageName; // New field to hold the image file name
}
