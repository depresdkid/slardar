using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helper : MonoBehaviour
{
    Text textUI;
    bool isTrigger;
    private void Start()
    {        
        textUI = GameObject.FindGameObjectWithTag("Help").GetComponent<Text>();
        isTrigger = false;
    }
    public string text;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isTrigger == false);
        {
            isTrigger = true;
            textUI.text = text;            
            Invoke("RemoveText", 3f);
            
        }
    }
    void RemoveText() {
        textUI.text = "";
    }    
}
