using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    public InvenatoryScripts myBag;
    public GameObject slotGrid;//在格子之内生成
    public Slot slotPrefab;
    public Text itemInfromation;
    public Use use;
    public bool select;
    public bool continueUse;
    public Item item;
    [SerializeField] public string itemName;
    public bool isCanuse;
    public PlyerController playerController;
    public Character character;
   void Awake()
    {
           if(instance !=null)
        {
            Destroy(this);
        }
        instance = this;
    }
    private void OnEnable()
    {
        RefreshItem();
        instance.itemInfromation.text = "";
        
    }
    public static void CreateNewItem(Item item)
    {
       
        Slot newItem = Instantiate(instance.slotPrefab,instance.slotGrid.transform.position,Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        newItem.slotNum.text=item.itemHeld.ToString();
    }
    ///创建新物体
    public static void RefreshItem()
    {
        for(int i = 0;i<instance.slotGrid.transform.childCount;i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
            {
                break;
            }
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }
        for(int i = 0;i<instance.myBag.itemList.Count;i++)
        {
            CreateNewItem(instance.myBag.itemList[i]);
        }

    }
    public static void UpdateItemInfo(string itemDscription)
    {
        instance.itemInfromation.text = itemDscription;
    }
    public static void SelectUse(Item item)
    {
       instance.select = true;
       instance.item = item;
       instance.itemName= item.name;
       instance. Checkisuse();
    }
    public  void Usse()
    {
        CheckContinueUse();
        if(instance.select&&instance.continueUse&&instance.isCanuse&&instance.character.currentHealth<instance.character.maxHealth&&instance.playerController.isuse)
        {
          instance.use.RecoverHealth();
            instance.item.itemHeld -= 1;
            RefreshItem();

        }
    }

    private void Checkisuse()
    {
        if (instance.itemName == "liquid")
        {
            isCanuse= true;
        }
        else
        {
            isCanuse= false;
        }
    }

    private void CheckContinueUse()
    {
        if (item != null)
        {
            if (instance.item.itemHeld > 0)
            {
                instance.continueUse = true;
            }
            else
            {
                instance.continueUse = false;
            }
        }
    }
}
