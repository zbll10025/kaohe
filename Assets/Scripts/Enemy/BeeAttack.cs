using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAttack : Attack
{
    public Animator ani;
    private void Update()
    {
        attackanimation();
    }
    public void attackanimation()
    {
        if (enemy != null && isattack&&ani!=null&&!enemy.isattackwait)
        {
            ani.SetBool("beeattack", true);
        }
        else
        {
            ani.SetBool("beeattack", false);
        }
        
    }
}
