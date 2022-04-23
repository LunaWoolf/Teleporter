using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class PresistenceManagerScript : MonoBehaviour
{
    public static PresistenceManagerScript Instance { get; private set; }

    public GameObject Player;
    public Vector3 PlayerNewPosition;
    public string Language = "zh-Hans";
    public DialogueRunner dr;

  


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    [YarnCommand("SetCurrentQuest_pms")]
    public void SetCurrentQuest(string questID)
    {
        PlayerStatus.currentQuest = questID;
    }

    [YarnFunction("PlayerName")]
    public static string GetPlayerName()
    {
        return PlayerStatus.playerName;

    }

    public void ChangePlayerName(string name)
    {

        PlayerStatus.playerName = name;
    }

    private float timer = 0;

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "OpenningScene")
        {
            if (Input.anyKeyDown)
                timer = 0;
            else
                timer += Time.deltaTime;

            if (timer > 200)
            {
                timer = 0;
                SceneManager.LoadScene("OpenningScene", LoadSceneMode.Single);
            }
        }
     
            
    }

}
