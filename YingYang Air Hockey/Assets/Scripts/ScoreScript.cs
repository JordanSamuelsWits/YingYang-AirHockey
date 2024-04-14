using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public enum Score
    {
        AiScore,
        PlayerScore
    }

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
                uiManager.ShowRestartCanvas(true);
        }
    }

    private int PlayerScore
    {
        get { return playerScore; }
        set
        {
            playerScore = value;
            if (value == MaxScore)
                uiManager.ShowRestartCanvas(false);
        }
    }

    public void Increment(Score whichScore)
    {
        if (whichScore == Score.AiScore)
            AiScoreTxt.text = (++AiScore).ToString();
        else
            PlayerScoreTxt.text = (++PlayerScore).ToString();
    }

    public void ResetScores()
    {
        AiScore = PlayerScore = 0;
        AiScoreTxt.text = PlayerScoreTxt.text = "0";
    }
}
