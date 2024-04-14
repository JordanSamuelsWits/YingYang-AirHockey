using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject CanvasGame;
    public GameObject CanvasRestart;

    [Header("CanvasRestart Children")]
    public GameObject WinTxt;
    public GameObject LoseTxt;
    public GameObject RestartBtn;

    [Header("Other")]
    public AudioManager audioManager;

    public ScoreScript scoreScript;
    public PuckScript puckScript;
    public PlayerMovement playerMovement;
    public AiScript aiScript;

    void Start()
    {
        // Ensure CanvasGame is active and CanvasRestart is inactive at the beginning
        if (CanvasGame != null)
            CanvasGame.SetActive(true);

        if (CanvasRestart != null)
            CanvasRestart.SetActive(false);  // Deactivate CanvasRestart initially
    }

    public void ShowRestartCanvas(bool didAiWin)
    {
        Time.timeScale = 0;

        CanvasGame.SetActive(false);  // Deactivate CanvasGame
        CanvasRestart.SetActive(true);  // Activate CanvasRestart

        if (didAiWin)
        {
            audioManager.PlayLostGame();

            // Show "YOU LOSE!" text and restart button
            LoseTxt.SetActive(true);
            RestartBtn.SetActive(true);

            // Hide "YOU WIN!" text
            WinTxt.SetActive(false);
        }
        else
        {
            audioManager.PlayWonGame();

            // Show "YOU WIN!" text and restart button
            WinTxt.SetActive(true);
            RestartBtn.SetActive(true);

            // Hide "YOU LOSE!" text
            LoseTxt.SetActive(false);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        CanvasGame.SetActive(true);  // Activate CanvasGame
        CanvasRestart.SetActive(false);  // Deactivate CanvasRestart

        // Reset game components
        scoreScript.ResetScores();
        puckScript.CenterPuck();
        playerMovement.ResetPosition();
        aiScript.ResetPosition();
    }
}
