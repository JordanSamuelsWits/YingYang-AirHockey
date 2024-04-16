using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public TMP_Text AiScoreTxt;     // Text component displaying AI's score
    public TMP_Text PlayerScoreTxt; // Text component displaying player's score

    public UiManager uiManager; // Reference to the UI manager script

    public int MaxScore; // Maximum score required to win

    private int aiScore;    // Current AI score
    private int playerScore; // Current player score

    // Property to encapsulate AI's score and trigger UI update when AI wins
    private int AiScore
    {
        get { return aiScore; }
        set
        {
            aiScore = value;
            if (value == MaxScore)
                uiManager.ShowRestartCanvas(false); // AI wins, show restart canvas
        }
    }

    // Property to encapsulate player's score and trigger UI update when player wins
    private int PlayerScore
    {
        get { return playerScore; }
        set
        {
            playerScore = value;
            if (value == MaxScore)
                uiManager.ShowRestartCanvas(true); // Player wins, show restart canvas
        }
    }

    // Increment the specified score (AI or player)
    public void IncrementScore(string whichScore)
    {
        if (whichScore == "AiGoal")
            AiScoreTxt.text = (++AiScore).ToString(); // Increment and update AI's score
        else if (whichScore == "PlayerGoal")
            PlayerScoreTxt.text = (++PlayerScore).ToString(); // Increment and update player's score
    }

    // Decrement the specified score (AI or player) if score is greater than 0
    public void DecrementScore(string whichScore)
    {
        if (whichScore == "AiGoal" && AiScore > 0)
            AiScoreTxt.text = (--AiScore).ToString(); // Decrement and update AI's score
        else if (whichScore == "PlayerGoal" && PlayerScore > 0)
            PlayerScoreTxt.text = (--PlayerScore).ToString(); // Decrement and update player's score
    }

    // Reset both AI's and player's scores to zero
    public void ResetScores()
    {
        AiScore = 0; // Reset AI's score
        PlayerScore = 0; // Reset player's score
        AiScoreTxt.text = PlayerScoreTxt.text = "0"; // Update UI to display zero scores
    }
}
