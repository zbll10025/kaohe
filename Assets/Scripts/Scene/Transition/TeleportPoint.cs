using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour, IInteractable
{
     
    public SceneLoadEvent loadEventSO;
    public GameSceneSo sceneToGo;
    public Vector3 positionToGo;
    public LoadPlayerData playerData;


    public void Load()
    {
        sceneToGo = playerData.gameScene;
        positionToGo = playerData.playerPositiion;
        if(sceneToGo==null||positionToGo==null)
        {
            return;
        }
        TriggerAction();
    }
    public void TriggerAction()
    {
       
        loadEventSO.RaiseLoadRequestEvent(sceneToGo, positionToGo,true);
    }
}
