using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetting : MonoBehaviour
{
   public  Item liquid;
   public Character playercharacter;

    public void Replayer()
    {
        playercharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        liquid.itemHeld = 51;
        playercharacter.currentHealth = playercharacter.maxHealth;
        playercharacter.OnHealthChange?.Invoke(playercharacter);
    }

}
