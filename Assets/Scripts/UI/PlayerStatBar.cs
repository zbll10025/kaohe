using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;
    public Image powerImage;

    /// <summary>
    /// 根据百分比调整血量
    /// </summary>
    /// <param name="persentage"></param>
    public void Update()
    {
        HealthChange();
    }
   
    public void  OnSkillPowerchange(float persentage)
    {
        powerImage.fillAmount =  persentage;
    }
    public void   OnHealthChange(float persentage)
    {
        healthImage.fillAmount = persentage;
    }
    public void HealthChange()
    {
        if (healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount -= Time.deltaTime;
        }
    }
}
