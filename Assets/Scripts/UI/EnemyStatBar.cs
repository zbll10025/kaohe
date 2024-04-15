using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatBar : MonoBehaviour
{
    public Image redHealth;
    public void OnHealthChange(float persentage)
    {
       
        redHealth.fillAmount = persentage;
    }
}

