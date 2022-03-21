using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class SceneManage : MonoBehaviour
{
    PresistenceManagerScript pms;

    public void Start()
    {
        //pms = GameObject.Find("PersistenceManager").GetComponent<PresistenceManagerScript>();
       
        
    }

    [YarnCommand("LoadLevel")]
    public void LoadLevel(string levelName)
    {
     
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);

     
       
  

    }

    public void LoadChapter0()
    {

    }

}
