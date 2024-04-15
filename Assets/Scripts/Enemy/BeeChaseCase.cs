using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeChaseCase : BaseState
{
    
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
    }

   
    public override void LogicUpdate()
    {
        if (!currentEnemy.isfindplayer)
        {
            currentEnemy.SwitchState(NPCState.Patrol);
        }
        if (currentEnemy.isstartrush)
        {
            currentEnemy.SwitchState(NPCState.BossRush);
        }
        
    }

    public override void physicsUpdate()
    {
       
    }
    
    public override void OnExit()
    {
        Debug.Log("tuichuzhuiji");
    }
}
