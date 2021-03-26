using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollider : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        FindObjectOfType<HealthDisplay>().DecreaseHealth();
        Destroy(other);
    }
}
