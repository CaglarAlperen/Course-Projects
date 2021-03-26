using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(WaitAndLoadNextSCene());
    }

    IEnumerator WaitAndLoadNextSCene()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(1);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
