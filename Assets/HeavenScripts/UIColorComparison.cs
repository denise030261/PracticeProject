using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UIColorComparison : MonoBehaviour
{
    public Image image1;
    public Image image2;
    public Image image3;
    public Image correctImage;
    public Image wrongImage;

    private string filePath;
    private int countValue;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);


        string folderPath = Path.Combine(Application.dataPath, "proto");
        filePath = Path.Combine(folderPath, "count.json");


        LoadJSON();


        countValue = LoadCountValue();
    }

    public void OnButtonClick()
    {
        if (image1.color != image2.color && image1.color != image3.color && image2.color != image3.color)
        {

            correctImage.gameObject.SetActive(true);
            wrongImage.gameObject.SetActive(false);


            countValue++;


            SaveCountValue(countValue);
        }
        else
        {

            correctImage.gameObject.SetActive(false);
            wrongImage.gameObject.SetActive(true);
        }
    }

    private void LoadJSON()
    {
        if (!File.Exists(filePath))
        {

            SaveCountValue(0);
        }
    }

    private int LoadCountValue()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            ColorComparisonData data = JsonUtility.FromJson<ColorComparisonData>(jsonData);
            return data.times;
        }
        else
        {
            return 0;
        }
    }

    private void SaveCountValue(int count)
    {
        ColorComparisonData data = new ColorComparisonData();
        data.times = count;

        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, jsonData);
    }
}

[System.Serializable]
public class ColorComparisonData
{
    public int times;
}
