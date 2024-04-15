using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
public class ScenceLoader : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    public Transform playerTransform;
    public Animator playeranimator;
    public Vector3 firstposition; 
    [Header("监听")]
    public SceneLoadEvent loadEventSO;
    public GameSceneSo firstLoadScene;
    private GameSceneSo sceneToLoad;
    [SerializeField]public GameSceneSo currentLoadScene;
    [Header("播报")]
    public voidEventSO afterSceneLoadedEvent;
    [Header("临时储存")]
    
    private Vector3 position;
    private bool fadeScreen;
    
    [Header("初始")]
   

    public float fadeDuration;
    public bool isLoding;
    [System.Serializable] class PerSence
    { 
      public  GameSceneSo gameScene;

    }
    [System.Serializable] class SCsence
    {
        public GameSceneSo gameScene;
    }

    private void Awake()
    {
        //Addressables.LoadSceneAsync(firstLoadScene.sceneReference,LoadSceneMode.Additive);
        //currentLoadScene = firstLoadScene;
        //currentLoadScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
    }
    private void Start()
    {
        NewGame();
    }
    private void OnEnable()
    {
        loadEventSO.LoadRequestEvent += OnLoadReRequestEvent;
    }
    private void OnDisable()
    {
        loadEventSO.LoadRequestEvent -= OnLoadReRequestEvent;
    }
    private void NewGame()
    {

        sceneToLoad = firstLoadScene;
        position = firstposition;
        OnLoadReRequestEvent(sceneToLoad, position, true);
        
    }
    #region 加载新场景

    private void OnLoadReRequestEvent(GameSceneSo locationToLoad, Vector3 posToGo, bool fadeScreen)
    {
        playerRigidbody.velocity = Vector2.zero;
        if (isLoding)
        {
            return;
        }
        isLoding = true;
        sceneToLoad=locationToLoad;
        position = posToGo;
        this.fadeScreen = fadeScreen;
        if (currentLoadScene != null)
        {
            StartCoroutine(UnloadPreviousScene());//开始携程
        }
        else
        {
            LoadNewScene();
        }
    }
    /// <summary>
    /// 携程卸载场景
    /// </summary>
    /// <returns></returns>
    private IEnumerator UnloadPreviousScene() {
        if(currentLoadScene != null)
        {
            var PerSence = new PerSence();
            PerSence.gameScene=currentLoadScene;
            SaveSystem.SaveByJson("PreSence", PerSence);

        }
        if(fadeScreen)
        {
     
        }
            yield return  new WaitForSeconds(fadeDuration);
            yield return  currentLoadScene.sceneReference.UnLoadScene();//找到现在的场景引用将其卸载
           // playerTransform.gameObject.SetActive(false);
            LoadNewScene();
    }
    /// <summary>
    /// 加载新场景
    /// </summary>
    private void LoadNewScene()
    {
       var  LoadingOption= sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        LoadingOption.Completed += OnLoadComplete;
    }

    private void OnLoadComplete(AsyncOperationHandle<SceneInstance> obj)
    {  
        currentLoadScene = sceneToLoad;
         SaveCurrentScene();
        playerTransform.position = position;
        playeranimator.SetBool("isdead", false);
       // playerTransform.gameObject.SetActive(true);
       
        if (fadeScreen)
        {
            //加载画面
        }
        isLoding= false;//防止玩家一直点击互动
        //加载场景之后的事件
        afterSceneLoadedEvent.RaiseEvent();
        Time.timeScale = 1.0f;
    }

    
    private void SaveCurrentScene()
    {
        var scscene = new SCsence();
        scscene.gameScene = currentLoadScene;
        SaveSystem.SaveByJson("SCscene", scscene);
    }
    #endregion
}
