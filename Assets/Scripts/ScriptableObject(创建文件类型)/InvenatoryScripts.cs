using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("Inventory/New Inventory"))]
public class InvenatoryScripts : ScriptableObject
{
    public List<Item>itemList= new List<Item>();
    
}
