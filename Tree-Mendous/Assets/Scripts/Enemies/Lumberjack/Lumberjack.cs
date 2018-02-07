using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack : Character {

    private ILumberjackState currentState;

    public float chargeSpeed;
    public float originalMovementSpeed { get; set; }

    public GameObject Target { get; set; }
    public bool slashing = false;

    public bool blinded = false;
    private float blindTimer;
    private float blindTimerCooldown;

    // Use this for initialization
    public override void Start () {
        base.Start();
        facingRight = false;
        ChangeState(new IdleState());

        originalMovementSpeed = movementSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        currentState.Execute();

        LookAtTarget();

        blindCount();
    }

    private void LookAtTarget()
    {
        if(Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;

            if(xDir < 0 && facingRight || xDir > 0 && !facingRight)
            {
                ChangeDirection();
            }
        }
    }

    public void ChangeState(ILumberjackState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        Debug.Log("State: " + currentState);

        currentState.Enter(this);
    }

    public void Move()
    {
        if (!Attack) {
            MyAnimator.SetFloat("speed", 1);

            transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
        }
        
    }

    // Should use meleestate slash function instead
    public void Slash()
    {
        Target.GetComponent<PlayerController>().addDamage(25);
        audioSource.PlayOneShot(meleeSound, meleeVolume);
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        currentState.OnTriggerEnter(other);
    }

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

    public void blindEnemyFor(float seconds)
    {
        blinded = true;
        blindTimerCooldown = seconds;
    }

    private void blindCount()
    {
       
        if (blinded == true)
        {
            blindTimer += Time.deltaTime;

            if (blindTimer >= 2)
            {
                blinded = false;
                blindTimer = 0;
            }
        }
    }
}
