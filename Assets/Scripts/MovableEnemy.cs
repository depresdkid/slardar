using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableEnemy : Enemy,IMove
{
    [SerializeField] float _speed,_moveDistans, _attackDistans, _heightAgr;
    Transform player;
    SpriteRenderer SpriteRenderer;
    int IsLeft = -1,randomChance = 1;        
    bool isRun = false, isAgr = false;
    float random;
    public override void Start()
    {
        random = Random.Range(1, 7);
        SpriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
        
    }

    // метод движени€ FF0000
    public void Moving() {
        if (random <= randomChance)
        {
            SpriteRenderer.color = new Color32(255, 0, 0, 255);
            _speed = 4;
            _moveDistans = 8;
            _damage = 5;
        }
        transform.Translate(Vector2.right * _speed  * Time.deltaTime * IsLeft);
        //анимации
        if (isRun == false)
        {

            try
            {
                try
                {
                    if (!isAgr)
                    {
                        if (random > randomChance)
                        {
                            _audioRun.Play();
                        }
                        else
                            _audioScrimer.Play();
                        isAgr = true;
                    }
                }
                catch (System.Exception)
                {
                }
                animator.SetTrigger("StartRunning");
            }
            catch (System.Exception)
            {

                throw;
            }
            animator.SetBool("IsRunning", true);
            isRun = true;
        }      
        
    }
    //ћетод вызываетс€ в конце анимации(удаление)
    void Die() {
        gameObgectEnemy.SetActive(false);
    }
    //атака
    protected override void Attack()
    {
        if (Player.player.isAlive)
        {
            _audioHit.Play();
            Player.player.GetDamage(_damage);
        }
    }    
    void Fliping() {
        //поворачиваетс€ в зависимости от расположени€ персонажа
        if (transform.position.x < player.position.x)
        {
            IsLeft = 1;
            SpriteRenderer.flipX = true;
        }
        else
        {
            SpriteRenderer.flipX = false;
            IsLeft = -1;
        }
    }
    private void FixedUpdate()
    {
        if (IsAlive && Player.player.isAlive)
        {
            Fliping();
            //передвижение и анимации
            float distance = Vector2.Distance(transform.position, player.position);
            float enemyPosY = transform.position.y;
            float playerPosY = player.position.y;
            //ѕроверка на вхождение в дистанцию атаки
            if (distance < _attackDistans )
            {
                //animator.SetTrigger("EndRunning");
                animator.SetBool("IsRunning", false);
                try
                {

                    animator.SetBool("IsAttack", true);
                }
                catch (System.Exception)
                {
                    throw;
                }
                isRun = false;

            }
            //ѕроверка на вхождение в дистанцию бега
            else if (distance < _moveDistans)
            {
                //ѕроверка на высоту
                if (playerPosY > enemyPosY + _heightAgr | playerPosY < enemyPosY - _heightAgr)
                {

                    animator.SetBool("IsRunning", false);
                    isRun = false;
                }
                else
                    Moving();
            }
            //проверка на выход за дистанцию
            else if (distance >= _attackDistans)
            {
                animator.SetBool("IsRunning", false);
                isRun = false;
            }
            if (distance >= _attackDistans)
            {
                try
                {
                    animator.SetBool("IsAttack", false);
                }
                catch (System.Exception)
                {
                }
            }

        }
        else {
            animator.SetBool("IsAttack", false);
            animator.SetBool("IsRunning", false);
        }
    }   
}
