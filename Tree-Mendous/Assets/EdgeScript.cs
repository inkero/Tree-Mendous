using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeScript : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DamageArea")
        {
            other.gameObject.GetComponentInParent<Lumberjack>().Target = null;

            Lumberjack enemy = other.gameObject.GetComponentInParent<Lumberjack>();
            LumberjackFov enemySight = other.gameObject.GetComponentInParent<Lumberjack>().gameObject.GetComponentInChildren<LumberjackFov>();

                enemy.blinded = true;

            other.gameObject.GetComponentInParent<Lumberjack>().ChangeDirection();
        }
    }
}
