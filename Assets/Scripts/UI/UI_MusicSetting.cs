using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MusicSetting : MonoBehaviour
{
    private float BGMVolume;
    private float SFXVolume;
    private int BGMOrder;
    private int SFXOrder;
    public GameObject[] BGMSize;
    public GameObject[] SFXSize;

    private void Start()
    {
        BGMVolume = PlayerPrefs.GetFloat("BGM") * 100;
        SFXVolume = PlayerPrefs.GetFloat("SFX") * 100;
        BGMOrder = PlayerPrefs.GetInt("BGMOrder", 2);
        SFXOrder = PlayerPrefs.GetInt("SFXOrder", 2);
        for (int i = 0; i <= BGMOrder; i++)
        {
            BGMSize[i].SetActive(false);
        }
        for (int i = BGMOrder + 1; i < 5; i++)
        {
            BGMSize[i].SetActive(true);
        }
    }
    public void OnClick_BGMVolumeBig()
    {
        if (BGMVolume < 100)
        {
            BGMVolume += 20;
            BGMOrder++;
            for(int i=0;i<=BGMOrder;i++)
            {
                BGMSize[i].SetActive(false);
            }
            for (int i = BGMOrder+1; i < 5; i++)
            {
                BGMSize[i].SetActive(true);
            }
            PlayerPrefs.SetFloat("BGM", BGMVolume / 100.0f);
            PlayerPrefs.SetInt("BGMOrder", BGMOrder);
        }
    }
    public void OnClick_BGMVolumeSmall()
    {
        if (BGMVolume > 0)
        {
            BGMVolume -= 20;
            BGMOrder--;
            for (int i = 0; i <= BGMOrder; i++)
            {
                BGMSize[i].SetActive(false);
            }
            for (int i = BGMOrder+1; i < 5; i++)
            {
                BGMSize[i].SetActive(true);
            }
            PlayerPrefs.SetFloat("BGM", BGMVolume / 100.0f);
            PlayerPrefs.SetInt("BGMOrder", BGMOrder);
        }
    }
    public void OnClick_SFXVolumeBig()
    {
        if (SFXVolume < 100)
        {
            SFXVolume += 20;
            SFXOrder++;
            for (int i = 0; i <= SFXOrder; i++)
            {
                SFXSize[i].SetActive(false);
            }
            for (int i = SFXOrder + 1; i < 5; i++)
            {
                SFXSize[i].SetActive(true);
            }
            PlayerPrefs.SetFloat("SFX", SFXVolume / 100.0f);
            PlayerPrefs.SetInt("SFXOrder", SFXOrder);
        }
    }
    public void OnClick_SFXVolumeSmall()
    {
        if (SFXVolume > 0)
        {
            SFXVolume -= 20;
            SFXOrder--;
            for (int i = 0; i <= SFXOrder; i++)
            {
                SFXSize[i].SetActive(false);
            }
            for (int i = SFXOrder + 1; i < 5; i++)
            {
                SFXSize[i].SetActive(true);
            }
            PlayerPrefs.SetFloat("SFX", SFXVolume / 100.0f);
            PlayerPrefs.SetInt("SFXOrder", SFXOrder);
        }
    }
    public void OnClick_Close()
    {
        gameObject.SetActive(false);
    }
}
