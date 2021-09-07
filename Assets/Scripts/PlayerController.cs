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
    //[SerializeField] private bool isJumped;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float groundCheckerRadius;

    private Animator playerAnimator;

    private Rigidbody2D playerRigidbody;

    private GameManager gameManager;

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
        //isJumped = false;
        isSkeleton = false;
        skeletonTimeCount = skeletonTime;
        defaultPlayerSprite = gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
        defaultPlayerAnimator = gameObject.GetComponentInChildren<Animator>().runtimeAnimatorController;
        liveManager = FindObjectOfType<LiveManager>();
    }

    private void Update()
    {
        isGround = Physics2D.OverlapCircle(groundChecker.position, groundCheckerRadius, groundMask);

        if(transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;
            speedIncreaseMilestone *= speedForcer;
            moveSpeed *= speedForcer;
        }

        playerRigidbody.velocity = new Vector2(moveSpeed, playerRigidbody.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
                stoppedJumping = false;
                currentPlayerPosition = gameObject.transform.position;
                //isJumped = true;
            }

            if(!isGround && canDoubleJump)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.position.x, jumpForce);

                jumpTimeCount = jumpTime;
                stoppedJumping = false;
                canDoubleJump = false;
                //isJumped = true;
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
            //isJumped = false;
        }

        playerAnimator.SetFloat("Speed", playerRigidbody.velocity.x);
        playerAnimator.SetBool("IsGrounded", isGround);
        //playerAnimator.SetBool("IsJumped", isJumped);

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
                }
            }
        }
    }

    public void SetTransformSkeleton(bool value)
    {
        isSkeleton = value;
    }

    public bool GetTransformSkeleton()
    {
        return isSkeleton;
    }
}