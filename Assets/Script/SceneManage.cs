using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
        /*
        switch (levelName)
        {
            case "DialogueScene":
                SceneManager.LoadScene("DialogueScene");
                break;
            case "DialogueScene":
                SceneManager.LoadScene("DialogueScene");
                break;
            default:
                Debug.Log("No Such Scene");
                break;
        }
        */

    }
}
