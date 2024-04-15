using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBoss : Bee
{
   
    protected override void Awake()
    {
        base.Awake();
        bossRush = new BeeBossRush();
    }
    protected override void Update()
    {
        base.Update();

    }
}
   
          
      