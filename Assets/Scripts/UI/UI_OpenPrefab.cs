using UnityEngine;
using UnityEngine.UI;

public class UI_OpenPrefab : MonoBehaviour
{
    public GameObject prefabToOpen;

    private Button button;

    private void Start()
    {
        // Get the Button component attached to the game object
        button = GetComponent<Button>();

        // Add a listener to the button's click event
        button.onClick.AddListener(OpenPrefab);
    }

    private void OpenPrefab()
    {
        // Instantiate the prefab
        GameObject prefabInstance = Instantiate(prefabToOpen);

        // Set the prefab's position as desired
        prefabInstance.transform.position = Vector2.zero;
    }
}
