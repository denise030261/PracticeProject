using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatManager : MonoBehaviour
{
    public GameObject[] State; // 스탯 막대기들
    public Text ActionNumText; // 스탯 텍스트
    public GameObject StatQuestion; // 스탯 질문
    private int CurrentNum; // 현재 호감도
    private int StatNum;
    public Text QuestionText;

    Dictionary<int, string> StatOrder = new Dictionary<int, string>(); // 스탯 딕셔너리

    private string[] StatText = { "활발함", "사회성", "대담함", "호기심", "애정표현",
        "차분함", "독립성", "신중함", "조용함", "냉정함"};

    private void Start()
    {
        StatOrder.Add(0, "Energy");
        StatOrder.Add(1, "Sociality");
        StatOrder.Add(2, "Deliberation");
        StatOrder.Add(3, "Curiosoty");
        StatOrder.Add(4, "Love");

        OnEnable();
    } // 딕셔너리 초기화
    
    private void OnEnable()
    {
        for(int i=0;i<5;i++)
        {
            RectTransform rectTransform = State[i].GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(PlayerPrefs.GetFloat(StatOrder[i] + "X"), PlayerPrefs.GetFloat(StatOrder[i]+"Y"));
        }
    } // 현재 스탯 반영

    public void Increase(int index)
    {
            if (index == 0 && StatManager.Instance.Energy < 4 && CurrentNum >= 100)
            {
                StatManager.Instance.Energy++;
                PlayerPrefs.SetInt("Energy", StatManager.Instance.Energy);
                StatMove(index, -111);
            DecreaseNum();
        }
            else if (index == 1 && StatManager.Instance.Sociality < 4 && CurrentNum >= 100)
            {
                StatManager.Instance.Sociality++;
                PlayerPrefs.SetInt("Sociality", StatManager.Instance.Sociality);
                StatMove(index, -111);
            DecreaseNum();
        }
            else if (index == 2 && StatManager.Instance.Deliberation < 4 && CurrentNum >= 100)
            {
                StatManager.Instance.Deliberation++;
                PlayerPrefs.SetInt("Deliberation", StatManager.Instance.Deliberation);
                StatMove(index, -111);
            DecreaseNum();
        }
            else if (index == 3 && StatManager.Instance.Curiosoty < 4 && CurrentNum >= 100)
            {
                StatManager.Instance.Curiosoty++;
                PlayerPrefs.SetInt("Curiosoty", StatManager.Instance.Curiosoty);
                StatMove(index, -111);
            DecreaseNum();
        }
            else if (index == 4 && StatManager.Instance.Love < 4 && CurrentNum >= 100)
            {
                StatManager.Instance.Love++;
                PlayerPrefs.SetInt("Love", StatManager.Instance.Love);
                StatMove(index, -111);
            DecreaseNum();
        }
    } // 왼쪽 버튼 누르면 각 스탯 증가, 위쪽부터 아래까지 0 ~ 4번째

    public void Decrease(int index)
    {
        if (CurrentNum >= 100)
        {
            if (index == 0 && StatManager.Instance.Energy > -4 && CurrentNum >= 100)
            {
                
                StatManager.Instance.Energy--;
                PlayerPrefs.SetInt("Energy", StatManager.Instance.Energy);
                StatMove(index, 111);
                DecreaseNum();
            }
            else if (index == 1 && StatManager.Instance.Sociality > -4 && CurrentNum >= 100)
            {
                StatManager.Instance.Sociality--;
                PlayerPrefs.SetInt("Sociality", StatManager.Instance.Sociality);
                StatMove(index, 111);
                DecreaseNum();
            }
            else if (index == 2 && StatManager.Instance.Deliberation > -4 && CurrentNum >= 100)
            {
                StatManager.Instance.Deliberation--;
                PlayerPrefs.SetInt("Deliberation", StatManager.Instance.Deliberation);
                StatMove(index, 111);
                DecreaseNum();
            }
            else if (index == 3 && StatManager.Instance.Curiosoty > -4 && CurrentNum >= 100)
            {
                StatManager.Instance.Curiosoty--;
                PlayerPrefs.SetInt("Curiosoty", StatManager.Instance.Curiosoty);
                StatMove(index, 111);
                DecreaseNum();
            }
            else if (index == 4 && StatManager.Instance.Love > -4 && CurrentNum >= 100)
            {
                StatManager.Instance.Love--;
                PlayerPrefs.SetInt("Love", StatManager.Instance.Love);
                StatMove(index, 111);
                DecreaseNum();
            }

        }
    } // 오른쪽 버튼 누르면 각 스탯 감소, 위쪽부터 아래까지 0 ~ 4번째

    private void StatMove(int index, int move)
    {
        RectTransform rectTransform = State[index].GetComponent<RectTransform>();
        rectTransform.anchoredPosition += new Vector2(move, 0f);
        PlayerPrefs.SetFloat(StatOrder[index] + "X", rectTransform.anchoredPosition.x);
        PlayerPrefs.SetFloat(StatOrder[index] + "Y", rectTransform.anchoredPosition.y);
    } // 스탯바 움직이기

    public void OnClick_CloseButton()
    {
        gameObject.SetActive(false);
    } // 팝업창 끄기

    private void Update()
    {
        CurrentNum = PlayerPrefs.GetInt("ActionNum");
        ActionNumText.text = PlayerPrefs.GetInt("ActionNum").ToString();

        for (int i = 0; i < 5; i++)
        {
            RectTransform rectTransform = State[i].GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(PlayerPrefs.GetFloat(StatOrder[i] + "X"), PlayerPrefs.GetFloat(StatOrder[i] + "Y"));
        }
    } // 실시간 호감도 반영

    private void DecreaseNum()
    {
        int ChangeNum = PlayerPrefs.GetInt("ActionNum") - 100;
        PlayerPrefs.SetInt("ActionNum", ChangeNum);
    } // 호감도 감소

    public void OnClick_StatButton(int index)
    {
        StatNum = index;
        StatQuestion.SetActive(true);
        QuestionText.text = "'"+StatText[index]+"'" + "을 증가하겠습니까?";
    }
    public void OnClick_QuestionButton(bool valiable)
    {
        if(valiable && StatNum>=0 && StatNum<=4)
        {
            Increase(StatNum);
            StatQuestion.SetActive(false);
        }
        else if(valiable && StatNum >= 5 && StatNum <= 9)
        {
            Decrease(StatNum - 5);
            StatQuestion.SetActive(false);
        }
        else if(!valiable)
        {
            StatQuestion.SetActive(false);
        }
    }
}
