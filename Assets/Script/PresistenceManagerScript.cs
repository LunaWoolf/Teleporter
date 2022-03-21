using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PresistenceManagerScript : MonoBehaviour
{
    public static PresistenceManagerScript Instance { get; private set; }

    public GameObject Player;
    public Vector3 PlayerNewPosition;

    public string Language = "zh-Hans";

    
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

    public void LoadPlayerPosition()
    {
        Debug.Log("okay");
        Player = GameObject.Find("Player");
        Player.GetComponent<Transform>().position = PlayerNewPosition;

    }



  
}
