using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class Train_Color : MonoBehaviour
{
    public Image[] targetImages;
    public string protocolorFilePath = "proto/protocolor.json";

    [System.Serializable]
    public class ProtocolorData
    {
        public string[] colors;
    }

    public void Start()
    {
        ApplyRandomColorToUIImage();
    }

    public void ApplyRandomColorToUIImage()
    {
        if (targetImages == null || targetImages.Length == 0)
        {
            Debug.LogError("Target Image components not assigned!");
            return;
        }

        string jsonPath = Path.Combine(Application.dataPath, protocolorFilePath);

        if (!File.Exists(jsonPath))
        {
            Debug.LogError($"Protocolor file not found at path: {jsonPath}");
            return;
        }

        string jsonContent = File.ReadAllText(jsonPath);
        ProtocolorData protocolorData = JsonUtility.FromJson<ProtocolorData>(jsonContent);

        if (protocolorData == null || protocolorData.colors.Length == 0)
        {
            Debug.LogError("Protocolor data is invalid or empty!");
            return;
        }

        if (targetImages.Length > protocolorData.colors.Length)
        {
            Debug.LogWarning("Number of targetImages is greater than the number of available colors. Some images may not receive a unique color.");
        }

        List<Color> assignedColors = new List<Color>();

        for (int i = 0; i < targetImages.Length; i++)
        {
            Color randomColor;

            do
            {
                string randomColorString = protocolorData.colors[Random.Range(0, protocolorData.colors.Length)];
                ColorUtility.TryParseHtmlString(randomColorString, out randomColor);
            } while (assignedColors.Contains(randomColor));

            assignedColors.Add(randomColor);

            targetImages[i].color = randomColor;
        }
    }
}