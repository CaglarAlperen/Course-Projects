using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{

    [SerializeField] float scrollSpeed = 0.2f;

    private void Update()
    {
        float yMove = scrollSpeed * Time.deltaTime;
        transform.Translate(new Vector2(0f, yMove));
    }
}
