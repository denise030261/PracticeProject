using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ticketemblem : MonoBehaviour
{
    public List<Button> loadButtons; // 버튼 오브젝트를 인스펙터에서 연결해주세요.
    public Image imageElement; // UI 이미지 요소를 인스펙터에서 연결해주세요.
    public string imagesFolderPath; // 이미지 파일이 저장된 폴더 경로를 인스펙터에서 입력해주세요.
    public string jsonFilePath; // JSON 파일의 경로를 인스펙터에서 입력해주세요.

    private List<string> imageList; // JSON 파일에서 읽어온 이미지 파일 리스트

    void Start()
    {
        // 버튼마다 이벤트 리스너 추가
        for (int i = 0; i < loadButtons.Count; i++)
        {
            int index = i; // 클로저 변수로 인덱스 값을 유지하기 위해 필요
            loadButtons[i].onClick.AddListener(() => LoadRandomImage(index));
        }

        // JSON 파일에서 이미지 파일 리스트 읽어오기
        LoadImageListFromJSON();
    }

    // JSON 파일에서 이미지 파일 리스트 읽어오기
    private void LoadImageListFromJSON()
    {
        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            ImageData imageData = JsonUtility.FromJson<ImageData>(jsonData);
            imageList = imageData.emblemAssets;
        }
        else
        {
            Debug.LogError("JSON 파일을 찾을 수 없습니다: " + jsonFilePath);
        }
    }

    // 버튼 클릭 시 호출되는 함수
    private void LoadRandomImage(int buttonIndex)
    {
        if (imageList != null && imageList.Count > 0)
        {
            if (buttonIndex >= 0 && buttonIndex < loadButtons.Count)
            {
                if (imageElement != null)
                {
                    // 이미지 파일 리스트에서 랜덤으로 하나 선택
                    string randomImageName = imageList[Random.Range(0, imageList.Count)];

                    // 선택한 이미지 파일을 포함한 전체 경로 생성
                    string randomImagePath = Path.Combine(imagesFolderPath, randomImageName + ".png").Replace("\\", "/");

                    // 선택한 이미지 파일을 로드하여 UI 이미지 요소에 할당
                    LoadImageFromFile(randomImagePath, imageElement);
                }
                else
                {
                    Debug.LogWarning("UI 이미지 요소가 연결되지 않았습니다.");
                }
            }
            else
            {
                Debug.LogWarning("유효하지 않은 버튼 인덱스입니다.");
            }
        }
        else
        {
            Debug.LogWarning("이미지 파일 리스트가 비어있습니다.");
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
    private class ImageData
    {
        public List<string> emblemAssets;
    }
}