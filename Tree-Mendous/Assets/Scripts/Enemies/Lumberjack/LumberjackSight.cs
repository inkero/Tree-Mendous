using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberjackSight : MonoBehaviour {

    [SerializeField]
    private Lumberjack lumberjack;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            lumberjack.Target = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            lumberjack.Target = null;
        }
    }
}
