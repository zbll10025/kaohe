using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayerData : MonoBehaviour
{
    public GameSceneSo gameScene;
    public Vector3 playerPositiion;
    [System.Serializable]class SaveData
    {
        public Vector3 playerPosition;

    }
    [System.Serializable]class PerSence
    {
        public GameSceneSo gameScene;
    }
    [System.Serializable]class SCScene {
        public GameSceneSo gameScene;
    }
    class Stone {
        public GameSceneSo stoneScene;
    }



    public void Load()
    {
       var SaveData = SaveSystem.LoadFromJson<SaveData>("PlayerData");
        var PerSence = SaveSystem.LoadFromJson<PerSence>("PreSence");
        gameScene =PerSence.gameScene;
        playerPositiion = SaveData.playerPosition;
        
    }
    public void LoadStoneSave() {
        var stone = SaveSystem.LoadFromJson<Stone>("StoneScene");
        var SaveData = SaveSystem.LoadFromJson<SaveData>("PlayerData");
        gameScene= stone.stoneScene;
        playerPositiion=SaveData.playerPosition;

    }
}
