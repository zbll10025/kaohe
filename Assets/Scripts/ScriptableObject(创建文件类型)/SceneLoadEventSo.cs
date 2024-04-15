
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Event/SceneLoadEvent")]
public class SceneLoadEvent : ScriptableObject 
{ 
    public UnityAction<GameSceneSo,Vector3,bool>LoadRequestEvent;
    /// <summary>
    /// ������������
    /// </summary>
    /// <param name="locationToLoad">Ҫ���صĳ���</param>
    /// <param name="postToGo">����ڼ��س�����ĳ�ʼλ��</param>
    /// <param name="fadeScreen">�Ƿ�Ҫ���붯��</param>
    public void RaiseLoadRequestEvent(GameSceneSo locationToLoad,Vector3 postToGo,bool fadeScreen)
    {
        LoadRequestEvent?.Invoke(locationToLoad, postToGo, fadeScreen);
    }
}
