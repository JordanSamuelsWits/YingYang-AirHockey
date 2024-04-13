using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public enum Score
    {
        AiScore,
        PlayerScore
    }

    public TMP_Text AiScoreTxt; // Reference to TextMeshPro Text for AI Score
    public TMP_Text PlayerScoreTxt; // Reference to TextMeshPro Text for Player Score

    private int aiScore, playerScore;

    public void Increment(Score whichScore)
    {
        if (whichScore == Score.AiScore)
        {
            aiScore++;
            UpdateTextMeshProText(AiScoreTxt, aiScore);
        }
        else if (whichScore == Score.PlayerScore)
        {
            playerScore++;
            UpdateTextMeshProText(PlayerScoreTxt, playerScore);
        }
    }

    private void UpdateTextMeshProText(TMP_Text textMeshProText, int value)
    {
        if (textMeshProText != null)
        {
            textMeshProText.text = value.ToString();
        }
        else
        {
            Debug.LogWarning("TextMeshPro Text component is not assigned.");
        }
    }
}
