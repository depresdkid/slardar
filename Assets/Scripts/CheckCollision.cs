using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.isReady = true;
        player.doubleJump = 1;

    }
}
