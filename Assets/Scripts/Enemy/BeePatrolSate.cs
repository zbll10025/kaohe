using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeePatrolSate : BaseState
{
   

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }

    
    public override void LogicUpdate()
    {
        if (currentEnemy.isfindplayer)
        {
            currentEnemy.SwitchState(NPCState.Chase);
        }
        
    }

    public override void physicsUpdate()
    {
       
    }
    public override void OnExit()
    {
        Debug.Log("tuichuxunluo");
    } 
}
