using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    int score = 0;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int number_of_game_sessions = FindObjectsOfType<GameSession>().Length;
        if (number_of_game_sessions > 1)
        {
            Destroy(gameObject);
        } 
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int score_value)
    {
        score += score_value;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
