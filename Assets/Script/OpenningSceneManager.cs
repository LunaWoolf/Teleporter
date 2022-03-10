using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class OpenningSceneManager : MonoBehaviour
{
    public VideoPlayer vid;
    public SceneManage sceneManager;
    public GameObject LanguagechoicePage;


    void Start()
    {
        vid.loopPointReached += CheckOver;
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        LanguagechoicePage.SetActive(true);
    }

    public void SkipVideo()
    {
        vid.Stop();
    }

    public void SetLanaguage(string lan)
    {
        PresistenceManagerScript.Instance.Language = lan;
        sceneManager.LoadLevel("Dialogue_00_01");
    }

   
}
