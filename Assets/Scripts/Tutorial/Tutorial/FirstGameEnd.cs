using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstGameEnd : MonoBehaviour
{
    public Text ChatText;
    private int order;
    private int Day;
    private int TutorialDay;

    private string[] Chat = {  "수고하셨습니다! 생각보다 적응이 빠르신 것 같군요!",
        "(관리소를 나오니 케이가 작은 유령과 함께 나를 기다리고 있다.)",
    "원래대로라면 관리소의 일은 여기서 끝입니다만,추가로 해주셔야 할 일이 있어 기다리고 있었습니다.",
        "다름이 아니라 최근 들어서 기억을 잃어 유령의 형태를 띄는 강아지가 점점 많이 발견되고 있습니다.",
        "그래서 저희 관리소 직원들이 유령들이 기억을 찾을 수 있게 돌보는 일을 하고 있습니다.",
    "어떻게 돌봐야 할지 알려드릴 테니 일단 집으로 이동하시죠."};

    private void Awake()
    {
        Day = PlayerPrefs.GetInt("Day");
        PlayerPrefs.SetInt("Day", Day);
        TutorialDay = PlayerPrefs.GetInt("TutorialDay", 0);
        PlayerPrefs.SetInt("TutorialDay", TutorialDay);
    }

    private void Start()
    {
        if (Day == 1 && TutorialDay==1)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
        ChatText.text = Chat[0];
        order = 1;
    }

    void Update()
    {
        if (order == Chat.Length && Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("Upbringing");
        }
        if (Input.GetMouseButtonDown(0) && Chat.Length > order)
        {
            ChatText.text = Chat[order];
            order++;
        }
    }
}
