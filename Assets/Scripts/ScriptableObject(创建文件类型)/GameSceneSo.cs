using UnityEngine.AddressableAssets;
using UnityEngine;

[CreateAssetMenu(menuName ="Game Scene/GameScenesSo")]
public class GameSceneSo : ScriptableObject
{
    public SceneType SceneType;
    public AssetReference sceneReference;//资源引用
}
