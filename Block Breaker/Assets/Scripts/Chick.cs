using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chick : MonoBehaviour
{

    [SerializeField] float yPush = -5f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,yPush);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
