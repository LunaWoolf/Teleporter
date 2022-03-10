using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class SceneManage : MonoBehaviour
{
 
    [YarnCommand("LoadLevel")]
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
  

    }
}
