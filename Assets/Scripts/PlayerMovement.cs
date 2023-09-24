using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    //Portade Variabler från Hugo
    [SerializeField] private Transform attackPointR;
    [SerializeField] private Transform attackPointL;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayer;

    public bool isAttacking = false;
    public bool canMove;

    private bool canJump = true;
    private float verticalValue;
    private bool facingRight = true;
    private float attackRate = 3f;
    private float nextAttackTime = 0f;
    private Animator _animator;


    //Variables
    private float horizontalValue;
    private Rigidbody2D rgbd;
    private SpriteRenderer rend;
    private bool isGrounded;
    private float rayDistance = 0.25f;
    private Animator anim;
    private AudioSource audioSource;
    private int startingHealth = 3;
    private int currentHealth = 0;

    //private int spellscrollCollected = 0;

    public int coinsCollected = 0;

    //Public variables
    public AudioSource footstepsSound;


    //Changeable variables in Unity
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private Transform leftFoot, rightFoot;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private AudioClip jumpSound;

    [SerializeField] private AudioClip hitSound;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text coinText;

    // Start is called before the first frame update
    void Start()
    {
        coinText.text = "" + coinsCollected;
        currentHealth = startingHealth;
        rgbd = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        //Från Hugo
        if (!isAttacking)
        {
            horizontalValue = Input.GetAxis("Horizontal");
        }
        else
        {
            horizontalValue = 0;
        }

        if (horizontalValue < 0)
        {
            bool facingRight = false;
            FlipSprite(true);
        }
        if (horizontalValue > 0)
        {
            bool facingRight = true;
            FlipSprite(false);
        }

        verticalValue = Input.GetAxis("Vertical");



        //Om det har gått tillräckligt med tid för att spelaren ska kunna attackera och man trycker på attack, gör Attack() funktionen och se till att man inte kan attackera igen förrän tid / attack rate har passerat.

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Attack"))
            {
                Attack();
                nextAttackTime = Time.time + 1 / attackRate;
            }
        }





        horizontalValue = Input.GetAxis("Horizontal");

        if (horizontalValue < 0)
        {
            FlipSprite(true);
        }

        if (horizontalValue > 0)
        {
            FlipSprite(false);
        }

        CheckIfGrounded();

        if (Input.GetButtonDown("Jump") && CheckIfGrounded() == true)
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.A) && CheckIfGrounded() == true || Input.GetKey(KeyCode.D) && CheckIfGrounded() == true)
        {

            footstepsSound.enabled = true;
        }
        else
        {
            footstepsSound.enabled = false;
        }

        anim.SetFloat("MoveSpeed", Mathf.Abs(rgbd.velocity.x));
        anim.SetFloat("VerticalSpeed", rgbd.velocity.y);
        anim.SetBool("IsGrounded", CheckIfGrounded());

    }

    //Fixed Update
    private void FixedUpdate()
    {
        rgbd.velocity = new Vector2(horizontalValue * moveSpeed * Time.deltaTime, rgbd.velocity.y);

        if (!canMove)
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coinsCollected++;
            coinText.text = "" + coinsCollected;
        }

        if (other.CompareTag("Health"))
        {
            RestoreHealth(other.gameObject);
        }

    }

    private void FlipSprite(bool direction)
    {
        rend.flipX = direction;
    }

    private void Jump()
    {
        rgbd.AddForce(new Vector2(0, jumpForce));
        audioSource.PlayOneShot(jumpSound, 0.8f);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        currentHealth = startingHealth;
        UpdateHealthBar();
        transform.position = spawnPosition.position;
        rgbd.velocity = Vector2.zero;
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;
    }

    private void RestoreHealth(GameObject healthPickup)
    {
        if (currentHealth >= startingHealth)
        {
            return;
        }

        else
        {
            currentHealth += 1;
            UpdateHealthBar();
            Destroy(healthPickup);
        }

    }

    private bool CheckIfGrounded()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, rayDistance, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, rayDistance, whatIsGround);

        //Visual test if grounded 
        /*Debug.DrawRay(leftFoot.position, Vector2.down * rayDistance, Color.blue, 0.25f);
        Debug.DrawRay(rightFoot.position, Vector2.down * rayDistance, Color.red, 0.25f);
        */


        if (leftHit.collider != null && leftHit.collider.CompareTag("Ground") || rightHit.collider != null && rightHit.collider.CompareTag("Ground"))
        {
            return true;
        }

        else
        {
            return false;
        }



    }

    private void Attack()
    {
        if (CheckIfGrounded())
        {
            //Get animationstate info
            AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(hitSound, 0.5f);
            if (currentState.IsName("PlayerIdle") || currentState.IsName("PlayerRun"))
            {
                //Play animation
                _animator.SetTrigger("AttackOne");
                isAttacking = true;
                //Check what direction we're facing

                if (facingRight)
                {   //Detect enemies in attack range
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointR.position, attackRange, enemyLayer);
                    //Deal damage
                    foreach (Collider2D enemy in hitEnemies)
                    {
                        enemy.gameObject.GetComponent<EnemyMovement>().TakeDamage();
                        Debug.Log("We hit " + enemy.name + " on our right with attack 1.");
                    }
                }

                if (!facingRight)
                {
                    //Detect enemies in attack range
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointL.position, attackRange, enemyLayer);
                    //Deal damage
                    foreach (Collider2D enemy in hitEnemies)
                    {
                        enemy.gameObject.GetComponent<EnemyMovement>().TakeDamage();
                        Debug.Log("We hit " + enemy.name + " on our left with attack 1.");
                    }
                }
                return;
            }





            if (currentState.IsName("Cleric_Attack1") || currentState.IsName("Cleric_Attack_Transition1"))
            {
                //Play animation
                _animator.SetTrigger("AttackTwo");
                isAttacking = true;
                //Check what direction we're facing

                if (facingRight)
                {   //Detect enemies in attack range
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointR.position, attackRange, enemyLayer);
                    //Deal damage
                    foreach (Collider2D enemy in hitEnemies)
                    {
                        enemy.gameObject.GetComponent<EnemyMovement>().TakeDamage();
                        Debug.Log("We hit " + enemy.name + " on our right with attack 2.");
                    }
                }

                if (!facingRight)
                {
                    //Detect enemies in attack range
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointL.position, attackRange, enemyLayer);
                    //Deal damage
                    foreach (Collider2D enemy in hitEnemies)
                    {
                        enemy.gameObject.GetComponent<EnemyMovement>().TakeDamage();
                        Debug.Log("We hit " + enemy.name + " on our left with attack 2.");
                    }
                }

                return;
            }
            if (currentState.IsName("Cleric_Attack2") || currentState.IsName("Cleric_Attack_Transition2"))
            {
                //Play animation
                _animator.SetTrigger("AttackThree");
                isAttacking = true;
                //Check what direction we're facing

                if (facingRight)
                {   //Detect enemies in attack range
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointR.position, attackRange, enemyLayer);
                    //Deal damage
                    foreach (Collider2D enemy in hitEnemies)
                    {
                        enemy.gameObject.GetComponent<EnemyMovement>().TakeDamage();
                        Debug.Log("We hit " + enemy.name + " on our right with attack 3.");
                    }
                }

                if (!facingRight)
                {
                    //Detect enemies in attack range
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointL.position, attackRange, enemyLayer);
                    //Deal damage
                    foreach (Collider2D enemy in hitEnemies)
                    {
                        enemy.gameObject.GetComponent<EnemyMovement>().TakeDamage();
                        Debug.Log("We hit " + enemy.name + " on our left with attack 3.");
                    }
                }

                return;
            }
            else
            {
                return;
            }

        }


    }

}
