using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem 
{
    public static void SaveByJson(string saveFileName,object data)
    {
        var json=JsonUtility.ToJson(data);
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        
        try
        { 
        File.WriteAllText(path,json);//����ļ�������д�ļ��е�����
        
            
        } 
        catch
        { //�����쳣
        }
    }
    public static T LoadFromJson<T>(string saveFileName)
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
       
        
        try
        {
            var json=File.ReadAllText(path);
             var data =JsonUtility.FromJson<T>(json);
               return data;
        }
        catch
        {
            return default;
        }
    }

     public static void DeleteSaveFile(string saveFileName)
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        File.Delete(path);
    }

}
