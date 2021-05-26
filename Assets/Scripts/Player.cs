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
    [SerializeField] private Slider sliderHealth;

    public static Player player;
    public bool isReady = true;
    public int doubleJump = 1;
    public float timer = 0;


    private Rigidbody2D rbPlayer;
    private SpriteRenderer spritePlayer;


    private bool isLeft = false;
    private float velosity;


    //Animator animator;

    //получаем максимальное кол-во хп дл€ health bar
    public void SetMaxHealth(float health)
    {
        sliderHealth.maxValue = health;
        sliderHealth.value = health;
    }

    //получаем текущее кол-во хп дл€ health bar
    public void SetHealth(float health)
    {
        sliderHealth.value = health;
    }

    //персонаж живой
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
    //тво€ хуйн€ макс пон€ти€ не имею что она делает
    //не ебЄт реально B)
    float Health {
        get {
            return Mathf.Round(health);
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

    //рывок
    void PlayerDash() {
        //обновл€ем вектор чтоб не складывать скорости
        // кстати он не работает :D (или работает но не так как надо)
        rbPlayer.velocity = new Vector2(0, 0);

        //проверка дл€ деша в сто€чем состо€нии и при движении
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
    //движение игрока
    void PlayerMove()
    {
        velosity = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * velosity * speed * Time.deltaTime);
        //поворот спарйта 
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

    //прыжок
    void PlayerJump() {

        rbPlayer.velocity = new Vector2(0, 0);
        rbPlayer.velocity = Vector2.up * jumpForse;
        
    }
    //реализаци€ интерфейса
    public void Moving()
    {
        PlayerMove();
        
    }

    //обновл€ем максимальное значение дл€ слайдера healt bar вызовом метода
    private void Start()
    {
        SetMaxHealth(maxHealth);
    }
    private void FixedUpdate()
    {
        //если песнонаж умер он не двигаетс€
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
                if (Input.GetKeyDown(KeyCode.LeftShift))
                    PlayerDash();
            }
            else
            {
                timer -= Time.deltaTime;
            }

            UI.deshReload = (float)System.Math.Round(timer, 1);
            UI.playerHealth = System.Convert.ToInt32(Health);
        }
        //обновл€ем текущее хп
        SetHealth(health);
    }

}
