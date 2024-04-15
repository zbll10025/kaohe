using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBossRush : BaseState
{
    public float wait;
    public float waitStrat;

    public override void OnEnter(Enemy enemy)
    {
        waitStrat = 1f;
        wait = waitStrat;
       currentEnemy = enemy;
       currentEnemy.rushtime = currentEnemy.rushStarttime;
        currentEnemy.currentSpeed = currentEnemy.boosSpeed;
       //currentEnemy. servent1.SetActive(true);
       // currentEnemy.servent2.SetActive(true);

    }

   
   
    public override void LogicUpdate()
    {
     
        if (currentEnemy.rushtime >= 0)
        {
            currentEnemy.isrush = true;
            currentEnemy.rushtime-=Time.deltaTime;
        }
        else
        {
     
            if (wait >= 0)
            {
                wait-=Time.deltaTime;
                currentEnemy.isrush=false;
            }
            else
            {
                wait = waitStrat;
                Againrush();
            }
        }

         if (currentEnemy.isrush)
        {
            currentEnemy.rb.velocity = currentEnemy.rushdir * currentEnemy.rushForce;
        }

    }
    public override void physicsUpdate()
    {
       
       
    } 
    public override void OnExit()
    {
        
    }
    public void Againrush()
    {
        currentEnemy.rushtime = currentEnemy.rushStarttime;
    }

  
}
