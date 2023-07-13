using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomQuestion : MonoBehaviour
{
   public void OnClick_YesButton()
    {
        if (UpbringingGameManager.Instance.ActionNum >= 50)
        {
            UpbringingGameManager.Instance.ActionNum -= 50;
            UpbringingGameManager.Instance.ActionNumText.text = UpbringingGameManager.Instance.ActionNum.ToString();
            PlayerPrefs.SetInt("ActionNum", UpbringingGameManager.Instance.ActionNum);

            int StatOrderNum = Random.Range(0, 5);
            int StatNum = Random.Range(0, 2);

            while (PlayerPrefs.GetInt(UpbringingGameManager.Instance.StatOrder[StatOrderNum]) + UpbringingGameManager.Instance.Cal[StatNum] > 4
                || PlayerPrefs.GetInt(UpbringingGameManager.Instance.StatOrder[StatOrderNum]) + UpbringingGameManager.Instance.Cal[StatNum] < -4)
            {
                StatOrderNum = Random.Range(0, 5);
                StatNum = Random.Range(0, 2);
            }

            PlayerPrefs.SetInt(UpbringingGameManager.Instance.StatOrder[StatOrderNum], PlayerPrefs.GetInt(UpbringingGameManager.Instance.StatOrder[StatOrderNum]) + UpbringingGameManager.Instance.Cal[StatNum]);
            PlayerPrefs.SetFloat(UpbringingGameManager.Instance.StatOrder[StatOrderNum] + "X", PlayerPrefs.GetFloat(UpbringingGameManager.Instance.StatOrder[StatOrderNum] + "X") - 111 * UpbringingGameManager.Instance.Cal[StatNum]);
        }
        gameObject.SetActive(false);
    }

    public void OnClick_NoButton()
    {
        gameObject.SetActive(false);
    }
}
