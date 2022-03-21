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
        PlayerStatus.playerPosition = new Vector3(235.63f, 1.55f, -15.76f);
        Debug.Log(PlayerStatus.playerPosition);
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
        //sceneManager.LoadLevel("Chapter0");
        sceneManager.LoadLevel("Dialogue_00_01");
    }

   
}
