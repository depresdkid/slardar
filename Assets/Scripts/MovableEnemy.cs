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

    // ����� �������� FF0000
    public void Moving() {
        if (random <= randomChance)
        {
            SpriteRenderer.color = new Color32(255, 0, 0, 255);
            _speed = 4;
            _moveDistans = 8;
            _damage = 5;
        }
        transform.Translate(Vector2.right * _speed  * Time.deltaTime * IsLeft);
        //��������
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
    //����� ���������� � ����� ��������(��������)
    void Die() {
        gameObgectEnemy.SetActive(false);
    }
    //�����
    protected override void Attack()
    {
        if (Player.player.isAlive)
        {
            _audioHit.Play();
            Player.player.GetDamage(_damage);
        }
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
        if (IsAlive && Player.player.isAlive)
        {
            Fliping();
            //������������ � ��������
            float distance = Vector2.Distance(transform.position, player.position);
            float enemyPosY = transform.position.y;
            float playerPosY = player.position.y;
            //�������� �� ��������� � ��������� �����
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
