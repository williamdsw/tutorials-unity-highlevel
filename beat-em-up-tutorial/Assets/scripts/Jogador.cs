using UnityEngine;
using UnityEngine.SceneManagement;

public class Jogador : MonoBehaviour
{
    [SerializeField] private float velocitySpeed = 4f;
    private float actualVelocitySpeed = 0f;
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private int maxLife = 15;
    private int actualLife = 0;
    [SerializeField] private string playerName = string.Empty;
    [SerializeField] private float minScenarioHeight = -8.4f;
    [SerializeField] private float maxScenarioHeight = -1.6f;
    [SerializeField] private Sprite spritePlayer;
    [SerializeField] private AudioClip clipCollision;
    [SerializeField] private AudioClip clipJump;
    [SerializeField] private AudioClip clipFood;
    [SerializeField] private Arma weapon;

    private Rigidbody rigidBody;
    private Animator animator;
    private Transform transformGroundChecker;
    private AudioSource audioSource;

    private GerenciadorUI uiManager;
    private GameManager gameManager;

    private bool isOnGround = false;
    private bool isDead = false;
    private bool isFacingRight = true;
    private bool isJumping = false;
    private bool isHoldingWeapon = false;

    public int MaxLife { get => maxLife; set => maxLife = value; }
    public string PlayerName { get => playerName; set => playerName = value; }
    public Sprite SpritePlayer { get => spritePlayer; set => spritePlayer = value; }

    private void Awake()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
        audioSource = this.GetComponent<AudioSource>();
    }

    private void Start()
    {
        uiManager = FindObjectOfType<GerenciadorUI>();
        gameManager = FindObjectOfType<GameManager>();
        transformGroundChecker = this.gameObject.transform.Find("ChaoVerificador");

        actualVelocitySpeed = velocitySpeed;
        actualLife = MaxLife;
    }

    private void Update()
    {
        /* Verifica se existe o player esta no chao */
        isOnGround = Physics.Linecast(this.transform.position, transformGroundChecker.position, 1 << LayerMask.NameToLayer("Chao"));

        /* Seta booleanos do animator */
        animator.SetBool("OnGround", isOnGround);
        animator.SetBool("Dead", isDead);
        animator.SetBool("Weapon", isHoldingWeapon);

        /* Verifica se esta pulando */
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            isJumping = true;
        }

        /* Verifica se esta atacando */
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            vertical = (!isOnGround ? 0 : vertical);
            rigidBody.velocity = new Vector3(horizontal * actualVelocitySpeed, rigidBody.velocity.y, vertical * actualVelocitySpeed);

            if (isOnGround)
            {
                animator.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.magnitude));
            }

            if (horizontal > 0 && !isFacingRight)
            {
                FlipPlayer();
            }
            else if (horizontal < 0 && isFacingRight)
            {
                FlipPlayer();
            }

            if (isJumping)
            {
                isJumping = false;
                rigidBody.AddForce(Vector2.up * jumpForce);
                PlaySong(clipJump);
            }

            float minCameraWidth = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
            float maxCameraWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
            float clampX = Mathf.Clamp(rigidBody.position.x, minCameraWidth + 1, maxCameraWidth - 1);
            float clampZ = Mathf.Clamp(rigidBody.position.z, minScenarioHeight, maxScenarioHeight);
            rigidBody.position = new Vector3(clampX, rigidBody.position.y, clampZ);
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Item de Vida"))
        {
            if (Input.GetButtonDown("Fire2"))
            {
                Destroy(collider.gameObject);
                animator.SetTrigger("Catching");
                PlaySong(clipFood);
                actualLife = MaxLife;
                uiManager.UpdateLifeBar(actualLife);
            }
        }

        if (collider.CompareTag("Arma"))
        {
            if (Input.GetButtonDown("Fire2"))
            {
                animator.SetTrigger("Catching");
                isHoldingWeapon = true;
                ArmaItem armaItem = collider.GetComponent<ArmaColetavel>().Weapon;
                weapon.AtivarArma(armaItem.Sprite, armaItem.Color, armaItem.Durability, armaItem.Damage);
                Destroy(collider.gameObject);
            }
        }
    }

    private void FlipPlayer()
    {
        isFacingRight = !isFacingRight;
        Vector3 escale = this.transform.localScale;
        escale.x *= -1;
        this.transform.localScale = escale;
    }

    private void ResetSpeed()
    {
        actualVelocitySpeed = 0;
    }

    private void BackSpeed()
    {
        actualVelocitySpeed = velocitySpeed;
    }

    private void PlayerRespawn()
    {
        if (gameManager.NumberOfLifes > 0)
        {
            isDead = false;
            uiManager.UpdateNumberOfLifes();
            actualLife = MaxLife;
            uiManager.UpdateLifeBar(actualLife);

            animator.Rebind();
            float minCameraWidth = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
            transform.position = new Vector3(minCameraWidth, 10, -4);
        }
        else
        {
            uiManager.UpdateMessage("Game Over");
            Destroy(gameManager);
            Invoke("LoadScene", 2f);
        }
    }

    private void CarregarCena()
    {
        SceneManager.LoadScene(0);
    }

    public void TakeDamage(int value)
    {
        if (!isDead)
        {
            actualLife -= value;
            animator.SetTrigger("HitDamage");
            PlaySong(clipCollision);
            uiManager.UpdateLifeBar(actualLife);

            if (actualLife <= 0)
            {
                isDead = true;
                gameManager.NumberOfLifes--;

                Vector3 force = (isFacingRight ? new Vector3(-3, 5, 0) : new Vector3(3, 5, 0));
                rigidBody.AddForce(force, ForceMode.Impulse);
            }
        }
    }

    public void PlaySong(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void UnableWeapon()
    {
        isHoldingWeapon = false;
    }
}