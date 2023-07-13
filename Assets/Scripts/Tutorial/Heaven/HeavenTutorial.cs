using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HeavenTutorial : MonoBehaviour
{
    public int Delay;
    public RectTransform ChatTransform;
    public Text ChatText;
    public Button StartButton;

    public GameObject RealStart;
    public GameObject TutorialStart;
    public GameObject TutorialChat;
    public GameObject BlockScreen;
    public GameObject[] TutorialInformation;

    private int order;
    private int Day;
    private int TutorialDay;
    private bool IsClick = true;

    private string[] Chat = {  "앞에 있는 버튼을 누르면 업무를 시작하게 됩니다. 한 번 눌러보시겠어요?", 
        "저기 티켓과 열차가 보이시죠? 저희가 해야 할 일은 열차에 탑승할 강아지의 티켓이 열차의 정보와 일치하는지 확인하고 안내하는 일입니다.",
    "왼쪽 열차의 티켓이면 왼쪽을 터치하고,", "오른쪽 열차의 티켓이면 오른쪽을 터치하면 됩니다.", 
        "가끔 색이 다른 티켓을 가지고 오는 강아지가 있는데, 그건 교환해줘야 하니 아래의 버튼을 눌러 돌려보내야 합니다.",
    "아 참, 실수를 하게 되면 업무 시간이 줄어드니 주의하세요!", "어때요? 생각보다 어렵지 않죠? 자, 그럼 한 번 직접 해볼까요?"};

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
        {
            gameObject.SetActive(true);
            StartButton.interactable=false;
        }
        else
            gameObject.SetActive(false);
        ChatText.text = Chat[0];
        order = 1;
    }

    void Update()
    {
        if (order == Chat.Length && Input.GetMouseButtonDown(0) && IsClick)
        {
            RealStart.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (Input.GetMouseButtonDown(0) && order == 1 && IsClick)
        {
            StartButton.interactable = true;
            TutorialChat.SetActive(false);
        }
        else if (Input.GetMouseButtonDown(0) && order == 2 && IsClick)
        {
            TutorialInformation[4].SetActive(false);
            for (int i=0;i<4; i++)
                TutorialInformation[i].SetActive(true);
            ModifyRectTransform(485,-296,900,700);
            ChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && order == 3 && IsClick)
        {
            for (int i = 2; i < 4; i++)
                TutorialInformation[i].SetActive(false);
            for (int i = 4; i < 6; i++)
                TutorialInformation[i].SetActive(true);
            ModifyRectTransform(-485, -296, 900, 700);
            ChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && order == 4 && IsClick)
        {
            for (int i = 4; i < 6; i++)
                TutorialInformation[i].SetActive(false);
            TutorialInformation[6].SetActive(true);
            ModifyRectTransform(0, 53, 1500, 800);
            ChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && order == 5 && IsClick)
        {
            for (int i = 7; i < 9; i++)
                TutorialInformation[i].SetActive(true);
            TutorialInformation[1].SetActive(false);
            TutorialInformation[6].SetActive(false);
            ModifyRectTransform(0, -296, 1500, 800);
            ChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && order == 6 && IsClick)
        {
            for (int i = 1; i < 9; i++)
                TutorialInformation[i].SetActive(false);
            ModifyRectTransform(0, -296, 1500, 800);
            ChatDisplay();
        }
    }

    public void OnClick_TutorialStart()
    {
        IsClick = false;
        TutorialStart.SetActive(false);
        BlockScreen.SetActive(true);
        StartCoroutine(Wait());
    } // 튜토리얼의 시작 버튼 누를 때

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 3; i++)
            TutorialInformation[i].SetActive(true);
        TutorialInformation[4].SetActive(true);
        IsClick = true;
        TutorialChat.SetActive(true);
        ChatDisplay();
    } // 기다리는 동안 게임 화면 보여주기

    void ChatDisplay()
    {
        ChatText.text = Chat[order];
        order++;
    }

    private void ModifyRectTransform(int x, int y, int width, int height)
    {
        // Width, Height, Pos X, Pos Y 변경
        ChatTransform.sizeDelta = new Vector2(width, height);
        ChatTransform.anchoredPosition = new Vector2(x, y);
    }
}
