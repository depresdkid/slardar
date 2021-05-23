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
    // ����� ��������
    public void Moving() {
             
        transform.Translate(Vector2.right * _speed * Time.deltaTime * IsLeft);
        //��������
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
    //����� ���������� � ����� ��������(��������)
    void Die() {
        gameObgectEnemy.SetActive(false);
    }
    protected override void Attack()
    {
        
    }
    void Fliping() {
        //�������������� � ����������� �� ������������ ���������
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
            //������������ � ��������
            float distance = Vector2.Distance(transform.position, player.position);
            float enemyPosY = transform.position.y;
            float playerPosY = player.position.y;
            //�������� �� ��������� � ��������� �����
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
            //�������� �� ��������� � ��������� ����
            else if (distance < _moveDistans)
            {
                //�������� �� ������
                if (playerPosY > enemyPosY + _heightAgr | playerPosY < enemyPosY - _heightAgr)
                {
                    
                    animator.SetBool("IsRunning", false);
                    isRun = false;
                }
                else
                    Moving();
            }
            //�������� �� ����� �� ���������
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
