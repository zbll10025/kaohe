using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ci : MonoBehaviour
{

    public float speed;
    public int damage;
    public float destroyDistance;
    public Rigidbody2D rb;
    public Vector3 startPos;
    public Vector3 dir;
    public Transform player;
    private void Start()
    {
        player = GameObject.Find("player").transform;
        if (player == null)
        {
            Destroy(this.gameObject);
        }
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        dir = (player.position - transform.position).normalized;
        rb.velocity = dir * speed;
    }
   
      
       
   
    private void Update()
    {
        //rb.velocity = dir * speed;
        float distance = (transform.position - startPos).sqrMagnitude;
        if (distance > destroyDistance)
        {
            Destroy(gameObject);
            
        }
    }
}
