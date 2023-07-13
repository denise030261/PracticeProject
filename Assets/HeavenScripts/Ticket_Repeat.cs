using UnityEngine;
using UnityEngine.UI;

public class Ticket_Repeat : MonoBehaviour
{
    public Button button;
    private ApplyRandomColor TicketColorScript;

    private void Start()
    {
        TicketColorScript = FindObjectOfType<ApplyRandomColor>();

        if (TicketColorScript != null)
        {
            button.onClick.AddListener(TicketColorScript.ApplyRandomColorToUIImage);
        }
    }
}
