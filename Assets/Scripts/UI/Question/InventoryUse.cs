using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUse : MonoBehaviour
{
    public GameObject Block; // 사용 불가능을 위한 블럭

    public void OnClick_YesButton()
    {
        UI_InvenotrySlot.Instance.Deleted();
        Block.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnClick_NoButton()
    {
        gameObject.SetActive(false);
    }
}
