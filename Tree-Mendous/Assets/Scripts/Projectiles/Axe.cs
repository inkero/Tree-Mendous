﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{

    public float projectileSpeed;
    public float weaponDamage;
    public float verticalSpeed = .2f;
    public float movementMultiplier = 1.0f;

    Rigidbody2D myRB;

    // Use this for initialization
    void Awake()
    {
        myRB = GetComponent<Rigidbody2D>();

        // If the projectile is facing left, add force to the left  
        if (transform.localRotation.z > 0)
        {
            myRB.AddForce(new Vector2(-1, verticalSpeed) * projectileSpeed, ForceMode2D.Impulse);
            // Otherwise add force to right
        }
        else
        {
            myRB.AddForce(new Vector2(1, verticalSpeed) * projectileSpeed, ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = transform.GetComponent<Rigidbody2D>().velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 8)
        { // If collision object is the player
            PlayerController hurtPlayer = col.gameObject.GetComponent<PlayerController>();
            hurtPlayer.addDamage(weaponDamage);
            Destroy(gameObject);
        }
        else if (col.gameObject.layer != 8)
        { // If collision object is not the player
            Destroy(gameObject);
        }
    }
}
