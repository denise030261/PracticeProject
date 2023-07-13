using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_QuestionNew : MonoBehaviour
{
    public void OnClick_NextNew()
    {
        SceneManager.LoadScene("Heaven");
    }
    public void OnClick_Cancel()
    {
        gameObject.SetActive(false);
    } // 팝업창 없애기
}
