using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableEnemy : Enemy,IMove
{
    [SerializeField] float _speed,_moveDistans, _attackDistans, _heightAgr;    
    Transform player;
    int IsLeft = -1;    
    SpriteRenderer SpriteRenderer;
    bool isRun = false;
    public override void Start()
    {        
        SpriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
    }
    // метод движени€
    public void Moving() {
             
        transform.Translate(Vector2.right * _speed * Time.deltaTime * IsLeft);
        //анимации
        if (isRun == false)
        {
            try
            {
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
    protected override void Attack()
    {
        
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

        if (IsAlive)
        {
            Fliping();
            //передвижение и анимации
            float distance = Vector2.Distance(transform.position, player.position);
            float enemyPosY = transform.position.y;
            float playerPosY = player.position.y;
            //ѕроверка на вхождение в дистанцию атаки
            if (distance < _attackDistans)
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
                Attack();
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
            if (distance >= _attackDistans) {
                try
                {
                    animator.SetBool("IsAttack", false);
                }
                catch (System.Exception)
                {
                }
            }

        }
    }   
}
