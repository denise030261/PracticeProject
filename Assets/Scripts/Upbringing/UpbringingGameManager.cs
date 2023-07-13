using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpbringingGameManager : MonoBehaviour
{
    public int ActionNum; // 호감도
    public Text ActionNumText;
    public Text DayText;
    public GameObject Stat;
    public GameObject[] StatValue; // 스탯 막대기 
    public GameObject[] BlockInteraction;

    public GameObject RandomQuestion;
    public GameObject InteractionQuestion;
    public GameObject NextDayQuestion;

    public Dictionary<int, string> StatOrder = new Dictionary<int, string>(); // 스탯 딕셔너리
    public int[] Cal = new int[2];


    public static UpbringingGameManager Instance { get; private set; } = null;
    private void Awake()
    {
        StatOrder.Add(0, "Energy");
        StatOrder.Add(1, "Sociality");
        StatOrder.Add(2, "Deliberation");
        StatOrder.Add(3, "Curiosoty");
        StatOrder.Add(4, "Love");

        Cal[0] = 1;
        Cal[1] = -1;

        ActionNum = PlayerPrefs.GetInt("ActionNum", 1000);
        PlayerPrefs.SetInt("ActionNum", ActionNum);
        Instance = this;
    }
    private void Start()
    {
        DayText.text = HeavenGameManager.Instance.Day.ToString()+"일 째";
        ActionNumText.text = ActionNum.ToString();

        //AudioManager.Instance.StopBGM();
        //AudioManager.Instance.playBGM("Upbringing");
    }

    private void Update()
    {
        ActionNumText.text = PlayerPrefs.GetInt("ActionNum", 1000).ToString();
    }

    public void OnClick_NextDay()
    {
        PlayerPrefs.SetInt("ActionNum", ActionNum);
        NextDayQuestion.SetActive(true);
    }
    public void OnClick_Random()
    {
        RandomQuestion.SetActive(true);
    } // '무작위 성향 +1' 버튼

    public void OnClick_Want()
    {
        Stat.SetActive(true);
    } // '원하는 성향 +1' 버튼

    public void OnClick_InteractiQuestion()
    {
        InteractionQuestion.SetActive(true);
    }

    public void NextDayAnswer(bool Yes)
    {
        NextDayQuestion.SetActive(false);
        if (Yes)
        {
            if (HeavenGameManager.Instance.Day < 20)
            {
                HeavenGameManager.Instance.Day++;
                PlayerPrefs.SetInt("Day", HeavenGameManager.Instance.Day);
                SceneManager.LoadScene("Heaven");

            } // 20일 내의 시간은 천국 씬으로 넘어감
            else
            {
                HeavenGameManager.Instance.Day = 0;
                PlayerPrefs.SetInt("Day", HeavenGameManager.Instance.Day);
            } // 20일 째에는 강아지 유령의 정체가 밝혀지는 순간
        }
    }
}
