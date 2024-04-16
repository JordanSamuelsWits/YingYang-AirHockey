using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public TMP_Text AiScoreTxt;
    public TMP_Text PlayerScoreTxt;

    public UiManager uiManager;

    public int MaxScore;

    private int aiScore;
    private int playerScore;

    private int AiScore
    {
        get { return aiScore; }
        set
        {
            aiScore = value;
            if (value == MaxScore)
                uiManager.ShowRestartCanvas(false); // AI wins
        }
    }

    private int PlayerScore
    {
        get { return playerScore; }
        set
        {
            playerScore = value;
            if (value == MaxScore)
                uiManager.ShowRestartCanvas(true); // Player wins
        }
    }

    public void IncrementScore(string whichScore)
    {
        if (whichScore == "AiGoal")
            AiScoreTxt.text = (++AiScore).ToString();
        else if (whichScore == "PlayerGoal")
            PlayerScoreTxt.text = (++PlayerScore).ToString();
    }

    public void DecrementScore(string whichScore)
    {
        if (whichScore == "AiGoal" && AiScore > 0)
            AiScoreTxt.text = (--AiScore).ToString();
        else if (whichScore == "PlayerGoal" && PlayerScore > 0)
            PlayerScoreTxt.text = (--PlayerScore).ToString();
    }

    public void ResetScores()
    {
        AiScore = 0;
        PlayerScore = 0;
        AiScoreTxt.text = PlayerScoreTxt.text = "0";
    }
}
