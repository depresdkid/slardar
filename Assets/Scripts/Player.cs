using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IMove
{
    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float jumpForse;
    [SerializeField] private float dashForse = 3000f;
    [SerializeField] private Vector2 moveVector;
    [SerializeField] private float deshReload;

    float timer = 0;    
    //Animator animator;
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
    private Rigidbody2D rbPlayer;
    private SpriteRenderer spritePlayer;

    private bool isReady = true;
    private int doubleJump = 1;
    private bool isLeft = false;
    private float velosity;
    public static Player player;
    float Health {
        get {
            return Mathf.Round(health);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "BotGround")
        {
            isReady = true;
            doubleJump = 1;
            print("На земле");
        }

    }
    //получить урон
    public void GetDamage(int damage) {
        health -= damage;
    }
    private void Awake()
    {
        player = this;
        rbPlayer = GetComponent<Rigidbody2D>();
        spritePlayer = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();

    }
    void playerDash() {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {            
            rbPlayer.velocity = new Vector2(0, 0);

            if (velosity == 0)
            {
                if (isLeft)
                {
                    //animator.SetTrigger("Desh");
                    rbPlayer.AddForce(Vector2.left * dashForse);
                }
                else
                {
                    //animator.SetTrigger("Desh");
                    rbPlayer.AddForce(Vector2.right * dashForse);
                }

            }
            else
            {
                if (isLeft)
                {
                    //animator.SetTrigger("Desh");
                    rbPlayer.AddForce(Vector2.left * dashForse / 2);
                }
                else
                {
                    //animator.SetTrigger("Desh");
                    rbPlayer.AddForce(Vector2.right * dashForse / 2);
                }
            }
            timer = deshReload;

        }

    }
    void playerMove()
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
           // animator.SetBool("IsRunning",true);
        }
        //else
           // animator.SetBool("IsRunning", false);
        
    }
    void playerJump() {

        rbPlayer.velocity = new Vector2(0, 0);
        rbPlayer.velocity = Vector2.up * jumpForse;
        
    }
    public void Moving()
    {
        playerMove();
        
    }   
    
    private void FixedUpdate()
    {
        //если песнонаж умер он не двигается
        if (isAlive)
        {
            Moving();
        }
        
    }
    private void Update()
    {
        if (isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isReady)
            {
                playerJump();
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
                playerDash();
            }
            else
            {
                timer -= Time.deltaTime;
            }
            UI.deshReload = (float)System.Math.Round(timer, 1);
            UI.playerHealth = System.Convert.ToInt32(Health);
        }
    }

}
