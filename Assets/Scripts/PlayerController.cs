using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private float moveSpeedStore;
    private LiveManager liveManager;

    [SerializeField] private float speedForcer;

    [SerializeField] private float speedIncreaseMilestone;
    private float speedIncreaseMilestoneStore;
    private Sprite defaultPlayerSprite;
    private RuntimeAnimatorController defaultPlayerAnimator;

    private WildIndicator wildIndicator;
    [SerializeField] private float wildForce;
    private bool wildSingle;
    private float wildMoveSpeed;

    private Vector2 currentPlayerPosition;

    private bool isSkeleton;

    [SerializeField] private float skeletonTime;
    private float skeletonTimeCount;

    private float speedMilestoneCount;
    private float speedMilestoneCountStore;

    [SerializeField] private float jumpForce;

    [SerializeField] private float jumpTime;
    private float jumpTimeCount;

    private bool stoppedJumping;
    private bool canDoubleJump;

    [SerializeField] private bool isGround;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float groundCheckerRadius;

    private Animator playerAnimator;

    private Rigidbody2D playerRigidbody;

    private GameManager gameManager;

    [SerializeField] private AudioSource runSound;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource spawnSound;
 
    private void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        playerAnimator = gameObject.GetComponentInChildren<Animator>();
        jumpTimeCount = jumpTime;
        speedMilestoneCount = speedIncreaseMilestone;
        gameManager = FindObjectOfType<GameManager>();
        moveSpeedStore = moveSpeed;
        speedMilestoneCountStore = speedMilestoneCount;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;
        stoppedJumping = true;
        isSkeleton = false;
        skeletonTimeCount = skeletonTime;
        defaultPlayerSprite = gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
        defaultPlayerAnimator = gameObject.GetComponentInChildren<Animator>().runtimeAnimatorController;
        liveManager = FindObjectOfType<LiveManager>();
        wildIndicator = FindObjectOfType<WildIndicator>();
        wildMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        if(wildIndicator.GetWildRotate() && !wildSingle)
        {
            moveSpeed = wildMoveSpeed;
            moveSpeed += wildForce;
            wildSingle = true;
        }
        else
        {
            if(!wildIndicator.GetWildRotate() && !wildSingle)
            {
                moveSpeed = wildMoveSpeed;
                moveSpeed -= wildForce;
                wildSingle = true;
            }
        }

        isGround = Physics2D.OverlapCircle(groundChecker.position, groundCheckerRadius, groundMask);

        if(transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;
            speedIncreaseMilestone *= speedForcer;
            moveSpeed *= speedForcer;
            wildMoveSpeed *= speedForcer;
        }

        playerRigidbody.velocity = new Vector2(moveSpeed, playerRigidbody.velocity.y);

        if (isGround && !runSound.isPlaying)
        {
            runSound.Play();
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
                stoppedJumping = false;
                currentPlayerPosition = gameObject.transform.position;
                jumpSound.Play();
            }

            if(!isGround && canDoubleJump)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.position.x, jumpForce);

                jumpTimeCount = jumpTime;
                stoppedJumping = false;
                canDoubleJump = false;
                jumpSound.Play();
            }
        }

        if(Input.GetButton("Jump") && !stoppedJumping)
        {
            if(jumpTimeCount > 0)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
                jumpTimeCount -= Time.deltaTime;
            }
        }

        if(Input.GetButtonUp("Jump"))
        {
            jumpTimeCount = 0;
            stoppedJumping = true;
        }

        if (isGround)
        {
            jumpTimeCount = jumpTime;
            canDoubleJump = true;
        }

        playerAnimator.SetFloat("Speed", playerRigidbody.velocity.x);
        playerAnimator.SetBool("IsGrounded", isGround);

        if (isSkeleton)
        {
            if(skeletonTimeCount > 0)
            {
                skeletonTimeCount -= Time.deltaTime;
            }
            else
            {
                isSkeleton = false;
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = defaultPlayerSprite;
                gameObject.GetComponentInChildren<Animator>().runtimeAnimatorController = defaultPlayerAnimator;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "KillZone")
        {
            if (isSkeleton)
            {
                gameObject.transform.position = currentPlayerPosition;
                spawnSound.Play();
            }
            else
            {
                if (liveManager.GetHeartValue() <= 1)
                {
                    gameManager.Restart();
                    moveSpeed = moveSpeedStore;
                    speedMilestoneCount = speedMilestoneCountStore;
                    speedIncreaseMilestone = speedIncreaseMilestoneStore;
                }
                else
                {
                    liveManager.MinusHeart();
                    gameObject.transform.position = currentPlayerPosition;
                    spawnSound.Play();
                }

                if (deathSound.isPlaying)
                {
                    deathSound.Stop();
                    deathSound.Play();
                }
                else
                {
                    deathSound.Play();
                }
            }
        }
    }

    public void SetTransformSkeleton(bool value)
    {
        isSkeleton = value;
        skeletonTimeCount = skeletonTime;
    }

    public bool GetTransformSkeleton()
    {
        return isSkeleton;
    }

    public void RunWild()
    {
        wildSingle = false;
    }
}