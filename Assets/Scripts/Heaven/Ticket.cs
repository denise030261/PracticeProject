using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class Ticket : MonoBehaviour
{
    public Image imageElement;  // 인스펙터에서 설정한 이미지 요소
    public Button button1;      // 인스펙터에서 설정한 버튼 1
    public Button button2;      // 인스펙터에서 설정한 버튼 2
    public Button button3;      // 인스펙터에서 설정한 버튼 3

    private List<Sprite> imageList;  // 이미지 리스트

    private void Start()
    {
        // 이미지 리스트 초기화
        LoadImages();

        // 초기 이미지 설정
        ChangeImage();

        // 버튼 클릭 이벤트 설정
        button1.onClick.AddListener(ChangeImage);
        button2.onClick.AddListener(ChangeImage);
        button3.onClick.AddListener(ChangeImage);
    }

    private void LoadImages()
    {
        imageList = new List<Sprite>();

        // 폴더 내의 이미지 파일들을 읽어와 리스트에 추가
        string folderPath = "Image/Heaven/tickets";
        Sprite[] sprites = Resources.LoadAll<Sprite>(folderPath);

        foreach (Sprite sprite in sprites)
        {
            imageList.Add(sprite);
        }
    }

    public void ChangeImage()
    {
        // 이미지 리스트에서 랜덤한 이미지 선택
        if (imageList.Count > 0)
        {
            int randomIndex = Random.Range(0, imageList.Count);
            Sprite randomImage = imageList[randomIndex];
            imageElement.sprite = randomImage;
        }
    }
}