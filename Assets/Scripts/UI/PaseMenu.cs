using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PaseMenu : MonoBehaviour
{
   public  GameObject paseMenu;
   public TeleportPoint menu;
    private void Awake()
    {
        menu = GetComponent<TeleportPoint>();
    }

    public void Continue()
    {
       paseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void Exit()
    {
       
        menu.TriggerAction();
        paseMenu.SetActive(false);
    }
    public void Save()
    {

    }
}
