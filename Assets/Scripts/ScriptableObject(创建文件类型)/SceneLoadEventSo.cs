
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Event/SceneLoadEvent")]
public class SceneLoadEvent : ScriptableObject 
{ 
    public UnityAction<GameSceneSo,Vector3,bool>LoadRequestEvent;
    /// <summary>
    /// 场景加载请求
    /// </summary>
    /// <param name="locationToLoad">要加载的场景</param>
    /// <param name="postToGo">玩家在加载场景后的初始位置</param>
    /// <param name="fadeScreen">是否要载入动画</param>
    public void RaiseLoadRequestEvent(GameSceneSo locationToLoad,Vector3 postToGo,bool fadeScreen)
    {
        LoadRequestEvent?.Invoke(locationToLoad, postToGo, fadeScreen);
    }
}
