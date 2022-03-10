using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresistenceManagerScript : MonoBehaviour
{
    public static PresistenceManagerScript Instance { get; private set; }

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

  
}
