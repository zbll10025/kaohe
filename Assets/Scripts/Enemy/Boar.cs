using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy//�̳е�����������Ϊ
{
    protected override void Awake()
    {
        base.Awake();
        patrolState = new BoarPatrolSate();
        chaseState  =  new BoarChaseState();
    }
}
