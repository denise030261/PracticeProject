using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatMove : MonoBehaviour
{
    public GameObject[] State;

    Dictionary<int, string> StatOrder = new Dictionary<int, string>(); // Ω∫≈» µÒº≈≥ ∏Æ

    private void Awake()
    {
        StatOrder.Add(0, "Energy");
        StatOrder.Add(1, "Sociality");
        StatOrder.Add(2, "Deliberation");
        StatOrder.Add(3, "Curiosoty");
        StatOrder.Add(4, "Love");
    } // µÒº≈≥ ∏Æ √ ±‚»≠

    private void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            RectTransform rectTransform = State[i].GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(PlayerPrefs.GetFloat(StatOrder[i] + "X")*99/111, PlayerPrefs.GetFloat(StatOrder[i] + "Y"));
        }
    } // Ω∫≈» øÚ¡˜¿” π›øµ
}
