using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    [Header("bee")]
    public Transform beeTransform;
    public Vector2 rndpos;
    [Header("活动范围")]
    public Transform movePos;
    public Transform lefttDowmpos;
    public Transform righttUpmpos;
    [Header("飞行等待时间")]
    public float flywait;
    public float startWaittime;
    [Header("寻找敌人范围")]
    public float radius;
    public Transform playerTransform;
    public float playerfx;
    public Enemy anotherboss;
    protected override void Awake()
    {
        base.Awake();
        patrolState = new BeePatrolSate();
        chaseState =new BeeChaseCase();
        //
        flywait = startWaittime;
        movePos.position = GetRandownPos();
        //
       playerTransform= GameObject.Find("player").transform;

    }


    protected override void Update()
    {
        base.Update();
        enemydir();
        CheckisRush();
        AttackWaitCount();
        IsMoveFlyWait();
        BeeFindPlayer();
        if (playerTransform == null)
        {
            Debug.Log("已找到");
            playerTransform = GameObject.Find("player").transform;


        }

    }
    protected override void OnDie()
    {
        Destroy(this.gameObject);
    }
    public override void Move()
    {
        if (!isattackwait||!isstartrush)
        {
            if (!isfindplayer)
            {
                beeTransform.position = Vector2.MoveTowards(beeTransform.position, movePos.position, currentSpeed * Time.deltaTime);
            }
            else
            {
                beeTransform.position = Vector2.MoveTowards(beeTransform.position, playerTransform.position, currentSpeed * Time.deltaTime);
            }
        }
    }
    public void IsMoveFlyWait()
    {
        if (Vector2.Distance(beeTransform.position, movePos.position) < 0.1f)
        {
            if (flywait <= 0)
            {
                movePos.position = GetRandownPos();
                flywait = startWaittime;
            }
            else
            {
                flywait-=Time.deltaTime;
            }
        }

    }
    
    public Vector2 GetRandownPos()
    {
        rndpos = new Vector2(UnityEngine.Random.Range(lefttDowmpos.position.x,righttUpmpos.position.x), UnityEngine.Random.Range(lefttDowmpos.position.y, righttUpmpos.position.y));
        return rndpos;
    }
    public void BeeFindPlayer()
    {
        if (playerTransform != null)
        {
            float distance = (beeTransform.position - playerTransform.position).magnitude;
            if (!isrush) { rushdir = (playerTransform.position - beeTransform.position).normalized; }
           
            if (distance < radius)
            {
                isfindplayer = true;
            }
            else
            {
                isfindplayer= false;
            }
        }
    }
     public void CheckisRush()
    {   
       float prenstage = enemycharacter.currentHealth/enemycharacter.maxHealth;
        if (anotherboss != null) {

            if (prenstage <=0.5||anotherboss.isstartrush)
        {
           isstartrush = true;
        }
        
        }
       
    }

    public void enemydir()
    {
        playerfx = beeTransform.position.x - playerTransform.position.x;
        if (playerfx < 0 && beeTransform.localScale.x > 0)
        {
            beeTransform.localScale = new Vector3(-2.5f, 2.5f, 0);
        }
        else if (playerfx > 0 && beeTransform.localScale.x < 0)
        {
            beeTransform.localScale = new Vector3(2.5f, 2.5f, 0);
        }

    }

}

   
    

    


