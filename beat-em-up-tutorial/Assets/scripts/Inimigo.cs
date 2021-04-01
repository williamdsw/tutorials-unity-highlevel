using UnityEngine;

namespace BeatEmUpTutorial
{
    public class Inimigo : MonoBehaviour
    {
        [SerializeField] private float maximumVelocity = 0f;
        [SerializeField] private float minScenarioHeight = 0f;
        [SerializeField] private float maxScenarioHeight = 0f;
        [SerializeField] private float damageTimer = 0.5f;
        [SerializeField] private float attackRate = 1f;
        [SerializeField] private int maxLife = 100;
        [SerializeField] private string enemyName = string.Empty;
        [SerializeField] private Sprite spriteFace;
        [SerializeField] private AudioClip clipCollision;
        [SerializeField] private AudioClip clipDeath;

        private float actualVelocitySpeed;
        private float velocitySpeedZ;
        private float walkTimer;
        private int actualLife;
        private float actualDamageTimer;
        private float nextAttack;

        private bool isOnGround = false;
        private bool isTakingDamage = false;
        protected bool isFacingRight = false;
        protected bool isDead = false;

        private Rigidbody rigidBody;
        private Transform transformGroundChecker;
        private AudioSource audioSource;
        protected Animator animator;

        private Transform transformPlayer;
        private GerenciadorUI uiManager;

        private void Awake()
        {
            rigidBody = this.GetComponent<Rigidbody>();
            animator = this.GetComponent<Animator>();
            audioSource = this.GetComponent<AudioSource>();
        }

        private void Start()
        {
            transformGroundChecker = transform.Find("ChaoVerificador");
            transformPlayer = FindObjectOfType<Jogador>().transform;
            uiManager = FindObjectOfType<GerenciadorUI>();

            actualLife = maxLife;
        }

        private void Update()
        {
            isOnGround = Physics.Linecast(this.transform.position, transformGroundChecker.position, 1 << LayerMask.NameToLayer("Chao"));

            animator.SetBool("Grounded", isOnGround);
            animator.SetBool("Dead", isDead);

            if (!isDead)
            {
                isFacingRight = (transformPlayer.position.x < transform.position.x) ? false : true;
                transform.eulerAngles = (isFacingRight ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0));
            }

            if (isTakingDamage && !isDead)
            {
                actualDamageTimer += Time.deltaTime;

                if (actualDamageTimer >= damageTimer)
                {
                    isTakingDamage = false;
                    actualDamageTimer = 0;
                }
            }

            walkTimer += Time.deltaTime;
        }

        private void FixedUpdate()
        {
            if (!isDead)
            {
                /* Define distancia e forca horizontal */
                Vector3 distanceToPlayer = (transformPlayer.position - transform.position);
                float horizontalForce = (distanceToPlayer.x / Mathf.Abs(distanceToPlayer.x));

                /* Define valor do timer de movimentacao */
                if (walkTimer >= Random.Range(1f, 2f))
                {
                    velocitySpeedZ = Random.Range(-1, 2);
                    walkTimer = 0;
                }

                /* Para o inimigo se estiver perto do player */
                if (Mathf.Abs(distanceToPlayer.x) < 1.5f)
                {
                    horizontalForce = 0;
                }

                if (!isTakingDamage)
                {
                    rigidBody.velocity = new Vector3(horizontalForce * actualVelocitySpeed, 0, velocitySpeedZ * actualVelocitySpeed);
                }

                animator.SetFloat("Speed", Mathf.Abs(actualVelocitySpeed));

                /* Distancia para atacar */
                if (Mathf.Abs(distanceToPlayer.x) < 1.5f && Mathf.Abs(distanceToPlayer.z) < 1.5f && Time.time > nextAttack)
                {
                    animator.SetTrigger("Attack");
                    actualVelocitySpeed = 0;
                    nextAttack = (Time.time + attackRate);
                }
            }

            rigidBody.position = new Vector3(rigidBody.position.x, rigidBody.position.y, Mathf.Clamp(rigidBody.position.z, minScenarioHeight, maxScenarioHeight));
        }

        protected void ResetSpeed()
        {
            actualVelocitySpeed = maximumVelocity;
        }

        public void TakeDamage(int value)
        {
            if (!isDead)
            {
                isTakingDamage = true;
                actualLife -= value;
                animator.SetTrigger("HitDamage");
                PlaySong(clipCollision);

                uiManager.UpdateEnemyUI(maxLife, actualLife, enemyName, spriteFace);

                if (actualLife <= 0)
                {
                    isDead = true;
                    rigidBody.AddRelativeForce(new Vector3(3, 5, 0), ForceMode.Impulse);
                    PlaySong(clipDeath);
                }
            }
        }

        public void Disable()
        {
            this.gameObject.SetActive(false);
        }

        public void PlaySong(AudioClip audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}