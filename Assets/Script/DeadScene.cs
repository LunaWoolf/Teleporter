using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadScene : MonoBehaviour
{

    public SceneManage sceneManager;
    void Start()
    {
        if (sceneManager == null)
        {
            sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManage>();

        }

    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (sceneManager != null)
            {
                sceneManager.LoadLevel("Chapter0");
                PlayerStatus.currentQuest = "Quest1_P";
            }
               
        }

    }
}
