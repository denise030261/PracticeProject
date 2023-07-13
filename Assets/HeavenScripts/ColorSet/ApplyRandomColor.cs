using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ApplyRandomColor : MonoBehaviour
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

        for (int i = 0; i < targetImages.Length; i++)
        {
            string randomColor = protocolorData.colors[Random.Range(0, protocolorData.colors.Length)];
            Color color;
            if (ColorUtility.TryParseHtmlString(randomColor, out color))
            {
                targetImages[i].color = color;
            }
        }
    }
}