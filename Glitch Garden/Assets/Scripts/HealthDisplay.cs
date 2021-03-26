using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{

    [SerializeField] int health = 5;
    Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<Text>();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        healthText.text = health.ToString();
    }

    public void DecreaseHealth()
    {
        health--;
        if (health <= 0)
        {
            Lose();
        }
        UpdateDisplay();
    }

    private void Lose()
    {
        FindObjectOfType<LevelController>().HandleLoseCondition();
    }

    public void SetHealth(int health)
    {
        this.health = health;
        UpdateDisplay();
    }
}
