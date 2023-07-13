using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InvenotrySlot : MonoBehaviour
{
    public GameObject[] SelectedBoundary;
    public Image[] SelectedImage;
    public Button[] buttons;
    public GameObject[] Icon;

    private int[] IsCheck = new int[18]; // 눌렀는지 안 눌렀는지
    private int SelectedNum; // 선택된 순서
    private Image EmptyImage; // 빈 슬롯

    public static UI_InvenotrySlot Instance { get; private set; } = null;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        Initialized();
    }
    public void Selected(Button button)
    {
        for (int i=0;i<18;i++)
        {
            if (buttons[i] == button)
            {
                if (IsCheck[i] == 0 && SelectedImage[i].GetComponent<Image>().sprite != null)
                {
                    IsCheck[i] = 1;
                    SelectedBoundary[i].SetActive(true);
                    UI_InventoryInformation.Instance.SwitchImage(SelectedImage[i]);
                    SelectedNum = i;
                } // 선택할 때 이미지가 있는 경우
                else if (IsCheck[i] == 0 && SelectedImage[i].GetComponent<Image>().sprite == null)
                {
                    UI_InventoryInformation.Instance.SwitchImageNull();
                } // 선택할 때 이미지가 없는 경우
                else if (IsCheck[i] == 1)
                {
                    IsCheck[i] = 0;
                    SelectedBoundary[i].SetActive(false);
                    UI_InventoryInformation.Instance.SwitchImageNull();
                } // 선택 취소될 때 테두리와 이미지 상태 사라짐
            }
            else if (IsCheck[i] == 1)
            {
                IsCheck[i] = 0;
                SelectedBoundary[i].SetActive(false);
            } // 버튼 누르지 않는 것들은 해제
        }
    }
    public void Deleted()
    {
        SelectedImage[SelectedNum].GetComponent<Image>().sprite = null;
        Icon[SelectedNum].SetActive(false);
        SelectedBoundary[SelectedNum].SetActive(false);
        UI_InventoryInformation.Instance.SwitchImageNull();
    } // 인벤토리 이미지 및 큰 이미지 삭제
    private void Initialized()
    {
        for (int i = 0; i < 18; i++)
        {
            IsCheck[i] = 0;
            SelectedBoundary[i].SetActive(false);
            UI_InventoryInformation.Instance.SwitchImageNull();
        }
    } // 초기화 상태로 만든다.
}
