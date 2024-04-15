using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Character : MonoBehaviour
{
    public PlyerController plyerController;
    public FloatText floatText;
    public Attack scene;
    [Header("��������")]
    public float maxHealth;
    public float currentHealth;
    [Header("�����޵�")]
    public float invulnerableDuration;//(�޷��˺���ʱ�䣩
    public float invulnerableCounter;
    public bool invulnerable;//�޵�״̬
    public UnityEvent<Transform> OntakeDamage;//�����ֺ���ע���ontakedamage������<transform>����transform��������ȥ
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
            invulnerableCounter-=Time.deltaTime;//Time.deltaTimeΪ�����һ֡��ʱ��
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
   public void TakeDamage(Attack attacker)//�˺�����
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
        }//�ܵ��˺����»ָ��޵�ʱ��
        else
        {
            
            currentHealth = 0;
            Ondide?.Invoke();
            //��������
        }
        OnHealthChange?.Invoke(this);
    }
    public void TakeDamage(Playerattack attacker)//�˺�����
    {
        if (invulnerable) { return; }
        if (currentHealth - attacker.damage > 0)
        {
            currentHealth -= attacker.damage;
            floatText.Hit(attacker.damage);
            TriggerInvulnerable();
            OntakeDamage?.Invoke(attacker.transform);
        }//�ܵ��˺����»ظ��޵�ʱ��
        else
        {
            currentHealth = 0;
            Ondide?.Invoke();
            //��������
        }
        OnHealthChange?.Invoke(this);
    }
    /// <summary>
    /// ���������޵�
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
