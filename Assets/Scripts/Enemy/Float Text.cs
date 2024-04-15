using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class FloatText : MonoBehaviour
{
    [SerializeField] GameObject perFab;
   
    public void Hit(float damage)
    {
        GameObject obj =Instantiate(perFab,transform.position,Quaternion.identity);
        perFab.GetComponent<Canvas>().worldCamera=Camera.main;
        obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text=damage.ToString();
        Destroy(obj,1f);
    }
    
}
