using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class OpenningSceneManager : MonoBehaviour
{
    public VideoPlayer vid;
    public SceneManage sceneManager;


    void Start()
    {
        vid.loopPointReached += CheckOver;
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        sceneManager.LoadLevel("DialogueScene");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
