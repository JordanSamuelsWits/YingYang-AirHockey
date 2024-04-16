using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that holds game-related constant values and settings
public class GameValues
{
    // Enum to represent different difficulty levels
    public enum Difficulties { EASY, MEDIUM, HARD };

    // Static variable to store the current game difficulty, initialized to EASY
    public static Difficulties Difficulty = Difficulties.EASY;
}
