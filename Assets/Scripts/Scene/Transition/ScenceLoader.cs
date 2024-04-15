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
    [Header("����")]
    public SceneLoadEvent loadEventSO;
    public GameSceneSo firstLoadScene;
    private GameSceneSo sceneToLoad;
    [SerializeField]public GameSceneSo currentLoadScene;
    [Header("����")]
    public voidEventSO afterSceneLoadedEvent;
    [Header("��ʱ����")]
    
    private Vector3 position;
    private bool fadeScreen;
    
    [Header("��ʼ")]
   

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
    #region �����³���

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
            StartCoroutine(UnloadPreviousScene());//��ʼЯ��
        }
        else
        {
            LoadNewScene();
        }
    }
    /// <summary>
    /// Я��ж�س���
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
            yield return  currentLoadScene.sceneReference.UnLoadScene();//�ҵ����ڵĳ������ý���ж��
           // playerTransform.gameObject.SetActive(false);
            LoadNewScene();
    }
    /// <summary>
    /// �����³���
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
            //���ػ���
        }
        isLoding= false;//��ֹ���һֱ�������
        //���س���֮����¼�
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
