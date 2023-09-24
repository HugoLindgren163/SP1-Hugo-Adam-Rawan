//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class PlayerMovement : MonoBehaviour
//{
//    [SerializeField]
//    private float movementSpeed = 300f;
//    [SerializeField]
//    private float jumpHeight = 25f;
//    [SerializeField]
//    private Transform leftFoot, rightFoot;
//    [SerializeField]
//    private LayerMask whatIsGround;
//    [SerializeField]
//    private Slider healthSlider;
//    [SerializeField]
//    private Image portraitSprite;
//    [SerializeField]
//    private TMP_Text coinText;
//    public float coinsCollected = 0f;
//    [SerializeField]
//    private AudioClip jumpSound, pickupSound, hitSound, hurtSound;


//    private int startingHealth = 2;
//    private float currentHealth = 0;


//    private float horizontalValue;
//    private float verticalValue;
//    private float actualSpeed;
//    private Rigidbody2D rigidbody;
//    private SpriteRenderer renderer;
//    private Animator _animator;


//Här är de variablerna jag använde för attacken. Attackpointsen är två empty gameObjects som ska vara på höger och vänster sida av spelaren. Attackrange lär behöva modifiereas.

//    [SerializeField]
//    private Transform attackPointR;
//    [SerializeField]
//    private Transform attackPointL;
//    [SerializeField]
//    private float attackRange = 0.5f;
//    [SerializeField]
//    private LayerMask enemyLayer;






//    [SerializeField]
//    private Transform spawnPosition;
//    [SerializeField]
//    private Sprite healthMax, healthDamaged, healthDead;


//isAttacking, o de andra här är också något som behövs

//    public bool isAttacking = false;
//    public bool canMove;
//    private bool isGrounded = true;
//    private bool canJump = true;
//    private float rayDistance = 0.15f;
//    private bool facingRight = true;
//    private float attackRate = 3f;
//    private float nextAttackTime = 0f;



//    private AudioSource audioSource;



//    // Start is called before the first frame update
//    void Start()
//    {
//        coinText.text = "X " + coinsCollected;
//        canMove = true;
//        currentHealth = startingHealth;
//        actualSpeed = movementSpeed;
//        rigidbody = GetComponent<Rigidbody2D>();
//        renderer = GetComponent<SpriteRenderer>();
//        _animator = GetComponent<Animator>();
//        audioSource = GetComponent<AudioSource>();
//    }

//    // Update is called once per frame
//    void Update()
//    {


//Om spelaren inte attackerar kan man gå normalt, annars står man still. Titta också om spelaren tittar åt höger eller åt vänster för attacken.


//        if (!isAttacking)
//        {
//            horizontalValue = Input.GetAxis("Horizontal");
//        }
//        else
//        {
//            horizontalValue = 0;
//        }

//        if (horizontalValue < 0)
//        {
//            bool facingRight = false;
//            FlipSprite(true);
//        }
//        if (horizontalValue > 0)
//        {
//            bool facingRight = true;
//            FlipSprite(false);
//        }

//        verticalValue = Input.GetAxis("Vertical");



//Om det har gått tillräckligt med tid för att spelaren ska kunna attackera och man trycker på attack, gör Attack() funktionen och se till att man inte kan attackera igen förrän tid / attack rate har passerat.

//        if (Time.time >= nextAttackTime)
//        {
//            if (Input.GetButtonDown("Attack"))
//            {
//                Attack();
//                nextAttackTime = Time.time + 1 / attackRate;
//            }
//        }


//        if (Input.GetButtonDown("Jump") && CheckIfGrounded() == true && canJump == true)
//        {
//            Jump();
//        }

//        _animator.SetFloat("MovementSpeed", Mathf.Abs(rigidbody.velocity.x));
//        _animator.SetFloat("VerticalSpeed", rigidbody.velocity.y);
//        _animator.SetBool("isGrounded", CheckIfGrounded());

//        CheckIfGrounded();
//    }

//    void FixedUpdate()
//    {
          

        //om spelare inte kan röra på sig, returnera direkt.

//        if (!canMove)
//        {
//            return;
//        }
//        rigidbody.velocity = new Vector2(horizontalValue * actualSpeed * Time.deltaTime, rigidbody.velocity.y);
//    }

//    private void Jump()
//    {
//        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
//        rigidbody.AddForce(new Vector2(0, jumpHeight));
//        audioSource.PlayOneShot(jumpSound, 0.5f);
//    }

//    private bool CheckIfGrounded()
//    {
//        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, rayDistance, whatIsGround);
//        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, rayDistance, whatIsGround);

//        if (leftHit.collider != null || rightHit.collider != null)
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }

//    private void FlipSprite(bool direction)
//    {
//        renderer.flipX = direction;
//    }



//Attack funktionen. Den börjar med att se vilket animationState man är i. Är du I "Cleric_Idle" eller "Cleric_Walk" så byter den till "AttackOne". Är du redan I attack one byter den till attack 2, osv.

//Beroende på om du tittar åt höger eller vänster så skapar den en collider centerad runt attack punkten.

// Du kan nog behöva byta ut referensen till "GhoulMovement" med fiendens script som du har lagt in en "TakeDamage" funktion I.


//    private void Attack()
//    {
//        if (CheckIfGrounded())
//        {
//            //Get animationstate info
//            AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);
//            audioSource.pitch = Random.Range(0.8f, 1.2f);
//            audioSource.PlayOneShot(hitSound, 0.5f);
//            if (currentState.IsName("Cleric_Idle") || currentState.IsName("Cleric_Walk"))
//            {
//                //Play animation
//                _animator.SetTrigger("AttackOne");
//                isAttacking = true;
//                //Check what direction we're facing

//                if (facingRight)
//                {   //Detect enemies in attack range
//                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointR.position, attackRange, enemyLayer);
//                    //Deal damage
//                    foreach (Collider2D enemy in hitEnemies)
//                    {
//                        enemy.gameObject.GetComponent<GhoulMovement>().TakeDamage();
//                        Debug.Log("We hit " + enemy.name + " on our right with attack 1.");
//                    }
//                }

//                if (!facingRight)
//                {
//                    //Detect enemies in attack range
//                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointL.position, attackRange, enemyLayer);
//                    //Deal damage
//                    foreach (Collider2D enemy in hitEnemies)
//                    {
//                        enemy.gameObject.GetComponent<GhoulMovement>().TakeDamage();
//                        Debug.Log("We hit " + enemy.name + " on our left with attack 1.");
//                    }
//                }

//                return;
//            }
//            if (currentState.IsName("Cleric_Attack1") || currentState.IsName("Cleric_Attack_Transition1"))
//            {
//                //Play animation
//                _animator.SetTrigger("AttackTwo");
//                isAttacking = true;
//                //Check what direction we're facing

//                if (facingRight)
//                {   //Detect enemies in attack range
//                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointR.position, attackRange, enemyLayer);
//                    //Deal damage
//                    foreach (Collider2D enemy in hitEnemies)
//                    {
//                        enemy.gameObject.GetComponent<GhoulMovement>().TakeDamage();
//                        Debug.Log("We hit " + enemy.name + " on our right with attack 2.");
//                    }
//                }

//                if (!facingRight)
//                {
//                    //Detect enemies in attack range
//                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointL.position, attackRange, enemyLayer);
//                    //Deal damage
//                    foreach (Collider2D enemy in hitEnemies)
//                    {
//                        enemy.gameObject.GetComponent<GhoulMovement>().TakeDamage();
//                        Debug.Log("We hit " + enemy.name + " on our left with attack 2.");
//                    }
//                }

//                return;
//            }
//            if (currentState.IsName("Cleric_Attack2") || currentState.IsName("Cleric_Attack_Transition2"))
//            {
//                //Play animation
//                _animator.SetTrigger("AttackThree");
//                isAttacking = true;
//                //Check what direction we're facing

//                if (facingRight)
//                {   //Detect enemies in attack range
//                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointR.position, attackRange, enemyLayer);
//                    //Deal damage
//                    foreach (Collider2D enemy in hitEnemies)
//                    {
//                        enemy.gameObject.GetComponent<GhoulMovement>().TakeDamage();
//                        Debug.Log("We hit " + enemy.name + " on our right with attack 3.");
//                    }
//                }

//                if (!facingRight)
//                {
//                    //Detect enemies in attack range
//                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointL.position, attackRange, enemyLayer);
//                    //Deal damage
//                    foreach (Collider2D enemy in hitEnemies)
//                    {
//                        enemy.gameObject.GetComponent<GhoulMovement>().TakeDamage();
//                        Debug.Log("We hit " + enemy.name + " on our left with attack 3.");
//                    }
//                }

//                return;
//            }
//        }
//        else
//        {
//            return;
//        }
//    }
