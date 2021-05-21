using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IMove
{
    [SerializeField] private float speed;
    [SerializeField] private float lives;
    [SerializeField] private float jumpForse;
    [SerializeField] private float dashForse = 3000f;
    [SerializeField] private Vector2 moveVector;
    private Rigidbody2D rbPlayer;
    private SpriteRenderer spritePlayer;
    private bool isReady = true;
    private int doubleJump = 1;
    private bool isLeft = false;
    private float velosity;
    private void OnCollisionEnter2D(Collision2D collision)
    {

        isReady = true;
        doubleJump = 1;

    }
    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        spritePlayer = GetComponent<SpriteRenderer>();


    }
    void playerDash() {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rbPlayer.velocity = new Vector2(0, 0);

            if (velosity == 0)
            {
                if (isLeft)
                {

                    rbPlayer.AddForce(Vector2.left * dashForse, ForceMode2D.Force);
                }
                else
                {

                    rbPlayer.AddForce(Vector2.right * dashForse, ForceMode2D.Force);
                }
            }
            else
            {
                if (isLeft)
                {

                    rbPlayer.AddForce(Vector2.left * dashForse / 2, ForceMode2D.Force);
                }
                else
                {

                    rbPlayer.AddForce(Vector2.right * dashForse / 2, ForceMode2D.Force);
                }
            }
        }


    }

    void playerMove()
    {
        velosity = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * velosity * speed * Time.deltaTime);

        if (isLeft)
        {
            spritePlayer.flipX = true;
        }
        else {
            spritePlayer.flipX = false;
        }
        if (velosity > 0)
        {
            isLeft = false;

        }
        else if (velosity < 0)
        {
            isLeft = true;

        }


    }
    void playerJump() {

        rbPlayer.AddForce(transform.up * jumpForse, ForceMode2D.Impulse);
        
    }
    public void Moving()
    {
        playerMove();

    }   
    
    private void FixedUpdate()
    {

        Moving();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isReady)
        {
            playerJump();
            doubleJump++;
            if (doubleJump < 3)
            {
                isReady = true;
            }
            else {
                isReady = false;
            }
        }
        playerDash();

    }
   
}
