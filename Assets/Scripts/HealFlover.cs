using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealFlover : MonoBehaviour, IArounds
{
    [SerializeField] float heal;
    GameObject _gameObject;
    Animator animator;
    bool isHeal = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        _gameObject = gameObject;
    }
    public void AroundsActions()
    {
        if (Player.player.Health >= 17)
        {
            Player.player.Health = 20;

        }
        else {
            Player.player.Health += heal;
        }
    }
    public void End() {
        _gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isHeal)
        {
            
            Invoke("End",1f);
            animator.SetTrigger("IsHeal");
            isHeal = true;
            AroundsActions();    
        }
    }

}
