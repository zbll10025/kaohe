using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class PlyerController : MonoBehaviour
{
    public PlayerInputControl inputControl;//(����������unitysystem���������йصĴ���
    public Vector2 inputDirection;//��ȡX��Y
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

    [Header("��������")]
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
    [Header("�������")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;
    [Header("����")]
    public bool isHurt;
    public bool isdead;
    public bool isattack;
    public bool isDashing;
    public bool isjump;
    public bool canjump;
    public int countjump=0;
    public bool canSkill;
    public Item liquid;
    [Header("��ҩʱ��")]
    public float usewait;
    public float usewaitstrat;
    public bool isuse;
    #region ��ʼ����
    private void Awake()//��strat����ִ��
    {
        boxCollider2D =GetComponent<BoxCollider2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        rb = GetComponent<Rigidbody2D>();
        inputControl = new PlayerInputControl();//���ñ�������ʵ����
         playerAnimation =GetComponent<playerAnimation>();
        capsuleCollider=GetComponent<CapsuleCollider2D>();
        wallMovement=GetComponent<WallMovement>();
        ani = GetComponent<Animator>();
        currentskillPower=skillPower; 
        OnpowerChange?.Invoke(this);inputControl.Player.Jump.started += jump;//���¼�ע�ắ���������°���ʱִ��jump����
        inputControl.Player.Attack.started += PlyerAttack;
        usewaitstrat = 20f;
        usewait = -1f;
    }

    

    private void OnEnable()//�����ں�������ǰ���屻����ʱinputControlҲ������
    {
        inputControl.Enable();
    }
    private void OnDisable()//(���ں�������ǰ���屻�ر�ʱinputControlҲ���ر�
    {
        inputControl.Disable();
    }
    #endregion
    private void Update()
    {
         inputDirection = inputControl.Player.Move.ReadValue<Vector2>();//��ȡ���úõ�vector2��ֵ
          CheckState();
          Dashing();
        checkHealth();
        
    }


    private void FixedUpdate()//�̶�����ֵ
    {
        if (!isHurt&&!isattack&&!isDashing)
        {
            Move();
        }
    }
    public void Move()
    {
        #region ���ݰ������ƶ�
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime,rb.velocity.y);//ʱ������
        #endregion

        #region �����ƶ���ʹ���﷭ת
        int facDir = (int)transform.localScale.x;//����ĳ�ʼ������transform��ͬ
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
    ///�ܻ�������
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
            Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;//normalized����ֵ���ص�0-1
            rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        }
    }
   
  
    private void PlyerAttack(InputAction.CallbackContext context)
    {
        playerAnimation.PlyerAttack();
        isattack = true;
       
    }
    /// <summary>
    /// ����ֹͣ����
    /// </summary>
    public void PlayerDead()
    {
        isdead = true;
        inputControl.Player.Disable();
    }
    /// <summary>
    /// ����Ƿ���ǽ��
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
    /// ���
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
                ///��������߼�
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
    /// �ж��Ƿ����������
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
