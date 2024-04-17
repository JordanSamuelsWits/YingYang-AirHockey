using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RestartButton : MonoBehaviour
{
    private Button restartButton; // Reference to the Button component
    private UiManager uiManager; // Reference to the UiManager script
    private TMP_Text buttonText; // Reference to the TMP Text component

    private void Start()
    {
        // Get the Button component from this GameObject
        restartButton = GetComponent<Button>();

        // Find the UiManager script in the scene (assumes it's on a GameObject named "GameManager")
        uiManager = GameObject.Find("GameManager").GetComponent<UiManager>(); // Replace "GameManager" with your UiManager object name

        // Add listener to the button click event
        restartButton.onClick.AddListener(RestartGame);

        // Get the TMP Text component from this GameObject (optional)
        buttonText = GetComponentInChildren<TMP_Text>();
    }

    private void RestartGame()
    {
        // Call RestartGame method in UiManager when button is clicked
        if (uiManager != null)
            uiManager.RestartGame();

        // Example: Change button text (optional)
        if (buttonText != null)
            buttonText.text = "Restarting...";
    }
}
