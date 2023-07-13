using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpbringingTutorialInventory : MonoBehaviour
{
    private int order = 0;
    private int Day;
    private int TutorialDay;
    private bool IsClick = false;

    public Text ChatText;
    public GameObject ChatSet;
    public GameObject[] TutorialEat;
    public GameObject ShopButton;
    public GameObject TutorialInventory;
    public GameObject ItemTutorial;
    public GameObject UseTutorial;
    public GameObject Stat;
    public GameObject IconImage;
    public RectTransform ChatTransform;

    private string[] Chat = { "구매한 음식은 이곳에서 확인할 수 있습니다.", "방금 구매한 음식이 저기 보이네요. 한 번 확인해보시겠어요?",
    "선택한 음식에 대한 정보는 여기서 확인할 수 있습니다.","음식은 하루에 한 번씩만 먹일 수 있지만, 지금은 하나밖에 없으니 유령에게 이 음식을 먹여봅시다.",
    "이렇게 음식을 먹였을 때 이곳에서 유령의 성향이 변하는 것을 확인할 수 있습니다."};

    private void Awake()
    {
        Day = PlayerPrefs.GetInt("Day");
        PlayerPrefs.SetInt("Day", Day);
        TutorialDay = PlayerPrefs.GetInt("TutorialDay", 0);
        PlayerPrefs.SetInt("TutorialDay", TutorialDay);
        ItemTutorial.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && order == 3 && IsClick)
        {
            ChatDisplay();
            UseTutorial.SetActive(true);
        }
        else if(Input.GetMouseButtonDown(0) && order == Chat.Length)
        {
            IconImage.SetActive(false);
            Stat.SetActive(false);
            gameObject.SetActive(false);
            // 호감도 gameObject를 올림
        }
    }

    public void Onclick_Cancel()
    {
        ShopButton.SetActive(false);
        TutorialEat[0].SetActive(true);
        TutorialEat[1].SetActive(true);
        ChatSet.SetActive(true);
        IsClick = false;
        ChatDisplay();
        order--;
    }

    public void Onclick_TutorialInventory()
    {
        TutorialEat[0].SetActive(false);
        TutorialEat[1].SetActive(false);
        TutorialInventory.SetActive(true);
        ItemTutorial.SetActive(true);
        order++;
        ChatDisplay();
        ChatTransform.anchoredPosition = new Vector2(0, 180);
    }

    public void Onclick_ItemTutorial()
    {
        ChatDisplay();
        IsClick = true;
        ChatTransform.anchoredPosition = new Vector2(0, -296);
    }

    public void Onclick_Use()
    {
        UseTutorial.SetActive(false);
        ChatSet.SetActive(false);
    }
    void ChatDisplay()
    {
        ChatText.text = Chat[order];
        order++;
    }
    public void Onclick_Yes()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        TutorialInventory.SetActive(false);
        Stat.SetActive(true);
        yield return new WaitForSeconds(2f);
        ChatSet.SetActive(true);
        ChatText.text = Chat[Chat.Length - 1];
        order = Chat.Length;
    }
}
