using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadConversationScene : MonoBehaviour
{
    public string levelName;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (levelName == "Dialogue_00_02")
            {
                PlayerStatus.playerPosition = new Vector3(-35.7f, 21f, -99f);

            }
            Debug.Log("scene");
            SceneManager.UnloadSceneAsync("Chapter0");
            SceneManager.LoadScene(levelName, LoadSceneMode.Single);
            

           

           

        }
    }
}
