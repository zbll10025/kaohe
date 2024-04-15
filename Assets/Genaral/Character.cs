using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Character : MonoBehaviour
{
    public PlyerController plyerController;
    public FloatText floatText;
    public Attack scene;
    [Header("基本属性")]
    public float maxHealth;
    public float currentHealth;
    [Header("受伤无敌")]
    public float invulnerableDuration;//(无法伤害的时间）
    public float invulnerableCounter;
    public bool invulnerable;//无敌状态
    public UnityEvent<Transform> OntakeDamage;//将各种函数注册进ontakedamage函数中<transform>将给transform组件传入进去
    public UnityEvent Ondide;
    public UnityEvent<Character> OnHealthChange;
    public Character my;
    private void Start()
    {

        currentHealth = maxHealth;
        OnHealthChange?.Invoke(this);
        try
        {
            plyerController = GetComponent<PlyerController>();
        }
        catch
        {

        }
    }
    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter-=Time.deltaTime;//Time.deltaTime为完成上一帧的时间
        }
        if(invulnerableCounter <= 0 ) {
            invulnerable = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("water"))
        {
            Ondide?.Invoke();
            OnHealthChange?.Invoke(this);
        }
        else if (collision.CompareTag("Ci"))
        {
           
            TakeDamage(scene);

        }
    }
   public void TakeDamage(Attack attacker)//伤害计算
    {
        if (invulnerable) { return; }
        if (plyerController != null)
        {
            if (invulnerable || plyerController.isDashing) { return; }
        }
        if (currentHealth - attacker.damage > 0)
        {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
            OntakeDamage?.Invoke(attacker.transform);
        }//受到伤害重新恢复无敌时间
        else
        {
            
            currentHealth = 0;
            Ondide?.Invoke();
            //触发死亡
        }
        OnHealthChange?.Invoke(this);
    }
    public void TakeDamage(Playerattack attacker)//伤害计算
    {
        if (invulnerable) { return; }
        if (currentHealth - attacker.damage > 0)
        {
            currentHealth -= attacker.damage;
            floatText.Hit(attacker.damage);
            TriggerInvulnerable();
            OntakeDamage?.Invoke(attacker.transform);
        }//受到伤害重新回复无敌时间
        else
        {
            currentHealth = 0;
            Ondide?.Invoke();
            //出发死亡
        }
        OnHealthChange?.Invoke(this);
    }
    /// <summary>
    /// 触发受伤无敌
    /// </summary>
    private void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
   public void fuhuo()
    {
        currentHealth = maxHealth;
        OnHealthChange?.Invoke(this);
    }
}
