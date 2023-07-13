using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ButtonClickScript : MonoBehaviour
{
    public Image image1; 
    public Image image2; 
    public Image correctImage;
    public Image wrongImage;

    private string filePath;

    private class JsonData
    {
        public int times;
    }

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);

        
        filePath = Path.Combine(Application.dataPath, "proto", "count.json");
    }

    public void OnButtonClick()
    {
        if (image1.color.Equals(image2.color))
        {
            
            correctImage.gameObject.SetActive(true);
            wrongImage.gameObject.SetActive(false);
            IncrementTimes();
        }
        else
        {
            
            correctImage.gameObject.SetActive(false);
            wrongImage.gameObject.SetActive(true);
        }
    }

    private void IncrementTimes()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            JsonData data = JsonUtility.FromJson<JsonData>(jsonData);
            if (data != null)
            {
                data.times++;
                string updatedJsonData = JsonUtility.ToJson(data);
                File.WriteAllText(filePath, updatedJsonData);
            }
            else
            {
                Debug.LogError("Failed to parse JSON data.");
            }
        }
        else
        {
            Debug.LogError("JSON file not found at path: " + filePath);
        }
    }
}
