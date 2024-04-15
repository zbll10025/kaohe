using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;
    [Header("检测值")]
    public bool manual;
    public Vector2 bootom;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public Vector2 waterOffset;
    public Vector2 platformOffset;
    public Vector2 rbootom;
    public Vector2 lbootom;
    [Header("状态")]
    public bool isGround;
    public float checkRaduls;
    public LayerMask groundLayer;
    public LayerMask WaterLayer;
    public LayerMask PlatformLayer;
    public bool touchLeftWall;
    public bool touchRightWall;
    public bool iswater;
    public bool isPlatform;
    public bool risground;
    public bool lisground;
    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
        if (!manual)
        {
            rightOffset = new Vector2((coll.bounds.size.x + coll.offset.x) / 2, coll.bounds.size.y / 2);
            leftOffset = new Vector2(-rightOffset.x, rightOffset.y);
        }
    }
    public void Update()
    {
        check();
    }
    public void FixedUpdate()
    {

    }

    public void check()
    {  //地面检测
        isGround  = Physics2D.OverlapCircle((Vector2)transform.position + bootom, checkRaduls, groundLayer);
        risground = Physics2D.OverlapCircle((Vector2)transform.position + rbootom, checkRaduls, groundLayer);
        lisground = Physics2D.OverlapCircle((Vector2)transform.position + lbootom, checkRaduls, groundLayer);
        //墙体检测
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduls, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduls, groundLayer);
        iswater = Physics2D.OverlapCircle((Vector2)transform.position + waterOffset, checkRaduls, WaterLayer);
        isPlatform= Physics2D.OverlapCircle((Vector2)transform.position + platformOffset, checkRaduls, PlatformLayer);
        Adjust();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bootom, checkRaduls);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduls);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduls);
        Gizmos.DrawWireSphere((Vector2)transform.position + waterOffset, checkRaduls);
        Gizmos.DrawWireSphere((Vector2)transform.position + lbootom, checkRaduls);
        Gizmos.DrawWireSphere((Vector2)transform.position + rbootom, checkRaduls);
    }
    private void Adjust()
    {
        if(isPlatform)
        {
            isGround = true;
        }
        
    }
}