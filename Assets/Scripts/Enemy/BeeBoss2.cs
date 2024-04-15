using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BeeBoss2 : Enemy
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
    public Vector3 offeset;
    [Header("子弹")]
    public GameObject ci;
    public float ciwait;
    public float ciwaitStrat;
    public bool isci;
    [Header("左右移动")]
    public Vector3 []PO;
    public Vector3 p1;
    public Vector3 p2;
    public static int cnt;
    public float movewait;
    public float movewaitStrat;
    public float playerfx;
    public Enemy anotherboos;
    protected override void Awake()
    {
        movewaitStrat = 1.5f;
        movewait = movewaitStrat;
        //
        cnt = 0;
        p1 = new Vector3(8,5, 0);
        p2 = new Vector3(-8,5, 0);
        PO = new Vector3 [2];
        PO[0] = p1;
        PO[1] = p2;
        offeset = offeset + PO[0];
        //
        base.Awake();
        bossRush = new BeeBoss2Rush();
        patrolState = new BeePatrolSate();
        chaseState = new BeeChaseCase();
        //
        flywait = startWaittime;
        movePos.position = GetRandownPos();
        //
        playerTransform = GameObject.Find("player").transform;
        //
        ciwaitStrat = 1f;
        ciwait = ciwaitStrat;
        
    }


    protected override void Update()
    {
        
        base.Update();
        enemydir();
        CheckisRush();
        AttackWaitCount();
        IsMoveFlyWait();
        BeeFindPlayer();
        if (ciwait >= 0)
        {
            ciwait-=Time.deltaTime;
        }
        else
        {
            BCi();
        }
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
        if (!isattackwait && !isstartrush)
        {
            if (!isfindplayer)
            {
                beeTransform.position = Vector2.MoveTowards(beeTransform.position, movePos.position, currentSpeed * Time.deltaTime);
            }
            else
            {
                beeTransform.position = Vector2.MoveTowards(beeTransform.position, playerTransform.position+offeset, currentSpeed * Time.deltaTime);
                if (movewait >= 0)
                {
                    movewait -= Time.deltaTime;
                }
                else
                {
                    offeset = PO[cnt];
                    cnt++;
                    if (cnt == 2)
                    {
                        cnt = 0;
                    }
                    movewait = movewaitStrat;
                }
                
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
                flywait -= Time.deltaTime;
            }
        }

    }

    public Vector2 GetRandownPos()
    {
        rndpos = new Vector2(UnityEngine.Random.Range(lefttDowmpos.position.x, righttUpmpos.position.x), UnityEngine.Random.Range(lefttDowmpos.position.y, righttUpmpos.position.y));
        return rndpos;
    }
    public void BeeFindPlayer()
    {
        if (playerTransform != null)
        {

            float distance = (beeTransform.position - playerTransform.position).magnitude;
            rushdir = (playerTransform.position - beeTransform.position).normalized;

            if (distance < radius)
            {
                isfindplayer = true;
            }
            else
            {
                isfindplayer = false;
            }
        }
    }
    public void CheckisRush()
    {
        float prenstage = enemycharacter.currentHealth / enemycharacter.maxHealth;
        if (prenstage <= 0.5||anotherboos.isstartrush)
        {
            isstartrush = true;
        }
    }
    public void BCi()
    {
        float angel = Mathf.Atan2(rushdir.y, rushdir.x) * Mathf.Rad2Deg;
        Vector3 eulerAngles = new Vector3(0, 0,angel);
        Instantiate(ci, enemytransform.position, Quaternion.Euler(eulerAngles));
        ciwait = ciwaitStrat;
    }
    public void BeeOnDestroy()
    {
        Destroy(this.gameObject);
    }
    public void enemydir()
    {
        playerfx = beeTransform.position.x-playerTransform.position.x;
        if(playerfx < 0&&beeTransform.localScale.x>0){
            beeTransform.localScale = new Vector3(-2.5f,2.5f,0);
        }
        else if(playerfx>0&&beeTransform.localScale.x<0) {
           beeTransform.localScale = new Vector3(2.5f, 2.5f, 0);
        }
       
    }

}
