using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour {

    [SerializeField]
    private string ignoreTag;

    void OnColliderEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == ignoreTag)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
        }

    }
}
