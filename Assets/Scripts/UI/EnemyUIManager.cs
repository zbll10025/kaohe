using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUIManager : MonoBehaviour
{
   public EnemyStatBar EnemyStatBar;
    [Header("¼àÌý")]
    public CharacterEventSo healthEvent;
    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
       
    }

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
       
    }
    private void OnHealthEvent(Character character)
    {
        var persentage = character.currentHealth / character.maxHealth;
        EnemyStatBar.OnHealthChange(persentage);
    }
}
