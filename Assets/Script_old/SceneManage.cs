using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class SceneManage : MonoBehaviour
{
    public static SceneManage Instance { get; private set; }
    public GameObject loadingScreen;
    public GameObject Player;
    PresistenceManagerScript pms;
    public GameObject EventSystem;
    public GameObject audioManager;

    private void Awake()
    {
       

    }


    public void Start()
    {
        //pms = GameObject.Find("PersistenceManager").GetComponent<PresistenceManagerScript>();

    }

    List<AsyncOperation> sceneLoading = new List<AsyncOperation>();

    [YarnCommand("LoadLevel")]
    public void LoadLevel(string levelName)
    {
        if (loadingScreen != null)
        {
            loadingScreen.gameObject.SetActive(true);
        }
       
        sceneLoading.Add(SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
        
    }

    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < sceneLoading.Count; i++)
        {
            while (!sceneLoading[i].isDone)
            {
                yield return null;
            }
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

            yield return new WaitForSeconds(1.5f);

            if (loadingScreen != null)
            {
                loadingScreen.gameObject.SetActive(false);
            }

        }
    }

    public void LoadDialogueSceneOnTop()
    {
        if (loadingScreen != null)
        {
            loadingScreen.gameObject.SetActive(true);
        }

       
        sceneLoading.Add(SceneManager.LoadSceneAsync("Dialogue_00_02", LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadProgressOnTop());
    }

    public IEnumerator GetSceneLoadProgressOnTop()
    {
        for (int i = 0; i < sceneLoading.Count; i++)
        {
            while (!sceneLoading[i].isDone)
            {
                yield return null;
            }
            //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

            yield return new WaitForSeconds(1.5f);

            if (loadingScreen != null)
            {
                loadingScreen.gameObject.SetActive(false);
                Player.SetActive(false);
                EventSystem.SetActive(false);
                if (audioManager != null)
                    audioManager.SetActive(false);

            }

        }
    }

    public void Unload()
    {
        /*if (loadingScreen.gameObject != null)
        {
            loadingScreen.gameObject.SetActive(true);
        }*/
        FindObjectOfType<PresistenceManagerScript>().ActivePlayer();
        SceneManager.UnloadSceneAsync("Dialogue_00_02");
        
    }


}
