using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talk : MonoBehaviour
{
    public GameObject manager;
    public Text talkText;
    public string containText;
    private bool isplayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")&& collision.GetType().ToString()== "UnityEngine.CapsuleCollider2D")
        {
            isplayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            Destroy(this.gameObject);
            isplayer = false;
            
        }
    }
    private void Update()
    {
        if (isplayer)
        {
            talkText.text = containText;
            manager.SetActive(true);
        }
    }
}
