using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Use : MonoBehaviour
{
    public Character Player;
    public UnityEvent<Character> OnHealthChange;
    public PlyerController PlyerController;
    public void RecoverHealth()
    {
        if (Player.currentHealth<Player.maxHealth&&PlyerController.isuse){
            Player.currentHealth += 50;
            OnHealthChange?.Invoke(Player);
            PlyerController.usewait = PlyerController.usewaitstrat;
        }
    }
}
