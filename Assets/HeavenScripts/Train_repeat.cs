using UnityEngine;
using UnityEngine.UI;

public class Train_repeat : MonoBehaviour
{
    public Button button;
    private Train_Color trainColorScript;

    private void Start()
    {
        trainColorScript = FindObjectOfType<Train_Color>();

        if (trainColorScript != null)
        {
            button.onClick.AddListener(trainColorScript.ApplyRandomColorToUIImage);
        }
    }
}
