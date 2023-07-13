using UnityEngine;
using UnityEngine.UI;

public class UI_PrefabOpen : MonoBehaviour
{
    public GameObject prefab;

    private bool isPrefabVisible = false;

    private void Start()
    {
        // Deactivate the prefab initially
        prefab.SetActive(false);
    }

    public void TogglePrefabDisplay()
    {
        // Toggle the visibility of the prefab
        isPrefabVisible = !isPrefabVisible;
        prefab.SetActive(isPrefabVisible);
    }
}
