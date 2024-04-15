using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public GameObject signSprite;
    public bool canPress;
    public IInteractable targetItem;
    private void Update()
    {
        signSprite.SetActive(canPress);
         OnConfirm();
    }
    private void OnConfirm()
    {
        if (canPress&&Input.GetKeyDown(KeyCode.E))
        {
            targetItem.TriggerAction();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
       
        if (collision.CompareTag("CanEnter"))
        {
           targetItem=collision.GetComponent<IInteractable>();
            canPress = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canPress = false;
    }

}
