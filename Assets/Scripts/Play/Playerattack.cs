using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Playerattack : MonoBehaviour
{
    public float damage;
    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
            other.GetComponent<Character>()?.TakeDamage(this);


    }
}
