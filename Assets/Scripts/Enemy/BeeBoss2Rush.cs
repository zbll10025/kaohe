using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeeBoss2Rush : BaseState
{
    public float wait;
    public float waitStrat;
    public bool iswait;
    public Transform player;
    public Vector3 nextposition;
    public bool isDashingTime;
    public bool isdashing;
    public static int cnt;
    public Vector3 rUP;
    public Vector3 rDOWN;
    public Vector3 lup;
    public Vector3 ldown;
    public Vector3[] Arr;
    public override void OnEnter(Enemy enemy)
    {
        player = GameObject.Find("player").transform;
        Arr = new Vector3[4];
          cnt = 0;
        Setoffest();
        //
          waitStrat = 1f;
          wait = waitStrat;
        //
          currentEnemy = enemy;
          currentEnemy.currentSpeed = currentEnemy.boosSpeed;
          currentEnemy.rushtime = currentEnemy.rushStarttime;
          nextposition = Arr[0];
         // Dashing();
    }
    public override void LogicUpdate()
    {
     
          TimeDashing();
         
          Wait();
        if (!iswait)
        {
           Move();
        }
    }

    public void Move()
    {
       
      currentEnemy.enemytransform.position = Vector2.MoveTowards(currentEnemy.enemytransform.position, player.position+nextposition, currentEnemy.currentSpeed * Time.deltaTime);
       
        if(currentEnemy.enemytransform.position == player.position + nextposition)
        {
            nextposition = Arr[cnt];
            cnt++;
            if (cnt == 4)
            {
                cnt = 0;
            }
            wait = waitStrat;
         
        }
       
    }
   
   public void Setoffest()
    {
        rUP =new Vector3(10,5,0);
        Arr[0] = rUP;
        rDOWN =new Vector3(10,-5,0);
        Arr[1] = rDOWN;
        lup =new Vector3(-10,5,0);
        Arr[2]=lup;
        ldown =new Vector3(-10,-5,0);
        Arr[3] =ldown;

    }
   public  void Wait()
    {
        if (wait >= 0)
        {
            wait-=Time.deltaTime;
            iswait = true;
        }
        else
        {
           
            iswait = false;
        }
    }
    public override void physicsUpdate()
    {

       
    }
    
    public override void OnExit()
    {


    }
    
    /// <summary>
    /// 冲刺
    /// </summary>
    public void Dashing()
    {
        if (isdashing)
        {
            currentEnemy.rb.velocity = currentEnemy.rushdir * currentEnemy.rushForce;
        }
    }

    /// <summary>
    /// 冲刺时间计算时间
    /// </summary>
    public void TimeDashing()
    {
        if (currentEnemy.rushtime >= 0) {
            currentEnemy.rushtime -=Time.deltaTime;
            isdashing = true;
        }
        else
        {
            if (isDashingTime)
            {   
                currentEnemy.rushtime = currentEnemy.rushStarttime; 
            }
            isdashing = false;
        }


    }

}

