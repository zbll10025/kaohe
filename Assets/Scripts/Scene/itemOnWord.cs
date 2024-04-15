using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemOnWord : MonoBehaviour
{
    public Item thisitem;
    public InvenatoryScripts playerInventory;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AddNewItem();
            Destroy(gameObject);
        }
    }

    private void AddNewItem()
    {
        if (!playerInventory.itemList.Contains(thisitem))
        {
            playerInventory.itemList.Add(thisitem);
            //InventoryManager.CreateNewItem(thisitem);
            //背包没有这件物品则加入背包且在背包创建它
         
        }
        else
        {
            thisitem.itemHeld += 1;
        }
        InventoryManager.RefreshItem();
    }
    
}
