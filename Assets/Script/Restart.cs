using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Restart : MonoBehaviour
{
    public SceneManage sceneManager;
    public GameObject pms;
    public GameObject eventSystem;
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
        if (Input.GetKey(KeyCode.LeftCommand) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.R))
        {
            RestartGame();
        }



    }

    public void RestartGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerStatus.currentQuest = "Quest1_P";
        pms = GameObject.Find("PersistenceManager");

       
        if (sceneManager == null)
        {
            sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManage>();
        }
        
        if (sceneManager != null)
        {
            eventSystem.SetActive(false);
            sceneManager.LoadLevel("OpenningScene");
        }
        if (pms != null)
        {
            Destroy(pms.gameObject);
        }



    }
}
