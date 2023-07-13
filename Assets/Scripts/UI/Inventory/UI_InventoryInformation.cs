using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventoryInformation : MonoBehaviour
{
    public Image FoodImage;
    public Text FoodText;

    public static UI_InventoryInformation Instance { get; private set; } = null;

    private void Awake()
    {
        Instance = this;
    }

    public void SwitchImage(Image SelectedImage)
    {
        FoodImage.GetComponent<Image>().sprite = SelectedImage.sprite;
    } // 이미지 바꾸기

    public void SwitchImageNull()
    {
        FoodImage.GetComponent<Image>().sprite = null;
        FoodText.text = "";
    } // 이미지 없애기 및 텍스트 없애기

}
