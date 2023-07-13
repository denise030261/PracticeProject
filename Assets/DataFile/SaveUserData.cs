using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class SaveUserData : MonoBehaviour
{
    public InputField inputField;
    public Button saveButton;

    private string filePath;

    private void Awake()
    {
        filePath = Path.Combine(Application.dataPath, "DataFile/UserData.json");
        saveButton.onClick.AddListener(SaveData);
    }

    private void SaveData()
    {
        // Load existing data if available
        UserData userData = LoadData();

        // Update data with new input
        if (userData != null)
        {
            userData.name = inputField.text;

            // Convert to JSON
            string jsonData = JsonUtility.ToJson(userData);

            // Save data to file
            File.WriteAllText(filePath, jsonData);

            Debug.Log("Data saved to: " + filePath);
        }
        else
        {
            Debug.LogError("Failed to load existing data.");
        }
    }

    private UserData LoadData()
    {
        if (File.Exists(filePath))
        {
            // Read existing data from file
            string jsonData = File.ReadAllText(filePath);
            UserData userData = JsonUtility.FromJson<UserData>(jsonData);
            return userData;
        }
        else
        {
            Debug.LogWarning("Data file does not exist. Creating a new one.");
            return new UserData();
        }
    }
}
