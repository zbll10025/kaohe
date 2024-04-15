using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    public float attackRange;
    public float attackRate;
    public Enemy enemy;
    public bool isattack;
    private void OnTriggerStay2D(Collider2D other)
    {     
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
             other.GetComponent<Character>()?.TakeDamage(this);
            Invoke("Attackafter", 0.2f);
            isattack = true;
        }
       
        
    }

    public void Attackafter()
    {
        if (enemy != null)
        {
            enemy.attackWait = enemy.attackStartWait;
            isattack = false;
            enemy.isattackwait = true;
        }
    }
}

