using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    public SceneManage sceneManager;
    public GameObject pms;
    // Start is called before the first frame update
    void Start()
    {
        if (sceneManager == null)
        {
            sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManage>();
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void RestartGame()
    {
        pms = GameObject.Find("PersistenceManager");
        if (pms != null)
        {
            Destroy(gameObject);
        }

        if (sceneManager != null)
        {
            sceneManager.LoadLevel("OpenningScene");
        }
        


    }
}
