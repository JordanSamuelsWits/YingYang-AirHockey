using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class CountdownManager : MonoBehaviour
{
    public TMP_Text countdownText;
    public float countdownDuration = 5f;

    private void Start()
    {
        StartCoroutine(StartGameCountdown());
    }

    private IEnumerator StartGameCountdown()
    {
        float timer = countdownDuration;

        while (timer > 0f)
        {
            countdownText.text = Mathf.Ceil(timer).ToString();
            timer -= Time.deltaTime;
            yield return null;
        }

        // Start the game (load the Main scene)
        SceneManager.LoadScene("Main");
    }
}
