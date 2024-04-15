using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState 
{
    protected Enemy currentEnemy;
    public abstract void OnEnter(Enemy enemy);
    public abstract void LogicUpdate();//�߼��ж�
    public abstract void physicsUpdate();//�����ж�
    public abstract void OnExit();

}
