using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AssortMenu : MonoBehaviour
{
    public GameObject Menu;

    public void OnClick_MeunOpen()
    {
        Menu.SetActive(true);
        Time.timeScale = 0;
    }
}
