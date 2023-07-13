using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTrash : MonoBehaviour
{
    public void OnClick_YesButton()
    {
        UI_InvenotrySlot.Instance.Deleted();
        gameObject.SetActive(false);
    }

    public void OnClick_NoButton()
    {
        gameObject.SetActive(false);
    }
}
