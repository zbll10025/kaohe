using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public Character enemycharacter;
     [HideInInspector] public Animator ani;
     [HideInInspector] public PhysicsCheck physicsCheck;
    public Transform enemytransform;
    [Header("基本参数")]
    public float normalSpeed;
    public float chaseSpeed;
    public float boosSpeed;
    [HideInInspector] public float currentSpeed;
    public Vector3 faceDir;
    public float hurtForce;
    public Transform attacker;
    [Header("计时器")]
    public float waitTime;//撞墙等候时间
    public float waitCounter;
    public bool wait;
    public float lostTime;
    public float lostTimeCounter;
    [Header("状态")]
    public bool isHurt;
    public bool isdead;
    public bool isstartrush;
    [Header("飞行敌人的寻找敌人")]
    public bool isfindplayer;
    private   BaseState  currentState;
    protected BaseState patrolState;//巡逻状态
    protected BaseState chaseState;//追击状态
    public BaseState bossRush;
    [Header("检测")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;
    public Vector2 lunchDir;
    [Header("攻击后等待时间")]
    public float attackWait;
    public float attackStartWait;
    public bool isattackwait;
    [Header("Boss冲刺")]
    public Vector2 rushdir;
    public float rushForce;
    public float rushtime;
    public float rushStarttime;
    public bool isrush;
    [Header("随从")]
    public GameObject servent1;
    public GameObject servent2;
    [Header("面向")]
    public Vector3 enenmydir;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        currentSpeed = normalSpeed;
        physicsCheck = GetComponent<PhysicsCheck>();
        waitCounter = waitTime;
       
    }
    protected virtual void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);//时时获得面朝方向
        enenmydir =new Vector3(-transform.localScale.x, 0, 0);
        currentState.LogicUpdate();
        TimeCounter();
    
    }
    /// <summary>
    /// 激活状态基
    /// </summary>
    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);//一启动就执行currentstate这个基类中onenter函数
    }
    private void FixedUpdate()
    {   
        if(!isHurt&&!isdead&&!wait)
        {
            Move();
        }
        currentState.physicsUpdate();
    }
    public void OnDisable()
    {
        currentState.OnExit();
    }
    public virtual void Move()
    {
        rb.velocity=new Vector2(currentSpeed*Time.deltaTime*faceDir.x,rb.velocity.y);
    }
    /// <summary>
    /// 撞墙等待时间（计时器）
    /// </summary>
    public void TimeCounter()
    {
            if(wait){
            waitCounter -= Time.deltaTime;
            if(waitCounter <= 0)
            {
                wait = false;
                waitCounter = waitTime;
                transform.localScale = new Vector3(faceDir.x, 1, 1);
                return;
            }
        }
        if (!foundplayer()&&lostTimeCounter>0)
        {
            lostTimeCounter -= Time.deltaTime;

        }
    }
    #region 事件执行方法
    /// <summary>
    /// 受到攻击后的行为
    /// </summary>
    /// <param name="attackTrans"></param>
    public void onTakeDamage(Transform attackTrans)
    {   
        attacker= attackTrans;
        //转身
        if(attacker.position.x-transform.position.x>0)
        { 
              transform.localScale = new Vector3(-1, 1, 1);
        }
        if (attacker.position.x-transform .position.x<0) 
        { 
                transform.localScale = new Vector3(1, 1, 1);
        }
        //受伤击退
        isHurt = true;
        ani.SetTrigger("hurt");
        Vector2 dir = new Vector2((transform.position.x-attacker.position.x),0).normalized;
        rb.velocity = new Vector3(0, rb.velocity.y);
       StartCoroutine(Onhurt(dir));//携程的启动
    }
    public void BeeonTakeDamage(Transform attackTrans)
    {
        attacker = attackTrans;
        //受伤击退
        isHurt = true;
        ani.SetTrigger("ishurt");
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;
        rb.velocity = new Vector3(0, rb.velocity.y);
        StartCoroutine(Onhurt(dir));//携程的启动
    }


    private IEnumerator Onhurt(Vector2 dir)
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        isHurt= false;
    }//携程
    /// <summary>
    /// 死亡
    /// </summary>
    protected virtual void OnDie()
    {
        ani.SetBool("dead", true);
        isdead = true;
    }
    public void DestroyAfterAnimation()
    {
        Destroy(this.gameObject);

    }//销毁项目

    /// <summary>
    /// 发现玩家
    /// </summary>
    /// <returns></returns>
    public bool foundplayer()
    {
         return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, faceDir, checkDistance, attackLayer);
     
    }
   public void SwitchState(NPCState state)
    {
        var newstate = state switch
        {
            NPCState.Patrol => patrolState,//=>:如果是NPCState.Patrol则返回patrolState
            NPCState.Chase => chaseState,
            NPCState.BossRush => bossRush,
            _ => null//相当于Switch中的defalt
        } ;
        currentState.OnExit();
        currentState = newstate;
        currentState.OnEnter(this);
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset+new Vector3(checkDistance*-transform.localScale.x,0), 0.2f);
    }

    public void AttackWaitCount()
    {
        if (attackWait<=0)
        {
            isattackwait = false;
        }
        else
        {
            attackWait-=Time.deltaTime;
        }
       
    }
   
}
