using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class ticketcompact : MonoBehaviour
{
    public List<Button> loadButtons; // 버튼 오브젝트를 인스펙터에서 연결해주세요.
    public Image emblemImageElement; // 엠블렘 이미지 요소를 인스펙터에서 연결해주세요.
    public Image sortImageElement; // 소트 이미지 요소를 인스펙터에서 연결해주세요.
    public Text textElement; // 텍스트 요소를 인스펙터에서 연결해주세요.
    public string emblemImagesFolderPath; // 엠블렘 이미지 파일이 저장된 폴더 경로를 인스펙터에서 입력해주세요.
    public string sortImagesFolderPath; // 소트 이미지 파일이 저장된 폴더 경로를 인스펙터에서 입력해주세요.
    public string jsonFilePath; // JSON 파일의 경로를 인스펙터에서 입력해주세요.

    private List<Set> setDataList; // JSON 파일에서 읽어온 데이터 리스트

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
        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            SetData setData = JsonUtility.FromJson<SetData>(jsonData);
            if (setData != null)
            {
                setDataList = setData.Sets;
                Debug.Log("JSON 파일 읽기 성공!");
            }
            else
            {
                Debug.LogError("JSON 데이터 변환 실패!");
            }
        }
        else
        {
            Debug.LogError("JSON 파일을 찾을 수 없습니다: " + jsonFilePath);
        }
    }

    private void SetupButtonListeners()
    {
        for (int i = 0; i < loadButtons.Count; i++)
        {
            int index = i; // 클로저 변수로 인덱스 값을 유지하기 위해 필요
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
                    // 데이터 리스트에서 랜덤으로 하나 선택
                    int randomIndex = Random.Range(0, setDataList.Count);
                    Set randomSet = setDataList[randomIndex];

                    Debug.Log("Button " + buttonIndex + " 클릭 - Color: " + randomSet.Color + ", Emblem: " + randomSet.emblemAssets + ", Sort: " + randomSet.Sort);

                    // 선택한 이미지 파일을 포함한 전체 경로 생성
                    string emblemImagePath = Path.Combine(emblemImagesFolderPath, randomSet.emblemAssets + ".png").Replace("\\", "/");
                    string sortImagePath = Path.Combine(sortImagesFolderPath, randomSet.Color + ".png").Replace("\\", "/");

                    // 선택한 이미지 파일을 로드하여 UI 이미지 요소에 할당
                    LoadImageFromFile(emblemImagePath, emblemImageElement);
                    LoadImageFromFile(sortImagePath, sortImageElement);

                    // 텍스트 요소에 텍스트 할당
                    textElement.text = randomSet.Sort;
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

    // 파일에서 이미지 로드
    private void LoadImageFromFile(string filePath, Image targetImage)
    {
        if (File.Exists(filePath))
        {
            byte[] imageData = File.ReadAllBytes(filePath);

            // 이미지를 Texture2D로 변환
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData);

            // Texture2D를 Sprite로 변환하여 UI 이미지 요소에 할당
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            targetImage.sprite = sprite;
        }
        else
        {
            Debug.LogError("이미지 파일을 찾을 수 없습니다: " + filePath);
        }
    }

    // JSON 데이터 클래스
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
}
