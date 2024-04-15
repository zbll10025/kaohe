using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class WallMovement : MonoBehaviour
{
    public  PhysicsCheck physicsCheck;
    public bool isWallMove;
    public Rigidbody2D rb;
    public PlyerController playerController;
   
    public enum Wallstste
    {   wallGrab,
        WallSlide,
        WallClimb,
        Walljump,
        None,
    }
    public Wallstste ws;
    private void Awake()
      {
        ws = Wallstste.None;
        rb = GetComponent<Rigidbody2D>();
      }
    private void Update()
    {
       // float playerInput = Input.GetAxis("Vertical");
        iswallCheck();
       
        
      // if (isWallMove)
       // {
        //    if (Input.GetButtonDown("jump"))
        //    {
        //        jump(Vector3.up);
         //   }else
         //   if (playerInput > 0)
          //  {
          //      WallClimb();
          //  }
           // else if (playerInput < 0)
           // {
           //     WallSlide();
           // }
           // else
           // {
                //WallGrab();
             //   rb.velocity = new Vector2(rb.velocity.x, -1f);
           // }
       // }
        //else
       // {
        //    ws=Wallstste.None;
        //    rb.gravityScale = 3f;
       // }
        
    }
    
    private void iswallCheck()
    {
        if (((physicsCheck.touchLeftWall) || (physicsCheck.touchRightWall)))
        {
            isWallMove = true;
        }
        else
        {
            isWallMove=false;
        }

    }
    private  void WallGrab()
    {
        rb.gravityScale = 0f;
        rb.velocity = Vector3.zero;
        ws= Wallstste.wallGrab;
    }
    private void WallClimb()
    {
        rb.velocity = Vector2.zero;
        rb.velocity=new Vector2(rb.velocity.x,8);
        ws = Wallstste.WallClimb;
    }
    private void WallSlide()
    {
        rb.velocity = Vector2.zero;
        rb.velocity = new Vector2(rb.velocity.x, -7.5f);
        ws = Wallstste.WallSlide;
    }
    void jump(Vector2 dir)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(dir * 10, ForceMode2D.Impulse);
    }
}
