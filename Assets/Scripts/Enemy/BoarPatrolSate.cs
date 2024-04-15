using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoarPatrolSate : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }
    public override void LogicUpdate()
    {
        if (currentEnemy.foundplayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
        }
       
        //·¢ÏÖµÐÈËÇÐ»»×·»÷
        if(currentEnemy.rb.velocity.x==0&&currentEnemy.rb.velocity.y==0){
            currentEnemy.ani.SetBool("walk", false);
        }else if (!currentEnemy.physicsCheck.isGround) {
            currentEnemy.wait = true;
            currentEnemy.ani.SetBool("walk", false);

        }
        else if ((currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDir.x > 0) || (!currentEnemy.physicsCheck.risground && currentEnemy.faceDir.x > 0) || !currentEnemy.physicsCheck.lisground&&currentEnemy.faceDir.x<0)
        {
            currentEnemy.wait = true;
            currentEnemy.ani.SetBool("walk", false);
            
        }
        else
        {
                currentEnemy.ani.SetBool("walk", true);
        }
    }
   
    public override void physicsUpdate()
    {
       
    }
    public override void OnExit()
    {
        currentEnemy.ani.SetBool("walk", false);
        //Debug.Log("exit");
    }


}
