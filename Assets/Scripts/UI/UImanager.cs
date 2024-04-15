using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    public GameObject paseMenu;
    [Header("¼àÌýÊÂ¼þ")]
    public CharacterEventSo healthEvent;
    public PlayerSkillEventSo powerEvent;

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
        powerEvent.OnEventRaised += OnpowerEvent;
    }

   

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
        powerEvent.OnEventRaised -= OnpowerEvent;
    }

    

    private void OnHealthEvent(Character character)
    {
      var persentage = character.currentHealth / character.maxHealth;
        playerStatBar.OnHealthChange(persentage);
    }
    private void OnpowerEvent(PlyerController controller)
    {
        var Skillpersentage = controller.currentskillPower / controller.skillPower;
        playerStatBar.OnSkillPowerchange(Skillpersentage);

    }
    public void pase()
    {
        paseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

}
