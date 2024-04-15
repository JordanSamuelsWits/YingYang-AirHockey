using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    public GameObject DifficultyToggles;

    private void Start()
    {
        DifficultyToggles.transform.GetChild((int)GameValues.Difficulty).GetComponent<Toggle>().isOn = true;
    }


    #region Difficulty
    public void SetEASYDifficulty(bool isOn)
    {
        if (isOn)
            GameValues.Difficulty = GameValues.Difficulties.EASY;
    }

    public void SetMEDIUMDifficulty(bool isOn)
    {
        if (isOn)
            GameValues.Difficulty = GameValues.Difficulties.MEDIUM;
    }

    public void SetHARDDifficulty(bool isOn)
    {
        if (isOn)
            GameValues.Difficulty = GameValues.Difficulties.HARD;
    }
    #endregion
}
