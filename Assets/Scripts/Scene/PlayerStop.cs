using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStop : MonoBehaviour
{

    public Rigidbody2D playerrigidbody;
    Vector3 velocity;
    void Start()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag("Player");
        if(targetObject != null)
        {
            playerrigidbody = targetObject.GetComponent<Rigidbody2D>();
        }
        velocity = Vector2.zero;
    }

    
    void Update()
    {
        if (playerrigidbody != null)
        {
            playerrigidbody.velocity = velocity;
        }
      
    }
}
