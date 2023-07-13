using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; } = null;

    public AudioSource BGMSource;
    //public AudioSource SFXSource;
    private static AudioManager instance;

    private void Awake()
    {
        Instance = this; // 인스턴스화

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        } // 씬 넘길 때 음악 잇기
    } 

    public void playBGM(string name)
    {
        AudioClip BGMClip = Resources.Load<AudioClip>("Music/BGM/" + name);
        if(BGMClip !=null)
        {
            if (BGMClip != null)
            {
                BGMSource.clip = BGMClip;
                BGMSource.volume = PlayerPrefs.GetFloat("BGM");
                BGMSource.Play();
            }
        }
    } // BGM 틀기

    public void StopBGM()
    {
        BGMSource.Stop();
    }
    /*public void playSFX(string name)
    {
        AudioClip SFXClip = Resources.Load<AudioClip>("SFX/" + name);
        if (SFXClip != null)
        {
            if (SFXClip != null)
            {
                SFXSource.clip = SFXClip;
                SFXSource.volume = PlayerPrefs.GetFloat("SFX");
                SFXSource.Play();
            }
        }
    } // SFX 틀기*/

    private void Update()
    {
        BGMSource.volume = PlayerPrefs.GetFloat("BGM",0.6f);
        //SFXSource.volume = PlayerPrefs.GetFloat("SFX");
    } // BGM, SFX을 바로 반영하기
}
