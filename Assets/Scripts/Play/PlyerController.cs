using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class PlyerController : MonoBehaviour
{
    public PlayerInputControl inputControl;//(变量）调用unitysystem控制输入有关的代码
    public Vector2 inputDirection;//读取X，Y
    public Rigidbody2D rb;
    public CapsuleCollider2D capsuleCollider;
    public PhysicsCheck physicsCheck;
    private playerAnimation playerAnimation;
    public Animator ani;
    public UnityEvent<PlyerController> OnpowerChange;
    public Character playerCharacter;
    public BoxCollider2D boxCollider2D;
    public WallMovement wallMovement;
    public GameObject deadUI;

    [Header("基本参数")]
    public float speed;
    public float jumpForce;
    public float hurtForce;
    public float hurtWaterForce;
    public float rushForce;
    public float starDashing;
    public float dashTime;
    public Vector2 dir;
    public  float skillPower;
    public float currentskillPower;
    public float dashPower;
    public float getPower;
    [Header("物理材质")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;
    [Header("属性")]
    public bool isHurt;
    public bool isdead;
    public bool isattack;
    public bool isDashing;
    public bool isjump;
    public bool canjump;
    public int countjump=0;
    public bool canSkill;
    public Item liquid;
    [Header("用药时间")]
    public float usewait;
    public float usewaitstrat;
    public bool isuse;
    #region 初始操作
    private void Awake()//在strat更早执行
    {
        boxCollider2D =GetComponent<BoxCollider2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        rb = GetComponent<Rigidbody2D>();
        inputControl = new PlayerInputControl();//启用变量将其实例化
         playerAnimation =GetComponent<playerAnimation>();
        capsuleCollider=GetComponent<CapsuleCollider2D>();
        wallMovement=GetComponent<WallMovement>();
        ani = GetComponent<Animator>();
        currentskillPower=skillPower; 
        OnpowerChange?.Invoke(this);inputControl.Player.Jump.started += jump;//（事件注册函数）当按下按键时执行jump函数
        inputControl.Player.Attack.started += PlyerAttack;
        usewaitstrat = 20f;
        usewait = -1f;
    }

    

    private void OnEnable()//（周期函数）当前物体被启用时inputControl也被启用
    {
        inputControl.Enable();
    }
    private void OnDisable()//(周期函数）当前物体被关闭时inputControl也被关闭
    {
        inputControl.Disable();
    }
    #endregion
    private void Update()
    {
         inputDirection = inputControl.Player.Move.ReadValue<Vector2>();//读取设置好的vector2的值
          CheckState();
          Dashing();
        checkHealth();
        
    }


    private void FixedUpdate()//固定更新值
    {
        if (!isHurt&&!isattack&&!isDashing)
        {
            Move();
        }
    }
    public void Move()
    {
        #region 根据按键来移动
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime,rb.velocity.y);//时间修正
        #endregion

        #region 根据移动来使人物翻转
        int facDir = (int)transform.localScale.x;//人物的初始面向与transform相同
        if (inputDirection.x < 0) 
        { facDir = -1; }
        else if(inputDirection.x > 0) 
        { facDir = 1; }
        transform.localScale = new Vector3(facDir, 1, 1);
        #endregion
        
    }
    private void jump(InputAction.CallbackContext context)
    {
       
       if(physicsCheck.isGround||wallMovement.isWallMove)
        {
            countjump = 0;
            canjump = true;
        }else
        if (countjump >= 2)
        {
            canjump = false;
        }
        
        if (physicsCheck.isGround||canjump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            isjump = true;
            countjump++;
        }
    }
   
    /// <summary>
    ///受击向后击飞
    /// </summary>

    public void GetHurt(Transform attacker)
    { 
        isHurt = true;
        rb.velocity=Vector2.zero;
        if (physicsCheck.iswater)
        {
            Vector2 dir = new Vector2(0, 1).normalized;
            rb.AddForce(hurtWaterForce * dir, ForceMode2D.Impulse);
        }
        else
        {
            Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;//normalized将数值返回到0-1
            rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        }
    }
   
  
    private void PlyerAttack(InputAction.CallbackContext context)
    {
        playerAnimation.PlyerAttack();
        isattack = true;
       
    }
    /// <summary>
    /// 死亡停止控制
    /// </summary>
    public void PlayerDead()
    {
        isdead = true;
        inputControl.Player.Disable();
    }
    /// <summary>
    /// 检测是否有墙壁
    /// </summary>
    private void CheckState()
    {
        capsuleCollider.sharedMaterial = physicsCheck.isGround ? normal : wall;
        boxCollider2D.sharedMaterial =    physicsCheck.isGround ? normal : wall;
    }
    public void DestroyAfterAnimation()
    {
        Destroy(this.gameObject);

    }
    public void CloseGameobject()
    {
      GameObject gameObject = this.gameObject;
       
        isdead = false;
        gameObject.SetActive(false);
    }
    public void LoadDead()
    {
       deadUI.SetActive(true);
        
    }
    /// <summary>
    /// 冲刺
    /// </summary>
    private void Dashing()
    {
           CheckPower();
        if (!isDashing&&!physicsCheck.iswater)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift)&&canSkill)
            {
                isDashing = true;
                isjump = false;
                ani.SetBool("isdash", true);
                starDashing = dashTime;
                ///冲刺能量逻辑
                currentskillPower -= dashPower;
            }
           OnpowerChange?.Invoke(this);
        }
        else
        {
            starDashing -= Time.deltaTime;
            if(starDashing <=0)
            {
                isDashing = false;
                ani.SetBool("isdash", false);
            }
            else
            {
                dir = new Vector2(transform.localScale.x,-0.25f);
                rb.velocity = dir * rushForce;

            }
        }
       
    }
    /// <summary>
    /// 判断是否还有力量冲刺
    /// </summary>
    public void CheckPower()
    {
        if (currentskillPower-dashPower>= 0&&currentskillPower<=skillPower)
        {
            canSkill = true;
            if(currentskillPower!=skillPower)
            currentskillPower += Time.deltaTime * getPower;
        }
        else if(currentskillPower-dashPower<0||currentskillPower<=0)
        {
            canSkill = false;
            currentskillPower += Time.deltaTime * getPower;
        }else if (currentskillPower > skillPower)
        {
            currentskillPower = skillPower;
        }
        OnpowerChange?.Invoke(this);
    }

    public void checkHealth()
    {
        if (usewait >= 0)
        {
            usewait-= Time.deltaTime;
            isuse = false;
        }
        else
        {
            isuse = true; 
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (liquid.itemHeld > 0&&playerCharacter.currentHealth<playerCharacter.maxHealth&&isuse)
            {
                liquid.itemHeld --;
                playerCharacter.currentHealth += 50;
                playerCharacter.OnHealthChange?.Invoke(playerCharacter);
                InventoryManager.RefreshItem();
                usewait = usewaitstrat;
            }
        }
    }
}
