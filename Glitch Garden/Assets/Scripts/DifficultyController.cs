using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{

    void Start()
    {
        float difficulty = PlayerPrefsController.GetDifficulty();
        int difficulty_int = FloatToInt(difficulty);
        FindObjectOfType<HealthDisplay>().SetHealth(6 - difficulty_int);
    }

    private int FloatToInt(float difficulty_float)
    {
        int difficulty_int = (int)Mathf.Ceil(difficulty_float * 5);
        return difficulty_int;
    }

}
