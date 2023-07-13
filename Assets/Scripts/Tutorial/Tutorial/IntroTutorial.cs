using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroTutorial : MonoBehaviour
{
    public GameObject NameQuestion;
    public Text ChatText;
    private int order;
    private string Name;
    private bool IsClick=true;
    private int Day;
    private int TutorialDay;

    private string[] Chat = {  "아, 이번에 7번 관리소를 담당하게 된 분이신가요?","혹시 성함이 어떻게 되실까요?",
    "아하... 확인했습니다", "반갑습니다! 저는 7번 관리소의 인수인계를 맡게 된 '케이'라고 합니다.",
        "앞으로 해야 할 일에 대해 알려드릴 테니,잘 보시고 따라하시면 됩니다."};

    private void Awake()
    {
        Day = PlayerPrefs.GetInt("Day", 0);
        PlayerPrefs.SetInt("Day", Day);
        TutorialDay = PlayerPrefs.GetInt("TutorialDay", 0);
        PlayerPrefs.SetInt("TutorialDay", TutorialDay);

        ChatText.text = Chat[0];
        order = 1;
    }

    private void Start()
    {
        if (Day >= 1)
        {
            gameObject.SetActive(false);
        }
        else if (Day == 0 && TutorialDay == 0)
        {
            gameObject.SetActive(true);
        }
    }
    void Update()
    {
        if (order == Chat.Length && Input.GetMouseButtonDown(0))
        {
            PlayerPrefs.SetInt("TutorialDay", 1);
            gameObject.SetActive(false);
            SceneManager.LoadScene("Heaven");
        }
        if (Input.GetMouseButtonDown(0) && Chat.Length>order && IsClick)
        {
            ChatText.text = Chat[order];
            if (order == 2)
            {
                NameCreate();
                ChatText.text = "";
                order++;
            }
            order++;
        }
    }

    void NameCreate()
    {
        NameQuestion.SetActive(true);
        IsClick = false;
    }

    public void OnClick_Check()
    {
        NameQuestion.SetActive(false);
        IsClick = true;
        ChatText.text = Chat[3];
    }
}
