using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MenuSetting : MonoBehaviour
{
    public GameObject Setting;
    public GameObject Inventory;
    public GameObject Shop;
    public GameObject Information;

    public void OnClick_Setting()
    {
        Setting.SetActive(true);
    }
    public void OnClick_Shop()
    {
        Shop.SetActive(true);
    }
    public void OnClick_Informaition()
    {
        Information.SetActive(true);
    }
    public void OnClick_Inventory()
    {
        Inventory.SetActive(true);
    }
}
