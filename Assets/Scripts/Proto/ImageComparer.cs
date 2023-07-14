using UnityEngine;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class RecordData
{
    public string present;
    public string past;
}

public class ImageComparer : MonoBehaviour
{
    public Image assignedImage;
    public string jsonFilePath = "JsonFiles/record";
    public Button compareButton;

    private RecordData recordData;
    private string assignedImageFileName;

    private void Start()
    {
        compareButton.onClick.AddListener(CompareImageAndJson);
        LoadJsonData();
    }

    private void LoadJsonData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFilePath);
        if (jsonFile != null)
        {
            recordData = JsonUtility.FromJson<RecordData>(jsonFile.text);
        }
        else
        {
            Debug.LogError("Failed to load JSON file: " + jsonFilePath);
        }
    }

    private void CompareImageAndJson()
    {
        if (assignedImage == null)
        {
            Debug.LogError("No image assigned.");
            return;
        }

        string assignedImagePath = GetImageAssetPath(assignedImage.sprite);
        assignedImageFileName = Path.GetFileNameWithoutExtension(assignedImagePath);

        if (assignedImageFileName.ToLower().Contains(recordData.past.ToLower()))
        {
            Debug.Log("The assigned image path contains the past field value!");
        }
        else
        {
            Debug.Log("The assigned image path does not contain the past field value.");
        }

        Debug.Log("Assigned Image Path: " + assignedImagePath);
        Debug.Log("Present Field: " + recordData.present);
        Debug.Log("Past Field: " + recordData.past);
    }

    private string GetImageAssetPath(Sprite sprite)
    {
        string assetPath = sprite.texture.name;
        return assetPath;
    }
}
