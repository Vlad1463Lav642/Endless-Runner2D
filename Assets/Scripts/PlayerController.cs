using UnityEngine;

/// <summary>
/// Обеспечивает управление игроком.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed; //Скорость игрока.

    private float moveSpeedStore; //Начальная скорость игрока.

    private LiveManager liveManager;

    [SerializeField] private float speedForcer; //Ускорение игрока.

    [SerializeField] private float speedIncreaseMilestone; //Расстояние которое необходимо пройти чтобы скорость увеличилась.
    private float speedIncreaseMilestoneStore; //Счетчик для смещения расстояния по пути игрока.
    private Sprite defaultPlayerSprite; //Начальный спрайт игрока.
    private RuntimeAnimatorController defaultPlayerAnimator; //Начальный аниматор игрока.

    private WildIndicator wildIndicator;
    [SerializeField] private float wildForce; //Сила ветра.
    private bool wildSingle; //Обеспечивает срабатывание ветра один раз.
    private float wildMoveSpeed; //Предотвращает суммирование силы ветра на скорость.

    private Vector2 currentPlayerPosition; // Последняя позиция игрока.
    private bool isRespawned;

    private bool isSkeleton;

    [SerializeField] private float skeletonTime; //Время действия свитка скелета на игрока.
    private float skeletonTimeCount; // Счетчик времени действия свитка.

    private float speedMilestoneCount; //Счетчик пройденного расстояния.
    private float speedMilestoneCountStore;

    [SerializeField] private float jumpForce; //Сила прыжка.

    [SerializeField] private float jumpTime; //Время прыжка.
    private float jumpTimeCount; //Счетчик времени прыжка.

    private bool stoppedJumping; //Возможность совершить прыжок.
    private bool canDoubleJump; //Возможность совершить двойной прыжок.

    [SerializeField] private bool isGround; //Булевое значение хранящее информацию о том, находится ли игрок на платформе.
    [SerializeField] private LayerMask groundMask; //Слой платформы.
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
        isGround = Physics2D.OverlapCircle(groundChecker.position, groundCheckerRadius, groundMask); //Находится ли игрок на платформе.

        if (!isRespawned) //Если игроку не нужно возраждаться.
        {
            if (wildIndicator.GetWildRotate() && !wildSingle) //Если ветер дует по направлению движения игрока и еще не влиял на его движение.
            {
                moveSpeed = wildMoveSpeed;
                moveSpeed += wildForce;
                wildSingle = true;
            }
            else
            {
                if (!wildIndicator.GetWildRotate() && !wildSingle) //Если ветер дует против направления движения игрока и еще не влиял на его движение.
                {
                    moveSpeed = wildMoveSpeed;
                    moveSpeed -= wildForce;
                    wildSingle = true;
                }
            }

            if (transform.position.x > speedMilestoneCount) //Увеличивает скорость игрока каждые 50 метров.
            {
                speedMilestoneCount += speedIncreaseMilestone;
                speedIncreaseMilestone *= speedForcer;
                moveSpeed *= speedForcer;
                wildMoveSpeed *= speedForcer;
            }

            playerRigidbody.velocity = new Vector2(moveSpeed, playerRigidbody.velocity.y);
        }

        if (isGround && !runSound.isPlaying) //Если игрок находится на платформе и звук движения еще не проигрывается.
        {
            runSound.Play();
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
                stoppedJumping = false;
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
            currentPlayerPosition = gameObject.transform.position;
            isRespawned = false;
        }

        playerAnimator.SetFloat("Speed", playerRigidbody.velocity.x);
        playerAnimator.SetBool("IsGrounded", isGround);

        if (isSkeleton && skeletonTimeCount > 0) //Если на игрока все еще действует свиток скелета.
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "KillZone") //Если игрок столкнулся с смертельной областью.
        {
            if (isSkeleton)
            {
                gameObject.transform.position = currentPlayerPosition;
                spawnSound.Play();

                isRespawned = true;
            }
            else
            {
                if (liveManager.GetHeartValue() <= 1) //Если количество жизней игрока < 1.
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

                    isRespawned = true;
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

    /// <summary>
    /// Устанавливает действие свитка на игрока.
    /// </summary>
    /// <param name="value">Действует ли список на игрока?</param>
    public void SetTransformSkeleton(bool value)
    {
        isSkeleton = value;
        skeletonTimeCount = skeletonTime;
    }

    /// <summary>
    /// Возвращает значение, воздействует ли свиток скелета на игрока.
    /// </summary>
    /// <returns>Текущее значение (true/false).</returns>
    public bool GetTransformSkeleton()
    {
        return isSkeleton;
    }

    /// <summary>
    /// Запускает воздействие ветра на скорость игрока.
    /// </summary>
    public void RunWild()
    {
        wildSingle = false;
    }

    /// <summary>
    /// Возвращает значение, находится ли игрок на платформе.
    /// </summary>
    /// <returns>Текущее значение (true/false).</returns>
    public bool GetIsGround()
    {
        return isGround;
    }
}