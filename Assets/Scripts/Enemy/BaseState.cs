using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState 
{
    protected Enemy currentEnemy;
    public abstract void OnEnter(Enemy enemy);
    public abstract void LogicUpdate();//¬ﬂº≠≈–∂œ
    public abstract void physicsUpdate();//ŒÔ¿Ì≈–∂œ
    public abstract void OnExit();

}
