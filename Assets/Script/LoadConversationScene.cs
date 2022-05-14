using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadConversationScene : MonoBehaviour
{
    public string levelName;
    public SceneManage sceneManager;

    public void Start()
    {
        if (sceneManager == null)
        {
            sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManage>();
             
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (levelName == "Dialogue_00_02")
            {
                //PlayerStatus.playerPosition = new Vector3(-18.9f, 21f, -99f);
                /*PlayerPrefs.SetFloat("p_x", -18.9f);
                PlayerPrefs.SetFloat("p_y", 21f);
                PlayerPrefs.SetFloat("p_z", -99f);
                PlayerPrefs.SetInt("save", 1);
                PlayerPrefs.SetInt("mono", 2);
                PlayerPrefs.Save();*/

                
            }
            sceneManager.LoadLevel(levelName);

            //SceneManager.UnloadSceneAsync("Chapter0");

            //SceneManager.LoadScene(levelName, LoadSceneMode.Single);
            

        }
    }
}
