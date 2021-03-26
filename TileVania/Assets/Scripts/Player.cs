using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Config Parameters
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpspeed = 5f;
    [SerializeField] float climbspeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    // State
    bool isAlive = true;

    // Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    float gravityScaleAtStart;


    // Message and Methods
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }

        Run();
        ClimbLadder();
        Jump();
        FlipSprite();
        Die();
    
    }

    private void Run()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(horizontalInput * moveSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool isRunning = Mathf.Abs(playerVelocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", isRunning);

    }

    private void ClimbLadder()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {
            myAnimator.SetBool("isClimbing", false);
            myRigidBody.gravityScale = gravityScaleAtStart;
            return; 
        }

        float verticalInput = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, verticalInput * climbspeed);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0f;

        bool isClimbing = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", isClimbing);
    }

    private void Jump()
    {
        bool isOnTheGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (isOnTheGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Vector2 jumpVelocityToAdd = new Vector2(0f, jumpspeed);
                myRigidBody.velocity += jumpVelocityToAdd;
            }
        }
        
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"))) 
        {
            myAnimator.SetTrigger("Die");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            isAlive = false;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
