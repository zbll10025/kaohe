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
    [Header("��������")]
    public float normalSpeed;
    public float chaseSpeed;
    public float boosSpeed;
    [HideInInspector] public float currentSpeed;
    public Vector3 faceDir;
    public float hurtForce;
    public Transform attacker;
    [Header("��ʱ��")]
    public float waitTime;//ײǽ�Ⱥ�ʱ��
    public float waitCounter;
    public bool wait;
    public float lostTime;
    public float lostTimeCounter;
    [Header("״̬")]
    public bool isHurt;
    public bool isdead;
    public bool isstartrush;
    [Header("���е��˵�Ѱ�ҵ���")]
    public bool isfindplayer;
    private   BaseState  currentState;
    protected BaseState patrolState;//Ѳ��״̬
    protected BaseState chaseState;//׷��״̬
    public BaseState bossRush;
    [Header("���")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;
    public Vector2 lunchDir;
    [Header("������ȴ�ʱ��")]
    public float attackWait;
    public float attackStartWait;
    public bool isattackwait;
    [Header("Boss���")]
    public Vector2 rushdir;
    public float rushForce;
    public float rushtime;
    public float rushStarttime;
    public bool isrush;
    [Header("���")]
    public GameObject servent1;
    public GameObject servent2;
    [Header("����")]
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
        faceDir = new Vector3(-transform.localScale.x, 0, 0);//ʱʱ����泯����
        enenmydir =new Vector3(-transform.localScale.x, 0, 0);
        currentState.LogicUpdate();
        TimeCounter();
    
    }
    /// <summary>
    /// ����״̬��
    /// </summary>
    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);//һ������ִ��currentstate���������onenter����
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
    /// ײǽ�ȴ�ʱ�䣨��ʱ����
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
    #region �¼�ִ�з���
    /// <summary>
    /// �ܵ����������Ϊ
    /// </summary>
    /// <param name="attackTrans"></param>
    public void onTakeDamage(Transform attackTrans)
    {   
        attacker= attackTrans;
        //ת��
        if(attacker.position.x-transform.position.x>0)
        { 
              transform.localScale = new Vector3(-1, 1, 1);
        }
        if (attacker.position.x-transform .position.x<0) 
        { 
                transform.localScale = new Vector3(1, 1, 1);
        }
        //���˻���
        isHurt = true;
        ani.SetTrigger("hurt");
        Vector2 dir = new Vector2((transform.position.x-attacker.position.x),0).normalized;
        rb.velocity = new Vector3(0, rb.velocity.y);
       StartCoroutine(Onhurt(dir));//Я�̵�����
    }
    public void BeeonTakeDamage(Transform attackTrans)
    {
        attacker = attackTrans;
        //���˻���
        isHurt = true;
        ani.SetTrigger("ishurt");
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;
        rb.velocity = new Vector3(0, rb.velocity.y);
        StartCoroutine(Onhurt(dir));//Я�̵�����
    }


    private IEnumerator Onhurt(Vector2 dir)
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        isHurt= false;
    }//Я��
    /// <summary>
    /// ����
    /// </summary>
    protected virtual void OnDie()
    {
        ani.SetBool("dead", true);
        isdead = true;
    }
    public void DestroyAfterAnimation()
    {
        Destroy(this.gameObject);

    }//������Ŀ

    /// <summary>
    /// �������
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
            NPCState.Patrol => patrolState,//=>:�����NPCState.Patrol�򷵻�patrolState
            NPCState.Chase => chaseState,
            NPCState.BossRush => bossRush,
            _ => null//�൱��Switch�е�defalt
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
