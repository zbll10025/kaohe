using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuhuoplayerload : MonoBehaviour
{
   public  GameObject player;
    public GameObject UI;
    public Character playercharacter;
    public Animator ani;
    public void Fuhuo()
    {
        player.SetActive(true);
        ani.SetBool("isdead", false);
        playercharacter.fuhuo();
        Time.timeScale = 1.0f;
        UI.SetActive(false);

    }
}
