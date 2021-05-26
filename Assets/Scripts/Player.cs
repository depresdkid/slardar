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

    //�������� ������������ ���-�� �� ��� health bar
    public void SetMaxHealth(float health)
    {
        sliderHealth.maxValue = health;
        sliderHealth.value = health;
    }

    //�������� ������� ���-�� �� ��� health bar
    public void SetHealth(float health)
    {
        sliderHealth.value = health;
    }

    //�������� �����
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
    //���� ����� ���� ������� �� ���� ��� ��� ������
    //�� ��� ������� B)
    float Health {
        get {
            return Mathf.Round(health);
        }
    }

    //�������� ����
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

    //�����
    void PlayerDash() {
        //��������� ������ ���� �� ���������� ��������
        // ������ �� �� �������� :D (��� �������� �� �� ��� ��� ����)
        rbPlayer.velocity = new Vector2(0, 0);

        //�������� ��� ���� � ������� ��������� � ��� ��������
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
    //�������� ������
    void PlayerMove()
    {
        velosity = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * velosity * speed * Time.deltaTime);
        //������� ������� 
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

    //������
    void PlayerJump() {

        rbPlayer.velocity = new Vector2(0, 0);
        rbPlayer.velocity = Vector2.up * jumpForse;
        
    }
    //���������� ����������
    public void Moving()
    {
        PlayerMove();
        
    }

    //��������� ������������ �������� ��� �������� healt bar ������� ������
    private void Start()
    {
        SetMaxHealth(maxHealth);
    }
    private void FixedUpdate()
    {
        //���� �������� ���� �� �� ���������
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
        //��������� ������� ��
        SetHealth(health);
    }

}
