using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    Transform playerTransform;
    BoxCollider2D boxCollider2D;
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }    
    void FixedUpdate()
    {
        float posHero = playerTransform.position.y + 0.2f;
        float posBlock = transform.position.y + transform.localScale.y;
        if (posHero > posBlock)
        {
            boxCollider2D.enabled = true;
        }
        else
            boxCollider2D.enabled = false;
    }
}
