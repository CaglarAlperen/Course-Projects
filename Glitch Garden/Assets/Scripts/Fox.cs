using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.name == "Gravestone(Clone)")
        {
            GetComponent<Animator>().SetTrigger("JumpTrigger");
        }
        else if (other.GetComponent<Defender>())
        {
            GetComponent<Attacker>().Attack(other);
        }
    }
}
