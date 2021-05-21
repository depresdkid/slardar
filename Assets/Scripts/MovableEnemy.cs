using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableEnemy : Enemy,IMove
{
    [SerializeField] float _speed,_moveDistans, _attackDistans;
    Transform player;
    int IsLeft = -1;
    SpriteRenderer SpriteRenderer;
    public override void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
    }
    public void Moving() {

        if (transform.position.x < player.position.x)
        {
            IsLeft = 1;
            SpriteRenderer.flipX = true;
        }

        else {
            SpriteRenderer.flipX = false;
            IsLeft = -1;
        }
        transform.Translate(Vector2.right * _speed * Time.deltaTime * IsLeft);
    }
    protected override void Attack()
    {
        print("Атака");
    }
    private void FixedUpdate()
    {        
        float distance = Vector2.Distance(transform.position, player.position);
        if (Mathf.Abs(transform.position.x)  > Mathf.Abs(player.position.x + 0.1f))
        {
            if (distance < _attackDistans)
            {
                Attack();
            }
            else if (distance < _moveDistans)
            {
                Moving();
            }
        }      
    }

    
}
