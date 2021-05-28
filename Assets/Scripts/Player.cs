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
    SpriteRenderer spritePlayer;


    private bool isLeft = false;
    private float velosity;


    Animator animator;

    //ïîëó÷àåì ìàêñèìàëüíîå êîë-âî õï äëÿ health bar
    public void SetMaxHealth(float health)
    {
        sliderHealth.maxValue = health;
        sliderHealth.value = health;
    }

    //ïîëó÷àåì òåêóùåå êîë-âî õï äëÿ health bar
    public void SetHealth(float health)
    {
        sliderHealth.value = health;
    }

    //ïåðñîíàæ æèâîé
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
    //òâîÿ õóéíÿ ìàêñ ïîíÿòèÿ íå èìåþ ÷òî îíà äåëàåò
    //íå åá¸ò ðåàëüíî B)
        get {
            return Mathf.Round(health);
        }
        set {
            health = value;
        }
    }

    //ïîëó÷èòü óðîí
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

    //ðûâîê
    void PlayerDash() {
        //îáíîâëÿåì âåêòîð ÷òîá íå ñêëàäûâàòü ñêîðîñòè
        // êñòàòè îí íå ðàáîòàåò :D (èëè ðàáîòàåò íî íå òàê êàê íàäî)
        rbPlayer.velocity = new Vector2(0, 0);

        //ïðîâåðêà äëÿ äåøà â ñòîÿ÷åì ñîñòîÿíèè è ïðè äâèæåíèè
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
                
                rbPlayer.AddForce(Vector2.right * dashForse / 2);
            }
        }
        timer = deshReload;
        animator.SetTrigger("Desh");

    }
    //äâèæåíèå èãðîêà
    void PlayerMove()
    {
        velosity = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * velosity * speed * Time.deltaTime);
        //ïîâîðîò ñïàðéòà 
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

    //ïðûæîê
    void PlayerJump() {

        rbPlayer.velocity = new Vector2(0, 0);
        rbPlayer.velocity = Vector2.up * jumpForse;
        
    }
    //ðåàëèçàöèÿ èíòåðôåéñà
    public void Moving()
    {
        PlayerMove();
        
    }

    //îáíîâëÿåì ìàêñèìàëüíîå çíà÷åíèå äëÿ ñëàéäåðà healt bar âûçîâîì ìåòîäà
    private void Start()
    {
        SetMaxHealth(maxHealth);
    }
    private void FixedUpdate()
    {
        //åñëè ïåñíîíàæ óìåð îí íå äâèãàåòñÿ
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
                if (Input.GetKey(KeyCode.LeftShift))
                    PlayerDash();
            }
            else
            {
                timer -= Time.deltaTime;
            }

            UI.deshReload = (float)System.Math.Round(timer, 1);
            UI.playerHealth = System.Convert.ToInt32(Health);
        }
        //îáíîâëÿåì òåêóùåå õï
        SetHealth(health);
    }

}
