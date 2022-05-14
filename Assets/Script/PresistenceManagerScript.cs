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
    public string Language = "en";
    public DialogueRunner dr;
    public GameObject EventSystem;
    public GameObject audioManager;
    public bool pause;
    public Restart restart;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(Instance.gameObject);
            DontDestroyOnLoad(gameObject);

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
        if (SceneManager.GetActiveScene().name != "OpenningScene" && !pause)
        {
            if (Input.anyKeyDown)
                timer = 0;
            else
                timer += Time.deltaTime;

            if (timer > 200)
            {
                timer = 0;
                restart = FindObjectOfType<Restart>();
                if (restart != null)
                    restart.RestartGame();
                
            }
        }
       
    }

    public void ActivePlayer()
    {
        Player.SetActive(true);
        Player.GetComponentInChildren<PlayerInteraction>().EndDialogue();
        EventSystem.SetActive(true);
        audioManager.SetActive(true);

    }
}
