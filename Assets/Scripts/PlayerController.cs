using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Speed
    public float moveSpeed;
    public float speedMultiplier;
    private float moveSpeedStore;
    public float speedMilestoneMultiplier;
    public float speedIncreaseMilestone;
    public float speedIncreaseMilestoneStore;
    private float speedMilestoneCount;
    private float speedMilestoneCountStore;
   
    //Jump
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;
    public bool isJumping;

    //Ground
    public bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    //Death
    public bool isDead;
    public float waitTimeDeath;

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;

    //Managers
    public ScoreManager theScoreManager;
    public GameManager theGameManager;

    //Sounds
    public AudioSource jumpSound;
    public AudioSource deathSound;

    //Others
    public EmissionSystem theEmissionSystem;
    private IEnumerator coroutine;

    void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        jumpTimeCounter = jumpTime;

        speedMilestoneCount = speedIncreaseMilestone;

        moveSpeedStore = moveSpeed;
        speedMilestoneCountStore = speedMilestoneCount;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;         
    }

    void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (transform.position.x > speedMilestoneCount) //Speed increase and store
        {
            speedMilestoneCount += speedIncreaseMilestone;
            speedIncreaseMilestone = speedIncreaseMilestone * speedMilestoneMultiplier;
            moveSpeed = moveSpeed * speedMultiplier;
        }

        if (!isDead)
        {
            myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);
        }

        //for IOS

        foreach (Touch touch in Input.touches)
        {
            if (touch.position.x < Screen.width / 2)
            {
                if (touch.phase == TouchPhase.Began) //Shoot
                {
                    myAnimator.SetTrigger("Player_Shoot");
                }
            }
            else if (touch.position.x > Screen.width / 2)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (isGrounded)
                    {
                        myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                        myAnimator.SetTrigger("Player_Jump");
                        jumpSound.Play();
                        isJumping = true;
                    }
                }

                if (touch.phase == TouchPhase.Stationary && isJumping)
                {
                    if (jumpTimeCounter > 0) //Jump with duration
                    {
                        myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                        jumpTimeCounter -= Time.deltaTime;
                        isJumping = true;
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    jumpTimeCounter = 0;
                }
            }

            //endif


#if UNITY_EDITOR

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isGrounded)
                {
                    myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                    myAnimator.SetTrigger("Player_Jump");
                    jumpSound.Play();
                    isJumping = true;
                }
            }

            if (Input.GetKey(KeyCode.Space) && isJumping)
            {
                if (jumpTimeCounter > 0) //Jump with duration
                {
                    myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                    jumpTimeCounter -= Time.deltaTime;
                    isJumping = true;
                }
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                jumpTimeCounter = 0;
            }


            if (Input.GetKeyDown(KeyCode.A)) //Shoot
            {
                myAnimator.SetTrigger("Player_Shoot");
            }

#endif

            myAnimator.SetBool("Grounded", isGrounded);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        var colliderTag = collision.gameObject.tag;

        //Groundcheck for can jump 
        if (colliderTag == "Ground" && isJumping) {

            jumpTimeCounter = jumpTime;
            myAnimator.SetTrigger("Player_Run");
            isJumping = false;
        }

        //Death through collision detection
        if (colliderTag == "Killbox" || colliderTag == "Enemy" || colliderTag == "KillStar")
        {
            isDead = true;
            deathSound.Play();
            myAnimator.SetTrigger("Player_Die");
            theScoreManager.scoreIncreasing = false;
            moveSpeed = moveSpeedStore;
            speedMilestoneCount = speedMilestoneCountStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;

            coroutine = WaitToRestart(waitTimeDeath);
            StartCoroutine(coroutine);

            this.enabled = false;

        }
    }

    IEnumerator WaitToRestart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        theGameManager.RestartGame();
    }

    public void ShootReady()
    {
        theEmissionSystem.ShootNinjaStar();
    }

    public void Run()
    {
        myAnimator.SetTrigger("Player_Run");
    }
}
