using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Ticket_Text : MonoBehaviour
{
    private List<string> texts;
    public Button[] buttons;
    public Text textUI;

    private void Start()
    {
        LoadTexts();
        SetupButtonListeners();
    }

    private void LoadTexts()
    {
        string filePath = "JsonFiles/Game/Set";
        TextAsset textAsset = Resources.Load<TextAsset>(filePath);
        if (textAsset != null)
        {
            SetData setData = JsonUtility.FromJson<SetData>(textAsset.text);
            if (setData != null)
            {
                texts = new List<string>();
                foreach (var set in setData.Sets)
                {
                    texts.Add(set.Sort);
                }
                Debug.Log("JSON 파일 읽기 성공!");
            }
            else
            {
                Debug.LogError("JSON 데이터 변환 실패!");
            }
        }
        else
        {
            Debug.LogError("JSON 파일 읽기 실패!");
        }
    }

    private void SetupButtonListeners()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
    }

    private void OnButtonClick(int buttonIndex)
    {
        if (texts != null && texts.Count > 0)
        {
            int randomIndex = Random.Range(0, texts.Count);
            string randomText = texts[randomIndex];
            Debug.Log("Button " + buttonIndex + " 클릭: " + randomText);

            if (textUI != null)
            {
                textUI.text = randomText;
            }
            else
            {
                Debug.LogError("Text UI를 찾을 수 없습니다!");
            }
        }
        else
        {
            Debug.LogWarning("텍스트 데이터가 없습니다!");
        }
    }
}

[System.Serializable]
public class SetData
{
    public List<Set> Sets;
}

[System.Serializable]
public class Set
{
    public string Color;
    public string emblemAssets;
    public string Sort;
}
