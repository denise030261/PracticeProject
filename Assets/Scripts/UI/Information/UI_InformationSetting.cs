using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InformationSetting : MonoBehaviour
{
    public GameObject DogTag;
    public GameObject FoodLow;
    public GameObject FoodMiddle;
    public GameObject FoodHigh;
    public GameObject LevelButton;
    private bool IsDog = false;
    private bool IsFood = false;

    private void Start()
    {
        DogTag.SetActive(false);
        LevelButton.SetActive(false);
        FoodLow.SetActive(false);
        FoodMiddle.SetActive(false);
        FoodHigh.SetActive(false);
    }
    public void OnClick_Close()
    {
        gameObject.SetActive(false);
    }

    public void OnClick_DogTag()
    {
        if (!IsDog)
        {
            DogTag.SetActive(true);
            LevelButton.SetActive(false);
            FoodLow.SetActive(false);
            FoodMiddle.SetActive(false);
            FoodHigh.SetActive(false);
            IsDog = true;
            IsFood = false;
        }
        else
        {
            DogTag.SetActive(false);
            IsDog = false;
        }
    }

    public void OnClick_FoodTag()
    {
        if (!IsFood)
        {
            LevelButton.SetActive(true);
            DogTag.SetActive(false);
            IsFood = true;
            IsDog = false;
        }
        else
        {
            LevelButton.SetActive(false);
            IsFood = false;
        }
    }

    public void OnClick_FoodLow()
    {
        FoodLow.SetActive(true);
        FoodMiddle.SetActive(false);
        FoodHigh.SetActive(false);
        LevelButton.SetActive(false);
        IsFood = false;
    }

    public void OnClick_FoodMiddle()
    {
        FoodMiddle.SetActive(true);
        FoodLow.SetActive(false);
        FoodHigh.SetActive(false);
        LevelButton.SetActive(false);
        IsFood = false;
    }

    public void OnClick_FoodHigh()
    {
        FoodHigh.SetActive(true);
        FoodLow.SetActive(false);
        FoodMiddle.SetActive(false);
        LevelButton.SetActive(false);
        IsFood = false;
    }
}
