using UnityEngine;

public class Stat_Manager : MonoBehaviour
{
    private JSON_Manager jsonManager;

    private void Awake()
    {
        jsonManager = GetComponent<JSON_Manager>();
    }

}
