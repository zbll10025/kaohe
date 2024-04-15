using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private SpriteRenderer SpriteRenderer;
    public Sprite openSprite;
    public Sprite closeSprite;
    public bool isDone;
    public GameObject liquid;
    public GameObject coin;
    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void OnEnable()
    {
        SpriteRenderer.sprite = isDone ? openSprite : closeSprite;
    }

    public void TriggerAction()
    {
        if (!isDone) {
            OpenChest();
        }
    }
    private void OpenChest()
    {

        SpriteRenderer.sprite = openSprite;
        isDone = true;
        this.gameObject.tag = "Untagged";
        if (liquid != null)
        liquid.gameObject.SetActive(true);
        if(coin!=null)
        coin.gameObject.SetActive(true);
    }

   
}
