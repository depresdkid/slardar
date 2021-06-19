using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IMove
{
    [SerializeField] private float speed;
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float jumpForse;
    [SerializeField] private float dashForse = 3000f;
    [SerializeField] private Vector2 moveVector;
    [SerializeField] private float deshReload;
    [SerializeField] private float cooldownAttack;
    [SerializeField] private Slider sliderHealth;
    [SerializeField] private AudioSource audioDesh;
    [SerializeField] GameObject attackHit;

    public static Player player;
    public bool isReady = true;
    public int doubleJump = 1;
    public float timer = 0;
    public float attackTimer = 0;



    private Rigidbody2D rbPlayer;
    SpriteRenderer spritePlayer;


    private bool isLeft = false, isFly = false, isAttacking = false;
    private float velosity;


    Animator animator;


    public void SetMaxHealth(float health)
    {
        sliderHealth.maxValue = health;
        sliderHealth.value = health;
    }


    public void SetHealth(float health)
    {
        sliderHealth.value = health;
    }


    public bool isAlive {
        get {
            if (health > 0)
            {
                return true;
            }
            else
                return false;
        }
    }
    public float Health {

        get {
            return Mathf.Round(health);
        }
        set {
            health = value;
        }
    }

    public void GetDamage(int damage) {
        health -= damage;
    }
    private void Awake()
    {
        player = this;
        rbPlayer = GetComponent<Rigidbody2D>();
        spritePlayer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            isFly = false;
        }
    }

    void PlayerDash() {

        rbPlayer.velocity = new Vector2(0, 0);
        audioDesh.Play();

        if (velosity == 0)
        {
            if (isLeft)
            {
                rbPlayer.AddForce(Vector2.left * dashForse);
            }
            else
            {
                rbPlayer.AddForce(Vector2.right * dashForse);
            }

            }
        else
        {

            if (isLeft)
            {
                rbPlayer.AddForce(Vector2.left * dashForse / 1.3f);
            }
            else
            {
                
                rbPlayer.AddForce(Vector2.right * dashForse / 1.3f);
            }
        }
        timer = deshReload;        
        animator.SetTrigger("Desh");

    }

    void PlayerMove()
    {
        velosity = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * velosity * speed * Time.deltaTime);

        if (isLeft)
        {
            spritePlayer.flipX = false;
        }
        else {
            spritePlayer.flipX = true;
        }
        if (velosity > 0)
        {
            isLeft = false;

        }
        else if (velosity < 0)
        {
            isLeft = true;

        }
        if (velosity!=0)
        {
            animator.SetBool("IsRun",true);
        }
        else
            animator.SetBool("IsRun", false);
        
    }


    void PlayerJump() {
        isFly = true;
        animator.SetTrigger("Jump");
        rbPlayer.velocity = new Vector2(0, 0);
        rbPlayer.velocity = Vector2.up * jumpForse;
        
    }

    public void Moving()
    {
        PlayerMove();
        
    }


    private void Start()
    {
        SetMaxHealth(maxHealth);
        attackHit.SetActive(false);
    }
    private void FixedUpdate()
    {

        if (isAlive)
        {
            Moving();
        }
        
    }

    private void Update()
    {
        print(isFly);
        if (isFly == false)
        {
            animator.SetTrigger("IsFall");
        }
        if (transform.position.y<0)
        {
            health = 0;
        }
        if (isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isReady)
            {
                PlayerJump();
                doubleJump++;
                if (doubleJump < 3)
                {
                    isReady = true;
                }
                else
                {
                    isReady = false;
                }
            }
            if (timer <= 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    PlayerDash();
            }
            else
            {
                timer -= Time.deltaTime;
            }
            //tyt ataka vot
            if (attackTimer <= 0)
            {
                isAttacking = false;
                if (Input.GetButtonDown("Fire1") && !isAttacking)
                {
                    attackHit.SetActive(true);
                    isAttacking = true;
                    animator.Play("Atack");
                    attackTimer = cooldownAttack;
                }
            }
            else
            {

                attackTimer -= Time.deltaTime;
            }
            
            UI.deshReload = (float)System.Math.Round(timer, 1);
            UI.playerHealth = System.Convert.ToInt32(Health);
        }

        SetHealth(health);
    }

}
