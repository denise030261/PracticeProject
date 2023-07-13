using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpbringingTutorial : MonoBehaviour
{
    public int Delay;
    public Text ChatText;
    public GameObject ChatSet;
    public GameObject BackGroundImage;
    public GameObject TutorialStat;
    public GameObject TutorialShop;
    public Image BlockImage;


    private int order;
    private int Day;
    private int TutorialDay;
    private bool IsClick = false;

    private string[] FirstChat = {  "유령을 돌보는 기간은 20일을 생각하고 있습니다.","이 유령들은 20일 정도의 기간동안 돌보다 보면 기억과 함께 모습을 되찾더군요.",
    "특히, 어떤 성향을 가지냐에 따라 모습이 달라지는 것 같았습니다.", "만약 이 가설이 사실이라면, 상점에서 판매하는 특별한 음식으로 유령의 기억을 더 정확하게 찾게 해줄 수 있을 겁니다.",
        "말이 나온 김에 바로 상점을 이용해봅시다."};

    private void Awake()
    {
        Day = PlayerPrefs.GetInt("Day");
        PlayerPrefs.SetInt("Day", Day);
        TutorialDay = PlayerPrefs.GetInt("TutorialDay", 0);
        PlayerPrefs.SetInt("TutorialDay", TutorialDay);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Day == 1 && TutorialDay == 1)
        {
            gameObject.SetActive(true);
            ChatSet.SetActive(false);
            BackGroundImage.SetActive(false);
            StartCoroutine(Wait());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && 2 == order && IsClick)
        {
            TutorialStat.SetActive(true);
            FirstChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && 4 == order && IsClick)
        {
            TutorialStat.SetActive(false);
            TutorialShop.SetActive(true);
            FirstChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && FirstChat.Length > order && IsClick)
        {
            FirstChatDisplay();
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        ChatSet.SetActive(true);
        BackGroundImage.SetActive(true);
        ChatText.text = FirstChat[0];
        order = 1;
        IsClick = true;
    } // 기다리는 동안 게임 화면 보여주기
    void FirstChatDisplay()
    {
        ChatText.text = FirstChat[order];
        order++;
    }
}
