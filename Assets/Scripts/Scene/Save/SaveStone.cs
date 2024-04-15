using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveStone : MonoBehaviour, IInteractable
{
   public SaveplayerData playerData;
    public GameObject sign;
    public SpriteRenderer SpriteRenderer;
    public GameSceneSo currentScene;
    public float colora;
    [System.Serializable]
    class Stone {

        public GameSceneSo stoneScene;
    }
    [System.Serializable]
    class SCsence
    {
        public GameSceneSo gameScene;
    }
    

    public void TriggerAction()
    {
        StoneSign();
        playerData.Save();
        SaveStoneCurrentScene();
    }

    private void StoneSign()
    {
       SpriteRenderer= sign.GetComponent<SpriteRenderer>();
        Color color=SpriteRenderer.color; 
        colora=color.a;
        color.a= 0.25f;
        SpriteRenderer.color=color;
         Invoke("DelayedMethod", 0.25f);
    }
    public void DelayedMethod()
    {
        
        Color color = SpriteRenderer.color;
        color.a = colora;
        SpriteRenderer.color = color;

    }
   public void SaveStoneCurrentScene()
    {
        var scscene = SaveSystem.LoadFromJson<SCsence>("SCscene"); 
        currentScene=scscene.gameScene;
        var stone=new Stone();
        stone.stoneScene = currentScene;
        SaveSystem.SaveByJson("StoneScene", stone);
    }
}
