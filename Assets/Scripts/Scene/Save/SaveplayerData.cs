using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveplayerData : MonoBehaviour
{
    public GameObject player;
    [System.Serializable]
    class SaveData
    {
        public Vector3 playerPosition;
    
    }

    public void Save()
    {
        var SaveData =new SaveData();

        SaveData.playerPosition=player.transform.position;

        SaveSystem.SaveByJson("PlayerData", SaveData);
    }

    

}
