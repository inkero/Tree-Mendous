using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character {

    public void addDamage(float damage)
    {
        //myAnim.SetBool ("hit", true);
        currentHealth -= damage;
        audioSource.PlayOneShot(hitSound, hitVolume);

        if (currentHealth <= 0)
        {
            StartCoroutine("makeDead");
        }
    }

    IEnumerator makeDead()
    {
        MyAnimator.SetBool("died", true);
        audioSource.PlayOneShot(deathSound, deathVolume);
        yield return new WaitForSeconds(0.5f);
        Instantiate(pickup, transform.position + new Vector3(0, -.5f, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
        Destroy(gameObject);
    }

}
