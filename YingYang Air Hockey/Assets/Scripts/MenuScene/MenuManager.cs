using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject countdownCanvas; // Reference to the countdown canvas GameObject
    public TMP_Text countdownText; // Reference to the TMP_Text component for displaying countdown
    public GameObject DifficultyToggles; // Reference to the GameObject containing difficulty toggles

    private void Start()
    {
        // Ensure the correct difficulty toggle is selected on start
        DifficultyToggles.transform.GetChild((int)GameValues.Difficulty).GetComponent<Toggle>().isOn = true;
    }

    public void PlayGame()
    {
        // Disable the difficulty toggles during countdown and gameplay
        DifficultyToggles.SetActive(false);

        // Start the countdown coroutine
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        const int countdownDuration = 5; // Countdown duration in seconds

        // Activate the countdown canvas
        countdownCanvas.SetActive(true);

        // Display the countdown
        for (int i = countdownDuration; i > 0; i--)
        {
            countdownText.text = i.ToString(); // Update countdown text

            yield return new WaitForSeconds(1f); // Wait for 1 second
        }

        // Load the Main scene after countdown
        SceneManager.LoadScene("Main");
    }

    #region Difficulty Selection Methods

    public void SetEASYDifficulty(bool isOn)
    {
        if (isOn)
        {
            GameValues.Difficulty = GameValues.Difficulties.EASY;
        }
    }

    public void SetMEDIUMDifficulty(bool isOn)
    {
        if (isOn)
        {
            GameValues.Difficulty = GameValues.Difficulties.MEDIUM;
        }
    }

    public void SetHARDDifficulty(bool isOn)
    {
        if (isOn)
        {
            GameValues.Difficulty = GameValues.Difficulties.HARD;
        }
    }

    #endregion
}
