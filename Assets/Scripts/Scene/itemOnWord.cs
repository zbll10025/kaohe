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
            //����û�������Ʒ����뱳�����ڱ���������
         
        }
        else
        {
            thisitem.itemHeld += 1;
        }
        InventoryManager.RefreshItem();
    }
    
}
