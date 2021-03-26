using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{

    [Range(0.1f,10f)] [SerializeField] float game_speed = 1f;
    [SerializeField] int points_per_block = 1;
    [SerializeField] Text score_text;
    [SerializeField] bool is_autoplay_enabled;

    [SerializeField] int current_score = 0;

    private void Awake()
    {
        int game_status_count = FindObjectsOfType<GameSession>().Length;
        if (game_status_count > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        score_text.text = current_score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = game_speed;
    }

    public void AddToScore()
    {
        current_score += points_per_block;
        score_text.text = current_score.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return is_autoplay_enabled;
    }
}
