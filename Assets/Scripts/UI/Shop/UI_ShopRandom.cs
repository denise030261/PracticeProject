using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShopRandom : MonoBehaviour
{
    public GameObject FreeBuyQuestion;
    public GameObject RandomResult;

    public void OnClick_Free()
    {
        FreeBuyQuestion.SetActive(true);
    }

    public void OnClick_Buy(bool Yes)
    {
        FreeBuyQuestion.SetActive(false);
        if (Yes)

       {
            RandomResult.SetActive(true);
       }
    }

    public void OnClick_Check()
    {
        RandomResult.SetActive(false);
    }
}
