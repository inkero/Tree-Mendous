using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorjackDamageArea : MonoBehaviour {

    [SerializeField]
    private Motorjack lumberjack;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            lumberjack.slashing = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            lumberjack.slashing = false;
        }
    }
}
