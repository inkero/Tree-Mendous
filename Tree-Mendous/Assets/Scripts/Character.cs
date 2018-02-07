using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    [SerializeField]
    protected Transform knifePos;

    public float movementSpeed;
    public float currentHealth = 100;
    public AudioSource audioSource;
    public AudioClip hitSound;
    public float hitVolume = 0.7f;
    public AudioClip meleeSound;
    public float meleeVolume = 0.7f;
    public AudioClip deathSound;
    public float deathVolume = 0.7f;
    public GameObject pickup;

    [HideInInspector]
    public bool facingRight;

    [SerializeField]
    private GameObject knifePrefab;

    public bool Attack { set; get; }

    public Animator MyAnimator { get; private set; }

    // Use this for initialization
    public virtual void Start () {
        facingRight = true;
        MyAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    
    public virtual void ThrowAxe(int value)
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.Euler(new Vector3(0,0,-90)));

            Animation anim = tmp.transform.GetChild(0).GetComponent<Animation>();

            anim.Play("AxeSpinRight");

            //tmp.GetComponent<Knife>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.Euler(new Vector3(0, 0, 90)));

            Animation anim = tmp.transform.GetChild(0).GetComponent<Animation>();

            anim.Play("AxeSpinLeft");
            
            //tmp.GetComponent<Knife>().Initialize(Vector2.left);
        }
    }

    public virtual void Slash(int value)
    {
        Debug.Log("Ouch!");
    }


}
