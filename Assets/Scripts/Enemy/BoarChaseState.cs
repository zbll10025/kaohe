using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.ani.SetBool("run", true);
    }
    public override void LogicUpdate()
    {
        if (currentEnemy.lostTimeCounter<=0)
        {
            currentEnemy.SwitchState(NPCState.Patrol);
        }
        if ((!currentEnemy.physicsCheck.risground && currentEnemy.faceDir.x > 0) || (!currentEnemy.physicsCheck.lisground && currentEnemy.faceDir.x < 0) || ((currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDir.x > 0)))//һ��ײ��ǽ����ǽ�ı��泯����
        {
            currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDir.x, 1, 1);
        }
    }
    public override void physicsUpdate()
    {
       
    }
    public override void OnExit()
    {
        currentEnemy.ani.SetBool("run", false);
        currentEnemy.lostTimeCounter = currentEnemy.lostTime;
    }

}
