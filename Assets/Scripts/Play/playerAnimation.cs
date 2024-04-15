using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    private Animator ani;
    private Rigidbody2D rb;
    private PhysicsCheck PhysicsCheck;
    private PlyerController PlyerController;
    
    private void Awake()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        PhysicsCheck = rb.GetComponent<PhysicsCheck>();
        PlyerController = rb.GetComponent<PlyerController>();

    }
    private void Update()
    {
        Setanimatiom();
    }
   public void Setanimatiom()
    {
        ani.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        ani.SetFloat("velocityY", rb.velocity.y);
        ani.SetBool("isgrand", PhysicsCheck.isGround);
        ani.SetBool("isdead", PlyerController.isdead);
        ani.SetBool("isattack", PlyerController.isattack);
        ani.SetBool("isjump", PlyerController.isjump);
        
    }
    public void PlayerHurt()
    {
        ani.SetTrigger("hurt");
    }
    public void PlyerAttack()
    {
        ani.SetTrigger("attack");
    }
}
