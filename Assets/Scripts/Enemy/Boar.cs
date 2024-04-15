using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy//继承敌人这个类的行为
{
    protected override void Awake()
    {
        base.Awake();
        patrolState = new BoarPatrolSate();
        chaseState  =  new BoarChaseState();
    }
}
