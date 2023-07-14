using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class ticketcompact : MonoBehaviour
{
    public List<Button> loadButtons;
    public Image emblemImageElement;
    public Image sortImageElement;
    public Text textElement;
    public string emblemImagesFolderPath;
    public string sortImagesFolderPath;
    public string setJsonFilePath;
    public string recordJsonFilePath;

    private List<Set> setDataList;

    private void Start()
    {
        LoadData();
        SetupButtonListeners();
        LoadTexts();
    }

    private void LoadData()
    {
        LoadTexts();
    }

    private void LoadTexts()
    {
        if (File.Exists(setJsonFilePath))
        {
            string jsonData = File.ReadAllText(setJsonFilePath);
            SetData setData = JsonUtility.FromJson<SetData>(jsonData);
            if (setData != null)
            {
                setDataList = setData.Sets;
                Debug.Log("Set.json 파일 읽기 성공!");
            }
            else
            {
                Debug.LogError("Set.json 데이터 변환 실패!");
            }
        }
        else
        {
            Debug.LogError("Set.json 파일을 찾을 수 없습니다: " + setJsonFilePath);
        }
    }

    private void SetupButtonListeners()
    {
        for (int i = 0; i < loadButtons.Count; i++)
        {
            int index = i;
            loadButtons[i].onClick.AddListener(() => LoadRandomImage(index));
        }
    }

    private void LoadRandomImage(int buttonIndex)
    {
        if (setDataList != null && setDataList.Count > 0)
        {
            if (buttonIndex >= 0 && buttonIndex < loadButtons.Count)
            {
                if (emblemImageElement != null && sortImageElement != null && textElement != null)
                {
                    int randomIndex = Random.Range(0, setDataList.Count);
                    Set randomSet = setDataList[randomIndex];

                    Debug.Log("Button " + buttonIndex + " 클릭 - Color: " + randomSet.Color + ", Emblem: " + randomSet.emblemAssets + ", Sort: " + randomSet.Sort);

                    string emblemImagePath = Path.Combine(emblemImagesFolderPath, randomSet.emblemAssets + ".png").Replace("\\", "/");
                    string sortImagePath = Path.Combine(sortImagesFolderPath, randomSet.Color + ".png").Replace("\\", "/");

                    LoadImageFromFile(emblemImagePath, emblemImageElement);
                    LoadImageFromFile(sortImagePath, sortImageElement);

                    textElement.text = randomSet.Sort;

                    // record.json 파일의 color 값을 업데이트
                    UpdateColorInJson(randomSet.Color);
                }
                else
                {
                    Debug.LogWarning("UI 요소가 연결되지 않았습니다.");
                }
            }
            else
            {
                Debug.LogWarning("유효하지 않은 버튼 인덱스입니다.");
            }
        }
        else
        {
            Debug.LogWarning("데이터 리스트가 비어있습니다.");
        }
    }

    private void LoadImageFromFile(string filePath, Image targetImage)
    {
        if (File.Exists(filePath))
        {
            byte[] imageData = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            targetImage.sprite = sprite;
        }
        else
        {
            Debug.LogError("이미지 파일을 찾을 수 없습니다: " + filePath);
        }
    }

    private void UpdateColorInJson(string color)
    {
        if (File.Exists(recordJsonFilePath))
        {
            string jsonData = File.ReadAllText(recordJsonFilePath);
            RecordData recordData = JsonUtility.FromJson<RecordData>(jsonData);

            if (recordData != null)
            {
                // 기존 present에 있던 데이터를 past로 옮깁니다.
                recordData.past = recordData.present;

                // 새로운 color 값을 present에 할당합니다.
                recordData.present = color;

                string updatedJsonData = JsonUtility.ToJson(recordData);
                File.WriteAllText(recordJsonFilePath, updatedJsonData);

                Debug.Log("record.json 파일 업데이트 성공!");
            }
            else
            {
                Debug.LogError("record.json 데이터 변환 실패!");
            }
        }
        else
        {
            Debug.LogError("record.json 파일을 찾을 수 없습니다: " + recordJsonFilePath);
        }
    }

    [System.Serializable]
    private class SetData
    {
        public List<Set> Sets;
    }

    [System.Serializable]
    private class Set
    {
        public string Color;
        public string emblemAssets;
        public string Sort;
    }

    [System.Serializable]
    private class RecordData
    {
        public string present;
        public string past;
    }
}
